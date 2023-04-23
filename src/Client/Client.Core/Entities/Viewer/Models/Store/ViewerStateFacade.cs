using Client.Core.Entities.Viewer.Models.Store.Actions;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Core.Entities.Viewer.Models.Store
{
    internal sealed class ViewerStateFacade : StateFacade<ViewerState>
    {
        #region Ctors

        public ViewerStateFacade(IStore store, IDispatcher dispatcher) : base(store, dispatcher)
        {
        }

        #endregion

        #region Selectors

        protected override ISelector<ViewerState> SelectState
             => ViewerStateSelectors.SelectFeatureState;



        #endregion

        public void InitializeViewer(Task<AuthenticationState> authenticationStateTask)
            => _dispatcher.Dispatch(new InitializeViewerAction
            {

                AuthenticationStateTask = authenticationStateTask,
            });

        public void SignIn(string login, string password)
            => _dispatcher.Dispatch(new SignInAction
            {
                Login = login,
                Password = password
            });

        public void SignUp(string userName, string email, string password)
            => _dispatcher.Dispatch(new SignUpAction
            {
                UserName = userName,
                Email = email,
                Password = password
            });

        public void SignOut()
            => _dispatcher.Dispatch(new SignOutAction { });
    }
}
