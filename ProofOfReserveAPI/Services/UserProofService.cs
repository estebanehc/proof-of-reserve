using MerkleLibrary.Hashing;
using MerkleLibrary.Tree;
using ProofOfReserveAPI.Models;

namespace ProofOfReserveAPI.Services
{
    public class UserProofService : IUserProofService
    {
        private readonly List<UserBalance> _userBalances;
        private readonly IMerkleHasher _hasher;
        private readonly MerkleTree<string> _tree;
        private readonly List<string> _serializedLeaves;
        private readonly Dictionary<int, string> _idToSerialized;

        public UserProofService()
        {
            _userBalances =
            [
                new(1, 1111), new(2, 2222), new(3, 3333), new(4, 4444),
                new(5, 5555), new(6, 6666), new(7, 7777), new(8, 8888)
            ];

            _serializedLeaves = [.. _userBalances.Select(ub => ub.ToString())];
            _idToSerialized = _userBalances.ToDictionary(u => u.UserId, u => u.ToString());

            _hasher = new TaggedHasher("ProofOfReserve_Leaf", "ProofOfReserve_Branch");
            _tree = new MerkleTree<string>(_hasher);
        }

        public string GetMerkleRootHex()
        {
            var root = _tree.ComputeRoot(_serializedLeaves);
            return ConvertToHex(root);
        }

        public MerkleProofResult? GetProofForUser(int userId)
    {
        if (!_idToSerialized.TryGetValue(userId, out var leafValue))
            return null;

        var path = _tree.GetMerkleProof(_serializedLeaves, leafValue);
        return new MerkleProofResult
        {
            UserBalance = leafValue,
            ProofPath = path.Select(p => (ConvertToHex(p.Hash), p.Direction)).ToList()
        };
    }

    private static string ConvertToHex(byte[] hash) =>
        BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}
