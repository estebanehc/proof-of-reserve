namespace ProofOfReserveAPI.Models
{
    public sealed class MerkleProofResult
    {
        public required string UserBalance { get; init; }
        public required List<(string Hash, int Direction)> ProofPath { get; init; }
    }
}