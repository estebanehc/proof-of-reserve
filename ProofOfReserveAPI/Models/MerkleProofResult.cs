namespace ProofOfReserveAPI.Models
{
    /// <summary>
    /// Represents a Merkle proof for a user's balance.
    /// </summary>
    public sealed class MerkleProofResult
    {
        public required string UserBalance { get; init; }

        /// <summary>
        /// Ordered list of proof steps from the user's leaf to the root.
        /// </summary>
        public required List<ProofNodeDto> ProofPath { get; init; }
    }
}