using System;

namespace Backend.Shared.Common
{
    public class ServerError
    {
        public EServerErrorCode Code { get; }
        public string Message { get; }
        public string Detail { get; }

        public bool HasError
        {
            get
            {
                return Code != EServerErrorCode.None;
            }
        }

        private ServerError(EServerErrorCode code, string message, string detail)
        {
            Code = code;
            Message = string.IsNullOrWhiteSpace(message) ? "Server request failed." : message;
            Detail = detail ?? string.Empty;
        }

        public static ServerError None()
        {
            return new ServerError(EServerErrorCode.None, string.Empty, string.Empty);
        }

        public static ServerError Create(EServerErrorCode code, string message)
        {
            return new ServerError(code, message, string.Empty);
        }

        public static ServerError Create(EServerErrorCode code, string message, string detail)
        {
            return new ServerError(code, message, detail);
        }

        public static ServerError FromException(Exception exception)
        {
            if (exception == null)
            {
                return Create(EServerErrorCode.Unknown, "Unknown exception occurred.");
            }

            return Create(EServerErrorCode.Unknown, exception.Message, exception.ToString());
        }
    }
}
