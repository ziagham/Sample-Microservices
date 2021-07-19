namespace CustomerApi.Domain.SeekWork
{
    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }
}