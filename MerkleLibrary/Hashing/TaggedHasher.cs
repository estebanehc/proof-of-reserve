using System.Security.Cryptography;
using System.Text;

namespace MerkleLibrary.Hashing
{
    /// <summary>
    /// Implements the BIP340-style tagged hash for Merkle leaf and branch hashing.
    /// </summary>
    public class TaggedHasher(string leafTag, string branchTag) : IMerkleHasher
    {
        private readonly byte[] _leafTagHash = Sha256(Encoding.UTF8.GetBytes(leafTag));
        private readonly byte[] _branchTagHash = Sha256(Encoding.UTF8.GetBytes(branchTag));

        
        /// <summary>
        /// Computes the hash of a Merkle leaf node using the tagged hash mechanism.
        /// </summary>
        /// <param name="input">The input string to hash.</param>
        /// <returns>The computed hash of the leaf node.</returns>
        public byte[] HashLeaf(string input)
        {
            var message = Encoding.UTF8.GetBytes(input);
            return TaggedHash(_leafTagHash, message);
        }
        /// <summary>
        /// Computes the hash of a Merkle branch by combining the left and right child hashes.
        /// </summary>
        /// <param name="left">The hash of the left child node.</param>
        /// <param name="right">The hash of the right child node.</param>
        /// <returns>The computed hash of the branch.</returns>
        public byte[] HashBranch(byte[] left, byte[] right)
        {
            var combined = Combine(left, right);
            return TaggedHash(_branchTagHash, combined);
        }

        private static byte[] TaggedHash(byte[] tagHash, byte[] message)
        {
            var data = new byte[tagHash.Length * 2 + message.Length];
            Buffer.BlockCopy(tagHash, 0, data, 0, tagHash.Length);
            Buffer.BlockCopy(tagHash, 0, data, tagHash.Length, tagHash.Length);
            Buffer.BlockCopy(message, 0, data, tagHash.Length * 2, message.Length);
            return Sha256(data);
        }

        private static byte[] Sha256(byte[] input)
        {
            return SHA256.HashData(input);
        }

        private static byte[] Combine(byte[] left, byte[] right)
        {
            var result = new byte[left.Length + right.Length];
            Buffer.BlockCopy(left, 0, result, 0, left.Length);
            Buffer.BlockCopy(right, 0, result, left.Length, right.Length);
            return result;
        }
    }
}
