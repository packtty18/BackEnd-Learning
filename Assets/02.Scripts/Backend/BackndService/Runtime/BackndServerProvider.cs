using Backend.BackndService.Initialization;
using Backend.Shared.Common;
using Backend.Shared.Initialization;
using Backend.Shared.Runtime;
using UnityEngine;

namespace Backend.BackndService.Runtime
{
    public class BackndServerProvider : MonoBehaviour, IServerProvider
    {
        private IServerInitializer _initializer;

        public EServerProviderType ProviderType
        {
            get
            {
                return EServerProviderType.Backnd;
            }
        }

        public IServerInitializer Initializer
        {
            get
            {
                if (_initializer == null)
                {
                    _initializer = new BackndServerInitializer();
                }

                return _initializer;
            }
        }
    }
}
