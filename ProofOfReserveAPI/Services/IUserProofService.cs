using ProofOfReserveAPI.Models;

namespace ProofOfReserveAPI.Services
{
    public interface IUserProofService
    {
        /// <summary>
        /// Returns the Merkle Root from the current in-memory user balances.
        /// </summary>
        string GetMerkleRootHex();

        /// <summary>
        /// Returns the Merkle Proof path and serialized user balance.
        /// </summary>
        MerkleProofResult? GetProofForUser(int userId);
    }
}