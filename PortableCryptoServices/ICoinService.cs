namespace PortableCryptoServices
{
    public interface ICoinService<T> where T: class
    {
        T GetById(string id);
    }
}