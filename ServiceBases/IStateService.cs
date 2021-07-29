namespace L.ServiceBases
{
    public interface IStateService : IService
    {
        bool IsRunning { get; }
    }
}