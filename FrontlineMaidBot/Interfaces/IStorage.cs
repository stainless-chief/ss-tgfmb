namespace FrontlineMaidBot.Interfaces
{
    public interface IStorage
    {
        T Load<T>(string path);
    }
}
