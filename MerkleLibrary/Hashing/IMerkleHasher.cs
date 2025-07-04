namespace MerkleLibrary.Hashing
{
    /// <summary>
    /// Define the contract for Merkle hashers, supporting leaf and branch hashing.
    /// </summary>
    public interface IMerkleHasher
    {
        /// <summary>
        /// Hash a leaf node (raw value like "aaa", "(1,1111)", etc.).
        /// </summary>
        /// <param name="input">UTF8 string to hash.</param>
        /// <returns>Byte array representing the leaf hash.</returns>
        byte[] HashLeaf(string input);

        /// <summary>
        /// Hash a branch node from two child nodes.
        /// </summary>
        /// <param name="left">Left child hash.</param>
        /// <param name="right">Right child hash.</param>
        /// <returns>Byte array representing the parent hash.</returns>
        byte[] HashBranch(byte[] left, byte[] right);
    }
}