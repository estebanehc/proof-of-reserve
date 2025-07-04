namespace ProofOfReserveAPI.Models
{
    /// <summary>
    /// Represents a user's ID and balance in the system.
    /// </summary>
    public readonly record struct UserBalance(int UserId, int Balance)
    {
        /// <summary>
        /// Serializes the balance as a string tuple like \"(10,100)\".
        /// </summary>
        public override string ToString() => $"({UserId},{Balance})";
    }
}
