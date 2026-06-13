namespace Backend.Shared.Common
{
    public class ServerResult
    {
        public bool IsSuccess { get; }
        public ServerError Error { get; }

        public bool IsFailure
        {
            get
            {
                return !IsSuccess;
            }
        }

        private ServerResult(bool isSuccess, ServerError error)
        {
            IsSuccess = isSuccess;
            Error = error ?? ServerError.None();
        }

        public static ServerResult Success()
        {
            return new ServerResult(true, ServerError.None());
        }

        public static ServerResult Failure(ServerError error)
        {
            return new ServerResult(false, error);
        }

        public static ServerResult Failure(EServerErrorCode code, string message)
        {
            return new ServerResult(false, ServerError.Create(code, message));
        }
    }
}

namespace Backend.Shared.Common
{
    public class ServerResult<TValue>
    {
        public bool IsSuccess { get; }
        public TValue Value { get; }
        public ServerError Error { get; }

        public bool IsFailure
        {
            get
            {
                return !IsSuccess;
            }
        }

        private ServerResult(bool isSuccess, TValue value, ServerError error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error ?? ServerError.None();
        }

        public static ServerResult<TValue> Success(TValue value)
        {
            return new ServerResult<TValue>(true, value, ServerError.None());
        }

        public static ServerResult<TValue> Failure(ServerError error)
        {
            return new ServerResult<TValue>(false, default, error);
        }

        public static ServerResult<TValue> Failure(EServerErrorCode code, string message)
        {
            return new ServerResult<TValue>(false, default, ServerError.Create(code, message));
        }
    }
}