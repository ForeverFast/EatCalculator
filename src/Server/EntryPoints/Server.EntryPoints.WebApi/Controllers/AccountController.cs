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
        [HttpPost, Route("sing-in")]
        public ValueTask<IResult<SignInResponse>> SingIn(SignInRequest request, CancellationToken ctn)
            => _accountService.SingInAsync(request, ctn);

        [AllowAnonymous]
        [HttpPost, Route("sing-up")]
        public ValueTask<IResult<SignUpResponse>> SingUp(SignUpRequest request, CancellationToken ctn)
            => _accountService.SingUpAsync(request, ctn);
    }
}
