namespace PaymentSystem.Domain.Interface
{
    public interface IHashValidation
    {
        string Hash(string info);
        bool Verify(string infoHash, string hash);
    }
}
