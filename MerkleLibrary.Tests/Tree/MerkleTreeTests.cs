using MerkleLibrary.Tree;
using MerkleLibrary.Hashing;

namespace MerkleLibrary.Tests.Tree
{
    public class MerkleTreeTests
    {
        [Fact]
        public void Should_ComputeMerkleRoot_From_Five_Strings()
        {
            // Arrange
            var items = new[] { "aaa", "bbb", "ccc", "ddd", "eee" };
            var hasher = new TaggedHasher("Bitcoin_Transaction", "Bitcoin_Transaction");
            var tree = new MerkleTree<string>(hasher);

            // Act
            var rootHash = tree.ComputeRoot(items);
            var hex = ByteArrayToHex(rootHash);

            // Output
            System.Diagnostics.Trace.WriteLine($"Merkle Root: {hex}");

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(hex));
            Assert.Matches("^[0-9a-f]{64}$", hex);
            Assert.Equal("4aa906745f72053498ecc74f79813370a4fe04f85e09421df2d5ef760dfa94b5", hex);
        }

        private static string ByteArrayToHex(byte[] bytes)
        {
            return Convert.ToHexStringLower(bytes);
        }
    }
}
