using EatCalculator.UI.App.Models;
using System.Reflection;

namespace EatCalculator.UI.App
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

        #region State methods

        protected override async Task OnParametersSetAsync()
        {
            await base.OnInitializedAsync();

            //_dispatcher.Dispatch(AppStateConfiguration);
        }

        #endregion
    }
}
