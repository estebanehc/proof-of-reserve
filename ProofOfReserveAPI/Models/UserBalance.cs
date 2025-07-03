namespace ProofOfReserveAPI.Models
{
    public readonly record struct UserBalance(int UserId, int Balance)
    {
        public override string ToString() => $"({UserId},{Balance})";
    }
}
