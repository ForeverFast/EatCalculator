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
        public async Task<SignInResponse> SingIn(SignInRequest request, CancellationToken ctn)
            => await _accountService.SingInAsync(request, ctn);

        [AllowAnonymous]
        [HttpPost, Route("sing-up")]
        public async Task<SignUpResponse> SingUp(SignUpRequest request, CancellationToken ctn)
            => await _accountService.SingUpAsync(request, ctn);
    }
}
