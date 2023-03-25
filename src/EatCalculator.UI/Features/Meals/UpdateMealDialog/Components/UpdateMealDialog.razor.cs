using EatCalculator.UI.Entities.Meals.Models.Contracts;
using EatCalculator.UI.Entities.Meals.Models.Store;
using EatCalculator.UI.Entities.Meals.Models.Store.Actions;
using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Features.Meals.UpdateMealDialog.Models;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Validation.SingleValueValidators;
using FluentValidation;

namespace EatCalculator.UI.Features.Meals.UpdateMealDialog
{
    public partial class UpdateMealDialog : BaseDialogComponent
    {
        #region Params

        [Parameter] public required Meal Meal { get; set; }

        #endregion

        #region Injects

        [Inject] MealStateFacade _mealStateFacade { get; init; } = null!;
        [Inject] ProductStateFacade _productStateFacade { get; init; } = null!;

        #endregion

        #region UI Fields

        private bool _titleEditMode = false;
        private string _title = string.Empty;
        private TitleValidator _titleValidator = null!;

        private List<EditablePortionWithProductInfo> _portions = new();
        private List<Product> _products = new();

        private List<Product> _notIncludedProducts
            => _products.Where(x => !_portions.Any(y => y.Portion.ProductId == x.Id)).ToList();

        private List<string> _portionsValidation = new();
        private bool _IsPortionsValidationInfoVisible = false;

        #endregion

        #region LC Methods

        protected override void OnInitialized()
            => CalculatePortionsInfo(() =>
            {
                base.OnInitialized();

                SubscribeToAction<UpdateMealSuccessAction>(OnUpdateMealSuccessAction);

                _title = Meal.Title;
                _titleValidator = new TitleValidator();
                _products.AddRange(_productStateFacade.Products.Value);

                Meal.Portions.Select(x => new PortionViewModel
                {
                    ProductId = x.ProductId,
                    ProteinPercentages = x.ProteinPercentages,
                    FatPercentages = x.FatPercentages,
                    CarbohydratePercentages = x.CarbohydratePercentages,
                }).ToList().ForEach(x =>
                {
                    var product = _productStateFacade.GetProductById(x.ProductId);
                    if (product == null)
                        return;

                    _portions.Add(new EditablePortionWithProductInfo
                    {
                        Product = product,
                        Portion = x,
                    });
                });
            });

        #endregion

        #region External events

        private void OnUpdateMealSuccessAction(UpdateMealSuccessAction _)
            => Close();

        #endregion

        #region Internal events

        private void OnBackToDayButtonClick()
            => Close();

        private void OnDeleteProductFromMealButtonClick(EditablePortionWithProductInfo portionWithProductInfo)
            => CalculatePortionsInfo(() => _portions.Remove(portionWithProductInfo));

        private void OnPortionViewModelValueChanged(Action action)
            => CalculatePortionsInfo(action);

        private void OnAddProductToMealButtonClick(Product product)
            => CalculatePortionsInfo(() => _portions.Add(new EditablePortionWithProductInfo
            {
                Product = product,
                Portion = new PortionViewModel
                {
                    ProductId = product.Id,
                },
            }));

        private void OnSaveButtonClick()
        {
            if (!CalculatePortionsInfo())
            {
                _IsPortionsValidationInfoVisible = true;    
                return;
            }

            _mealStateFacade.UpdateMeal(Meal.Id, new UpdateMealContract
            {
                Title = _title,
                Order = Meal.Order,
                Portions = _portions.Select(x => new Portion
                {
                    Id = 0,
                    MealId = Meal.Id,
                    ProductId = x.Portion.ProductId,
                    ProteinPercentages = x.Portion.ProteinPercentages,
                    FatPercentages = x.Portion.FatPercentages,
                    CarbohydratePercentages = x.Portion.CarbohydratePercentages,
                }).ToList(),
            });
        }

        #endregion

        #region Private methods

        private bool CalculatePortionsInfo(Action? action = null)
        {
            action?.Invoke();

            _portionsValidation.Clear();

            if (_portions.Count == 0)
                return true;

            var proteinTotal = _portions.Sum(x => x.Portion.ProteinPercentages);
            if (proteinTotal < 100.0)
                _portionsValidation.Add("Протеина меньше 100%");
            if (proteinTotal > 100.0)
                _portionsValidation.Add("Протеина больше 100%");

            var fatTotal = _portions.Sum(x => x.Portion.FatPercentages);
            if (fatTotal < 100.0)
                _portionsValidation.Add("Жиров меньше 100%");
            if (fatTotal > 100.0)
                _portionsValidation.Add("Жиров больше 100%");

            var carbohydrateTotal = _portions.Sum(x => x.Portion.CarbohydratePercentages);
            if (carbohydrateTotal < 100.0)
                _portionsValidation.Add("Углеводов меньше 100%");
            if (carbohydrateTotal > 100.0)
                _portionsValidation.Add("Углеводов больше 100%");

            if (_portions.Any(x
                => x.Portion.ProteinPercentages == 0.0
                && x.Portion.FatPercentages == 0.0
                && x.Portion.CarbohydratePercentages == 0.0))
                _portionsValidation.Add("Есть незадействованные продукты!");

            return !_portionsValidation.Any();
        }

        private void Close()
            => MudDialog.Close();

        #endregion

        #region Inner models

        record EditablePortionWithProductInfo
        {
            public required PortionViewModel Portion { get; init; }
            public required Product Product { get; init; }
        }

        #endregion
    }
}
