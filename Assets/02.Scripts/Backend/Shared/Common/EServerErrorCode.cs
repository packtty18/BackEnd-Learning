using UnityEngine;


namespace Backend.Shared.Common
{
    public enum EServerErrorCode
    {
        None = 0,
        Unknown = 1,
        NotInitialized = 2,
        AlreadyInitialized = 3,
        NetworkUnavailable = 4,
        InvalidConfiguration = 5,
        AuthenticationFailed = 6,
        PermissionDenied = 7,
        Timeout = 8,
        Canceled = 9,
        ProviderNotSupported = 10
    }
}
