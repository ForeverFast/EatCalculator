using Common.Base.Exceptions;
using System.Net;

namespace Common.Exceptions
{
    public class UnauthorizedException : BaseException<UnauthorizedExceptionModel>
    {
        #region Ctors

        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException(List<string> errors) : base(errors)
        {
        }

        #endregion

        #region Props

        public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public override UnauthorizedExceptionModel ErrorModel => new()
        {
            Errors = Errors
        };

        #endregion
    }

    public record UnauthorizedExceptionModel : BaseExceptionModel
    {
    }
}
