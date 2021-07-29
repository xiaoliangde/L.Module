namespace L.ServiceBases
{
    public interface IService
    {
        string Name { get; set; }
        void Open();
        void Close();
    }
}