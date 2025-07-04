namespace ProofOfReserveAPI.Models
{
    /// <summary>
    /// Represents a single step in the Merkle proof path.
    /// </summary>
    public sealed class ProofNodeDto
    {
        public required string Hash { get; init; }     // Hex string
        public required int Direction { get; init; }   // 0 = left, 1 = right
    }
}
