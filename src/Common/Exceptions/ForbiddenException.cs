using Common.Base.Exceptions;
using System.Net;

namespace Common.Exceptions
{
    public class ForbiddenException : BaseException<ForbiddenExceptionModel>
    {
        #region Ctors

        public ForbiddenException(string message) : base(message)
        {
        }

        public ForbiddenException(List<string> errors) : base(errors)
        {
        }

        #endregion

        #region Props

        public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

        public override ForbiddenExceptionModel ErrorModel => new()
        {
            Errors = Errors
        };

        #endregion
    }

    public record ForbiddenExceptionModel : BaseExceptionModel
    {
    }
}
