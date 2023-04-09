using System.Net;

namespace Common.Base.Exceptions
{
    public abstract class BaseException<T> : Exception where T : BaseExceptionModel
    {
        #region Ctors

        public BaseException(string message)
        {
            Errors.Add(message);
        }

        public BaseException(List<string> errors)
        {
            Errors = errors ?? new();
        }

        #endregion

        #region Props

        public List<string> Errors { get; } = new();
        public abstract HttpStatusCode StatusCode { get; }
        public abstract T ErrorModel { get; }

        #endregion
    }
}
