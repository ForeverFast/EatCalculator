using Client.Core.App.Models;
using System.Reflection;

namespace Client.Core.App
{
    public partial class ClientApp
    {
        #region Params

        [Parameter] public required ClientAppConfiguration AppConfiguration { get; set; }
        [Parameter] public Assembly[]? Assemblies { get; set; }
        [Parameter] public Type LayoutComponent { get; set; } = typeof(ClientAppLayout);

        #endregion

        #region Injects

        [Inject] IDispatcher _dispatcher { get; init; } = null!;

        #endregion

        #region LC Methods

        protected override async Task OnParametersSetAsync()
        {
            await base.OnInitializedAsync();

            //_dispatcher.Dispatch(AppStateConfiguration);
        }

        #endregion
    }
}
