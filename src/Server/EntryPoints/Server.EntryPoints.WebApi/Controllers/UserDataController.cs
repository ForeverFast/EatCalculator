using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Core.Interfaces.Services;
using Server.Core.Models.Api.UserData.Requests;
using Server.Core.Models.Api.UserData.Responses;

namespace Server.EntryPoints.WebApi.Controllers
{
    [ApiController]
    [Route("api/user-data")]
    public class UserDataController : ControllerBase
    {
        #region Injects

        private readonly IUserDataService _userDataService;

        #endregion

        #region Ctors

        public UserDataController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        #endregion

        [Authorize]
        [HttpGet, Route("check-updates")]
        public ValueTask<CheckUpdatesResponse> CheckUpdates(CheckUpdatesRequest request, CancellationToken ctn)
            => _userDataService.CheckUpdatesAsync(request, ctn);

        [Authorize]
        [HttpGet, Route("load-user-eat-data")]
        public async Task<LoadUserEatDataResponse> LoadUserEatData(LoadUserEatDataRequest request, CancellationToken ctn)
            => await _userDataService.LoadUserEatDataAsync(request, ctn);

        [Authorize]
        [HttpPost, Route("upload-user-eat-data")]
        public async Task<UploadUserEatDataResponse> UploadUserEatData(UploadUserEatDataRequest request, CancellationToken ctn)
            => await _userDataService.UploadUserEatDataAsync(request, ctn);
    }
}
