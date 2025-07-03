using MerkleLibrary.Hashing;

namespace MerkleLibrary.Tree
{
    /// <summary>
    /// Generic Merkle Tree builder that returns the root hash from a list of inputs.
    /// </summary>
    /// <typeparam name="T">The type of input values (e.g., string, int) convertible to string.</typeparam>
    public class MerkleTree<T>(IMerkleHasher hasher)
    {
        private readonly IMerkleHasher _hasher = hasher;

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
    }
}
