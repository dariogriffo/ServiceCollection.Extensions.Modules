using ServiceCollection.Extensions.Modules.UnitTests.Interfaces;

namespace ServiceCollection.Extensions.Modules.UnitTests.Implementations;

public class ServiceImpl3 : IService
{
    public string Message() => "3";
}