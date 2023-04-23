﻿using Client.Core.Entities.Viewer.Models.Store;
using Client.Core.Shared.Configs;
using Client.Core.Shared.Lib;
using Client.Core.Shared.Lib.BaseComponents;

namespace Client.Core.Pages.Identity
{
    [Route($"{Routes.Identity.BasePath}/{Routes.Identity.UserProfile}")]
    public partial class UserProfilePage : BasePageComponent
    {
        #region Injects

        [Inject] ViewerStateFacade _viewerStateFacade { get; init; } = null!;

        #endregion

        #region Selectors

        
        #endregion

        #region UI Fields

        private LoadingState _loadingState = LoadingState.Loading;

        #endregion

        #region LC Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (_viewerStateFacade.State.Value.Viewer != null)
            {
                _navigationManager.NavigateToIndexPage();
                return;
            }

            _loadingState = LoadingState.Content;
        }

        #endregion
    }
}
