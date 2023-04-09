using Common.Base.Exceptions;
using System.Net;

namespace Common.Exceptions
{
    public class InternalServerErrorException : BaseException<InternalServerErrorExceptionModel>
    {
        #region Ctors

        public InternalServerErrorException(string message) : base(message)
        {
        }

        public InternalServerErrorException(List<string> errors) : base(errors)
        {
        }

        #endregion

        #region Props

        public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

        public override InternalServerErrorExceptionModel ErrorModel => new()
        {
            Errors = Errors
        };

        #endregion
    }

    public record InternalServerErrorExceptionModel : BaseExceptionModel
    {
    }
}
