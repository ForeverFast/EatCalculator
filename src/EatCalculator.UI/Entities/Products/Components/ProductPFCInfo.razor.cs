using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Entities.Products.Components
{
    public partial class ProductPFCInfo
    {
        #region Params

        [Parameter, EditorRequired] public required Product Product { get; set; }

        #endregion

        #region UI Fields

        private string _pFCInfo
            => $"БЖУ {Product.Protein}/{Product.Fat}/{Product.Carbohydrate} на {Product.Grams}г.";

        #endregion
    }
}
