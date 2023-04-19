using Server.Core.Models.Api.Identity.Requests;
using Server.Core.Models.Api.Identity.Responses;

namespace Server.Core.Interfaces.Services
{
    public interface IAccountService
    {
        ValueTask<IResult<SignInResponse>> SingInAsync(SignInRequest request, CancellationToken ctn);
        ValueTask<IResult<SignUpResponse>> SingUpAsync(SignUpRequest request, CancellationToken ctn);
    }
}
