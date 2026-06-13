// Assets/02.Scripts/Backend/01.ServerShared/Runtime/ServerContext.cs

using Backend.Shared.Common;
using Backend.Shared.Initialization;

namespace Backend.Shared.Runtime
{
    public class ServerContext
    {
        public EServerProviderType ProviderType { get; }
        public IServerInitializer Initializer { get; }

        public bool IsValid
        {
            get
            {
                return ProviderType != EServerProviderType.None && Initializer != null;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return Initializer != null && Initializer.IsInitialized;
            }
        }

        public ServerContext(EServerProviderType providerType, IServerInitializer initializer)
        {
            ProviderType = providerType;
            Initializer = initializer;
        }
    }
}