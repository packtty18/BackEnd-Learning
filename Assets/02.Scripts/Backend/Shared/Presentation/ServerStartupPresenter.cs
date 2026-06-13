using Backend.Shared.Common;
using UnityEngine;

namespace Backend.Shared.Presentation
{
    public class ServerStartupPresenter : MonoBehaviour
    {
        public void PresentBindResult(ServerResult result)
        {
            if (result == null)
            {
                Debug.LogError("Server bind result is null.");
                return;
            }

            if (result.IsSuccess)
            {
                Debug.Log("Server provider binding succeeded.");
                return;
            }

            PresentFailure("Server provider binding failed.", result.Error);
        }

        public void PresentInitializeResult(ServerResult result)
        {
            if (result == null)
            {
                Debug.LogError("Server initialize result is null.");
                return;
            }

            if (result.IsSuccess)
            {
                Debug.Log("Server initialization succeeded.");
                return;
            }

            PresentFailure("Server initialization failed.", result.Error);
        }

        private void PresentFailure(string title, ServerError error)
        {
            if (error == null)
            {
                Debug.LogError(title);
                return;
            }

            Debug.LogError($"{title} Code: {error.Code}, Message: {error.Message}, Detail: {error.Detail}");
        }
    }
}
