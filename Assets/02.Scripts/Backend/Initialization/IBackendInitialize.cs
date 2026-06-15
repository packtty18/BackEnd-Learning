using System.Threading;
using Cysharp.Threading.Tasks;

namespace BackEndLearning.Backend.Initialization
{
    public interface IBackendInitialize
    {
        UniTask<BackendInitializationResult> InitializeAsync(CancellationToken cancellationToken);
    }
}
