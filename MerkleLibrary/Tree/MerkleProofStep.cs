namespace MerkleLibrary.Tree
{
    /// <summary>
    /// Represents a single node in the Merkle proof path.
    /// </summary>
    public readonly record struct MerkleProofStep(byte[] Hash, int Direction);
}
