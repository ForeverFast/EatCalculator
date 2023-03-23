﻿using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace EatCalculator.UI.App
{
    public partial class ClientAppLayout : FluxorLayout
    {
        #region Injects

        [Inject] NavigationManager _navigationManager { get; init; } = null!;

        #endregion

        #region UI Fields

        private bool _drawerOpened = false;

        #endregion

        #region Css/Styles

        private string _drawerCssName
            => new CssBuilder("client-app-layout__drawer")
            .AddClass("active", _drawerOpened)
            .Build();

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _navigationManager.LocationChanged += OnLocationChanged;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _navigationManager.LocationChanged -= OnLocationChanged;
        }

        #endregion

        #region External events

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            if (!_drawerOpened)
                return;

            _drawerOpened = false;

            StateHasChanged();
        }

        #endregion

        #region Internal events

        private void OnSwipe(SwipeDirection direction)
        {
            _ = direction switch
            {
                { } when _drawerOpened && direction == SwipeDirection.RightToLeft => _drawerOpened = false,
                { } when !_drawerOpened && direction == SwipeDirection.LeftToRight => _drawerOpened = true,
                _ => false,
            };

            StateHasChanged();
        }

        private void OnNavLinkClick()
            => _drawerOpened = false;

        #endregion
    }
}
