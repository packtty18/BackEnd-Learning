using System;
using System.Threading;
using Backend.Shared.Common;
using Cysharp.Threading.Tasks;

namespace Backend.Shared.Runtime
{
    public class ServerManager
    {
        public ServerContext Context { get; private set; }

        public bool HasContext
        {
            get
            {
                return Context != null && Context.IsValid;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return Context != null && Context.IsInitialized;
            }
        }

        public ServerResult BindProvider(IServerProvider provider)
        {
            if (provider == null)
            {
                return ServerResult.Failure(
                    EServerErrorCode.InvalidConfiguration,
                    "Server provider is null.");
            }

            if (provider.ProviderType == EServerProviderType.None)
            {
                return ServerResult.Failure(
                    EServerErrorCode.InvalidConfiguration,
                    "Server provider type is none.");
            }

            if (provider.Initializer == null)
            {
                return ServerResult.Failure(
                    EServerErrorCode.InvalidConfiguration,
                    "Server provider initializer is null.");
            }

            Context = new ServerContext(provider.ProviderType, provider.Initializer);
            return ServerResult.Success();
        }

        public async UniTask<ServerResult> InitializeAsync(CancellationToken cancellationToken)
        {
            if (!HasContext)
            {
                return ServerResult.Failure(
                    EServerErrorCode.InvalidConfiguration,
                    "Server context is not ready.");
            }

            if (IsInitialized)
            {
                return ServerResult.Failure(
                    EServerErrorCode.AlreadyInitialized,
                    "Server is already initialized.");
            }

            try
            {
                return await Context.Initializer.InitializeAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return ServerResult.Failure(
                    EServerErrorCode.Canceled,
                    "Server initialization was canceled.");
            }
            catch (Exception exception)
            {
                return ServerResult.Failure(ServerError.FromException(exception));
            }
        }
    }
}
