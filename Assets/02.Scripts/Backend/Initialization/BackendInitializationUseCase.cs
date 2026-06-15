using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace BackEndLearning.Backend.Initialization
{
    public class BackendInitializationUseCase
    {
        private readonly IBackendInitialize _initialize;
        private readonly Func<DateTime> _nowProvider;

        public BackendInitializationUseCase(IBackendInitialize initialize, Func<DateTime> nowProvider)
        {
            _initialize = initialize;
            _nowProvider = nowProvider;
        }

        public BackendInitializationResult CreateRunningResult()
        {
            return BackendInitializationResult.CreateRunning(_nowProvider());
        }

        public BackendInitializationResult CreateCanceledResult()
        {
            return BackendInitializationResult.CreateCanceled(_nowProvider());
        }

        public async UniTask<BackendInitializationResult> InitializeAsync(CancellationToken cancellationToken)
        {
            return await _initialize.InitializeAsync(cancellationToken);
        }
    }
}
