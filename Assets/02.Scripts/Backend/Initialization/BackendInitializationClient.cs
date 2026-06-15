using System.Threading;
using BackEnd;
using Cysharp.Threading.Tasks;

namespace BackEndLearning.Backend.Initialization
{
    public class BackendInitializationClient : IBackendInitialize
    {
        private readonly BackendInitializationResultMapper _resultMapper;
        private readonly bool _useAsyncInitialize;

        public BackendInitializationClient(BackendInitializationResultMapper resultMapper, bool useAsyncInitialize)
        {
            _resultMapper = resultMapper;
            _useAsyncInitialize = useAsyncInitialize;
        }

        public async UniTask<BackendInitializationResult> InitializeAsync(CancellationToken cancellationToken)
        {
            BackendReturnObject response = _useAsyncInitialize
                ? await InitializeWithCallbackAsync(cancellationToken)
                : await InitializeOnMainThreadAsync(cancellationToken);

            return _resultMapper.Map(response);
        }

        private async UniTask<BackendReturnObject> InitializeOnMainThreadAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await UniTask.SwitchToMainThread(cancellationToken);
            return global::BackEnd.Backend.Initialize();
        }

        private async UniTask<BackendReturnObject> InitializeWithCallbackAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            UniTaskCompletionSource<BackendReturnObject> completionSource = new UniTaskCompletionSource<BackendReturnObject>();
            global::BackEnd.Backend.InitializeAsync(response => completionSource.TrySetResult(response));

            return await completionSource.Task.AttachExternalCancellation(cancellationToken);
        }
    }
}
