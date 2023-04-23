using Server.Core.Models.Api.Identity.Requests;
using Server.Core.Models.Api.Identity.Responses;

namespace Server.Core.Interfaces.Services
{
    public interface IAccountService
    {
        ValueTask<IResult<SignInResponse>> SignInAsync(SignInRequest request, CancellationToken ctn);
        ValueTask<IResult<SignUpResponse>> SignUpAsync(SignUpRequest request, CancellationToken ctn);
    }
}
