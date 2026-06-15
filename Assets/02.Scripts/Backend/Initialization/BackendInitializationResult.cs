using System;

namespace BackEndLearning.Backend.Initialization
{
    public class BackendInitializationResult
    {
        public BackendInitializationResult(
            EBackendInitializationState state,
            bool isSuccess,
            string statusCode,
            string errorCode,
            string message,
            string rawResponse,
            DateTime recordedAt)
        {
            State = state;
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Message = message;
            RawResponse = rawResponse;
            RecordedAt = recordedAt;
        }

        public EBackendInitializationState State { get; }
        public bool IsSuccess { get; }
        public string StatusCode { get; }
        public string ErrorCode { get; }
        public string Message { get; }
        public string RawResponse { get; }
        public DateTime RecordedAt { get; }

        public static BackendInitializationResult CreateRunning(DateTime recordedAt)
        {
            return new BackendInitializationResult(
                EBackendInitializationState.Running,
                false,
                "Running",
                string.Empty,
                "뒤끝 SDK 초기화를 실행 중입니다.",
                string.Empty,
                recordedAt);
        }

        public static BackendInitializationResult CreateCanceled(DateTime recordedAt)
        {
            return new BackendInitializationResult(
                EBackendInitializationState.Canceled,
                false,
                "Canceled",
                "OperationCanceled",
                "뒤끝 SDK 초기화 대기가 취소되었습니다.",
                string.Empty,
                recordedAt);
        }
    }
}
