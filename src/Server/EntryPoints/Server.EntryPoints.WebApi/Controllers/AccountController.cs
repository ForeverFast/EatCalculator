using Common.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Core.Interfaces.Services;
using Server.Core.Models.Api.Identity.Requests;
using Server.Core.Models.Api.Identity.Responses;

namespace Server.EntryPoints.WebApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        #region Injects

        private readonly IAccountService _accountService;

        #endregion

        #region Ctors

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion

        [AllowAnonymous]
        [HttpPost, Route("sign-in")]
        public ValueTask<IResult<SignInResponse>> SignIn(SignInRequest request, CancellationToken ctn)
            => _accountService.SignInAsync(request, ctn);

        [AllowAnonymous]
        [HttpPost, Route("sign-up")]
        public ValueTask<IResult<SignUpResponse>> SignUp(SignUpRequest request, CancellationToken ctn)
            => _accountService.SignUpAsync(request, ctn);
    }
}
