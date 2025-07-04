using MerkleLibrary.Hashing;
using ProofOfReserveAPI.Services;

namespace ProofOfReserveTests
{
    public class UserProofTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void MerkleProof_Should_Reconstruct_ValidRoot(int userId)
        {
            // Arrange
            var proofService = new UserProofService();
            var proof = proofService.GetProofForUser(userId)!;
            var expectedRoot = proofService.GetMerkleRootHex();

            var hasher = new TaggedHasher("ProofOfReserve_Leaf", "ProofOfReserve_Branch");

            // Act:
            var current = hasher.HashLeaf(proof.UserBalance);

            foreach (var node in proof.ProofPath)
            {
                var sibling = Convert.FromHexString(node.Hash);
                current = node.Direction == 0
                    ? hasher.HashBranch(sibling, current)
                    : hasher.HashBranch(current, sibling);
            }

            var computedRoot = BitConverter.ToString(current).Replace("-", "").ToLowerInvariant();

            // Assert
            Assert.Equal(expectedRoot, computedRoot);
        }
    }
}
