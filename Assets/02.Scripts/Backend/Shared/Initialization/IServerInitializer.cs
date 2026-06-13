using System.Threading;
using Backend.Shared.Common;
using Cysharp.Threading.Tasks;

namespace Backend.Shared.Initialization
{
    public interface IServerInitializer
    {
        EServerInitializeState State { get; }
        bool IsInitialized { get; }
        
        UniTask<ServerResult> InitializeAsync(CancellationToken cancellationToken);
    }
}