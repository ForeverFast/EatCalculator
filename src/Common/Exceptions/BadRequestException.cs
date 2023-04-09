using Common.Base.Exceptions;
using System.Net;

namespace Common.Exceptions
{
    public class BadRequestException : BaseException<BadRequestExceptionModel>
    {
        #region Ctors

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(List<string> errors) : base(errors)
        {
        }

        #endregion

        #region Props

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public override BadRequestExceptionModel ErrorModel => new()
        {
            Errors = Errors,
        };

        #endregion
    }

    public record BadRequestExceptionModel : BaseExceptionModel
    {
    }
}
