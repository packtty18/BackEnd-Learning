using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackEndLearning.Backend.Initialization
{
    [Serializable]
    public class BackendInitializationSnapshot
    {
        [SerializeField]
        [ReadOnly]
        private EBackendInitializationState _state = EBackendInitializationState.NotStarted;

        [SerializeField]
        [ReadOnly]
        private bool _isSuccess;

        [SerializeField]
        [ReadOnly]
        private string _statusCode = string.Empty;

        [SerializeField]
        [ReadOnly]
        private string _errorCode = string.Empty;

        [SerializeField]
        [ReadOnly]
        private string _message = "아직 뒤끝 SDK 초기화를 실행하지 않았습니다.";

        [SerializeField]
        [ReadOnly]
        [MultiLineProperty(4)]
        private string _rawResponse = string.Empty;

        [SerializeField]
        [ReadOnly]
        private string _recordedAt = string.Empty;

        public EBackendInitializationState State => _state;

        public void Apply(BackendInitializationResult result)
        {
            _state = result.State;
            _isSuccess = result.IsSuccess;
            _statusCode = result.StatusCode;
            _errorCode = result.ErrorCode;
            _message = result.Message;
            _rawResponse = result.RawResponse;
            _recordedAt = result.RecordedAt.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
