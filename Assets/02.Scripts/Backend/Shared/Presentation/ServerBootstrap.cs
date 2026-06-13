using System.Threading;
using Backend.Shared.Common;
using Backend.Shared.Runtime;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Backend.Shared.Presentation
{
    public class ServerBootstrap : MonoBehaviour
    {
        [Header("Server Provider")]
        [SerializeField] private MonoBehaviour _serverProviderBehaviour;

        [Header("Startup Options")]
        [SerializeField] private bool _shouldInitializeOnStart = true;
        [SerializeField] private bool _shouldLogResult = true;

        [Header("Presenter")]
        [SerializeField] private ServerStartupPresenter _presenter;

        private ServerManager _serverManager;
        private ServerStartupOptions _startupOptions;

        public ServerManager ServerManager
        {
            get
            {
                return _serverManager;
            }
        }

        private void Awake()
        {
            _serverManager = new ServerManager();
            _startupOptions = new ServerStartupOptions(_shouldInitializeOnStart, _shouldLogResult);
        }

        private void Start()
        {
            if (!_startupOptions.ShouldInitializeOnStart)
            {
                return;
            }

            CancellationToken cancellationToken = this.GetCancellationTokenOnDestroy();
            StartServerAsync(cancellationToken).Forget();
        }

        private async UniTask StartServerAsync(CancellationToken cancellationToken)
        {
            IServerProvider provider = _serverProviderBehaviour as IServerProvider;
            ServerResult bindResult = _serverManager.BindProvider(provider);

            if (_startupOptions.ShouldLogResult && _presenter != null)
            {
                _presenter.PresentBindResult(bindResult);
            }

            if (bindResult.IsFailure)
            {
                return;
            }

            ServerResult initializeResult = await _serverManager.InitializeAsync(cancellationToken);

            if (_startupOptions.ShouldLogResult && _presenter != null)
            {
                _presenter.PresentInitializeResult(initializeResult);
            }
        }
    }
}
