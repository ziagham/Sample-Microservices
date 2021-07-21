namespace OrderApi.Domain.SeekWork
{
    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }
}