using System;
using BackEnd;

namespace BackEndLearning.Backend.Initialization
{
    public class BackendInitializationResultMapper
    {
        private readonly Func<DateTime> _nowProvider;

        public BackendInitializationResultMapper(Func<DateTime> nowProvider)
        {
            _nowProvider = nowProvider;
        }

        public BackendInitializationResult Map(BackendReturnObject response)
        {
            bool isSuccess = response.IsSuccess();
            EBackendInitializationState state = isSuccess
                ? EBackendInitializationState.Succeeded
                : EBackendInitializationState.Failed;

            return new BackendInitializationResult(
                state,
                isSuccess,
                response.GetStatusCode(),
                response.GetErrorCode(),
                response.GetMessage(),
                response.ToString(),
                _nowProvider());
        }
    }
}
