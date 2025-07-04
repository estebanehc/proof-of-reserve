using MerkleLibrary.Hashing;

namespace MerkleLibrary.Tree
{
    /// <summary>
    /// Generic Merkle Tree builder that returns the root hash from a list of inputs.
    /// </summary>
    /// <typeparam name="T">The type of input values (e.g., string, int) convertible to string.</typeparam>
    public class MerkleTree<T>
    {
        private readonly IMerkleHasher _hasher;

        /// <summary>
        /// Initializes a new instance of the <see cref="MerkleTree{T}"/> class.
        /// </summary>
        /// <param name="hasher">The hasher implementation used for hashing leaves and branches.</param>
        public MerkleTree(IMerkleHasher hasher)
        {
            _hasher = hasher;
        }

        /// <summary>
        /// Computes the Merkle Root for the given data set.
        /// </summary>
        /// <param name="items">List of items to include in the Merkle tree.</param>
        /// <returns>Merkle root hash as a byte array.</returns>
        /// <exception cref="ArgumentException">Thrown if the input list is empty.</exception>
        public byte[] ComputeRoot(IEnumerable<T> items)
        {
            var leaves = items.Select(i => _hasher.HashLeaf(i!.ToString()!)).ToList();
            if (leaves.Count == 0)
                throw new ArgumentException("Cannot compute Merkle Root of empty list.");

            return ComputeRootFromLeaves(leaves);
        }

        private byte[] ComputeRootFromLeaves(List<byte[]> currentLevel)
        {
            while (currentLevel.Count > 1)
            {
                var nextLevel = new List<byte[]>();

                for (int i = 0; i < currentLevel.Count; i += 2)
                {
                    var left = currentLevel[i];
                    var right = (i + 1 < currentLevel.Count) ? currentLevel[i + 1] : currentLevel[i]; // Duplicate last if odd

                    var parent = _hasher.HashBranch(left, right);
                    nextLevel.Add(parent);
                }

                currentLevel = nextLevel;
            }

            return currentLevel[0];
        }

        /// <summary>
        /// Generates a Merkle proof for a specific item in the list.
        /// </summary>
        /// <param name="items">The list of items included in the Merkle tree.</param>
        /// <param name="target">The item for which the proof is generated.</param>
        /// <returns>A list of <see cref="MerkleProofStep"/> representing the proof.</returns>
        /// <exception cref="ArgumentException">Thrown if the target item is not found in the list.</exception>
        public List<MerkleProofStep> GetMerkleProof(List<string> items, string target)
        {
            var leafHashes = items.Select(_hasher.HashLeaf).ToList();

            // Find the index of the target item
            int index = items.IndexOf(target);
            if (index == -1)
                throw new ArgumentException("Item not found in the list");

            var proof = new List<MerkleProofStep>();
            var currentLevel = leafHashes;

            while (currentLevel.Count > 1)
            {
                var nextLevel = new List<byte[]>();
                int siblingIndex;

                for (int i = 0; i < currentLevel.Count; i += 2)
                {
                    var left = currentLevel[i];
                    var right = (i + 1 < currentLevel.Count) ? currentLevel[i + 1] : currentLevel[i];
                    var parent = _hasher.HashBranch(left, right);
                    nextLevel.Add(parent);

                    // If the current node is part of this pair
                    if (i == index || i + 1 == index)
                    {
                        if (i == index) // Node is on the left
                        {
                            siblingIndex = (i + 1 < currentLevel.Count) ? i + 1 : i;
                            proof.Add(new MerkleProofStep(currentLevel[siblingIndex], 1)); // Sibling to the right
                        }
                        else // Node is on the right
                        {
                            siblingIndex = i;
                            proof.Add(new MerkleProofStep(currentLevel[siblingIndex], 0)); // Sibling to the left
                        }

                        index = nextLevel.Count - 1;
                    }
                }

                currentLevel = nextLevel;
            }

            return proof;
        }
    }
}
