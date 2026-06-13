using System;
using System.Threading;
using BackEnd;
using Backend.Shared.Common;
using Backend.Shared.Initialization;
using Cysharp.Threading.Tasks;

namespace Backend.BackndService.Initialization
{
    public class BackndServerInitializer : IServerInitializer
    {
        public EServerInitializeState State { get; private set; }

        public bool IsInitialized
        {
            get
            {
                return State == EServerInitializeState.Initialized;
            }
        }

        public async UniTask<ServerResult> InitializeAsync(CancellationToken cancellationToken)
        {
            if (State == EServerInitializeState.Initialized)
            {
                return ServerResult.Success();
            }

            if (State == EServerInitializeState.Initializing)
            {
                return ServerResult.Failure(
                    EServerErrorCode.AlreadyInitialized,
                    "Backnd server initialization is already running.");
            }

            State = EServerInitializeState.Initializing;

            try
            {
                await UniTask.SwitchToMainThread(cancellationToken);

                BackendReturnObject result = global::BackEnd.Backend.Initialize();

                if (result.IsSuccess())
                {
                    State = EServerInitializeState.Initialized;
                    return ServerResult.Success();
                }

                State = EServerInitializeState.Failed;
                return ServerResult.Failure(
                    EServerErrorCode.InvalidConfiguration,
                    result.ToString());
            }
            catch (OperationCanceledException)
            {
                State = EServerInitializeState.Failed;
                return ServerResult.Failure(
                    EServerErrorCode.Canceled,
                    "Backnd server initialization was canceled.");
            }
            catch (Exception exception)
            {
                State = EServerInitializeState.Failed;
                return ServerResult.Failure(ServerError.FromException(exception));
            }
        }
    }
}
