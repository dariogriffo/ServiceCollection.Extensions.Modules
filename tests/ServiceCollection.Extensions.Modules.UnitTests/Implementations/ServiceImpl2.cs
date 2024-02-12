namespace ServiceCollection.Extensions.Modules.UnitTests.Implementations
{
    using ServiceCollection.Extensions.Modules.UnitTests.Interfaces;

    public class ServiceImpl2 : IService
    {
        public string Message() => "2";
    }
}