using System;
using System.Threading;
using BackEndLearning.Backend.Initialization;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackEndLearning.Backend
{
    public class BackendManager : MonoBehaviour
    {
        [Title("뒤끝 초기화 설정")]
        [SerializeField]
        private bool _initializeOnStart = true;

        [SerializeField]
        private bool _useAsyncInitialize = true;

        [Title("뒤끝 초기화 결과")]
        [SerializeField]
        [InlineProperty]
        [HideLabel]
        private BackendInitializationSnapshot _snapshot = new BackendInitializationSnapshot();

        private BackendInitializationUseCase _initializationUseCase;
        private BackendInitializationLogger _logger;
        private CancellationTokenSource _runningCancellationTokenSource;

        public EBackendInitializationState State => _snapshot.State;

        private void Awake()
        {
            ComposeDependencies();
        }

        private void Start()
        {
            if (_initializeOnStart == false)
            {
                return;
            }

            InitializeBackend();
        }

        private void OnDestroy()
        {
            CancelRunningInitialization();
        }

        [Button("뒤끝 초기화 실행", ButtonSizes.Large)]
        [GUIColor(0.2f, 0.8f, 0.4f)]
        [ContextMenu("뒤끝 초기화 실행")]
        public void InitializeBackend()
        {
            RunInitializeBackendAsync().Forget();
        }

        [Button("초기화 취소", ButtonSizes.Medium)]
        [GUIColor(1f, 0.65f, 0.2f)]
        [ContextMenu("초기화 취소")]
        public void CancelRunningInitialization()
        {
            if (_runningCancellationTokenSource == null)
            {
                return;
            }

            _runningCancellationTokenSource.Cancel();
            _runningCancellationTokenSource.Dispose();
            _runningCancellationTokenSource = null;
        }

        private async UniTaskVoid RunInitializeBackendAsync()
        {
            if (_snapshot.State == EBackendInitializationState.Running)
            {
                Debug.LogWarning("뒤끝 SDK 초기화가 이미 실행 중입니다.");
                return;
            }

            StartInitialization();

            try
            {
                BackendInitializationResult result = await _initializationUseCase.InitializeAsync(_runningCancellationTokenSource.Token);
                ApplyResult(result);
            }
            catch (OperationCanceledException)
            {
                ApplyResult(_initializationUseCase.CreateCanceledResult());
            }
            finally
            {
                DisposeRunningCancellationTokenSource();
            }
        }

        private void ComposeDependencies()
        {
            Func<DateTime> nowProvider = () => DateTime.Now;
            BackendInitializationResultMapper resultMapper = new BackendInitializationResultMapper(nowProvider);
            IBackendInitialize initialize = new BackendInitializationClient(resultMapper, _useAsyncInitialize);

            _initializationUseCase = new BackendInitializationUseCase(initialize, nowProvider);
            _logger = new BackendInitializationLogger();
        }

        private void StartInitialization()
        {
            _runningCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
            ApplyResult(_initializationUseCase.CreateRunningResult());
        }

        private void ApplyResult(BackendInitializationResult result)
        {
            _snapshot.Apply(result);
            _logger.Log(result, result.Message);
        }

        private void DisposeRunningCancellationTokenSource()
        {
            if (_runningCancellationTokenSource == null)
            {
                return;
            }

            _runningCancellationTokenSource.Dispose();
            _runningCancellationTokenSource = null;
        }
    }
}
