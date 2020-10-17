namespace SampleAppV1.Services
{
    public interface IFactory<T>
    {
        T Get();
    }
}
