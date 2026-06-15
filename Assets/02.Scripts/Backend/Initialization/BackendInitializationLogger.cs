using UnityEngine;

namespace BackEndLearning.Backend.Initialization
{
    public class BackendInitializationLogger
    {
        public void Log(BackendInitializationResult result, string message)
        {
            switch (result.State)
            {
                case EBackendInitializationState.Succeeded:
                    Debug.Log(message);
                    break;
                case EBackendInitializationState.Failed:
                    Debug.LogError(message);
                    break;
                case EBackendInitializationState.Canceled:
                    Debug.LogWarning(message);
                    break;
                default:
                    Debug.Log(message);
                    break;
            }
        }
    }
}
