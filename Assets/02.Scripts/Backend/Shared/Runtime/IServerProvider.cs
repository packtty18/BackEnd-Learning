using Backend.Shared.Common;
using Backend.Shared.Initialization;

namespace Backend.Shared.Runtime
{
    public interface IServerProvider
    {
        EServerProviderType ProviderType { get; }

        IServerInitializer Initializer { get; }
    }
}