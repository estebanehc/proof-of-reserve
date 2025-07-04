namespace ProofOfReserveAPI.Models
{
    /// <summary>
    /// Represents a Merkle proof for a user's balance.
    /// </summary>
    public sealed class MerkleProofResult
    {
        public required string UserBalance { get; init; }

        /// <summary>
        /// Each item is (hexHash, direction: 0=left, 1=right)
        /// </summary>
        public required List<(string Hash, int Direction)> ProofPath { get; init; }
    }
}