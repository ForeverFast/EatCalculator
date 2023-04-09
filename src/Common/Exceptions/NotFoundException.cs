using Common.Base.Exceptions;
using System.Net;

namespace Common.Exceptions
{
    public class NotFoundException : BaseException<NotFoundExceptionModel>
    {
        #region Ctors

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(List<string> errors) : base(errors)
        {
        }

        #endregion

        #region Props

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public override NotFoundExceptionModel ErrorModel => new()
        {
            Errors = Errors
        };

        #endregion
    }

    public record NotFoundExceptionModel : BaseExceptionModel
    {
    }
}
