﻿using EatCalculator.UI.Entities.Meals.Models.Contracts;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor;

namespace EatCalculator.UI.Entities.Meals.Models.Store.Actions
{
    internal record UpdateMealAction : BaseAction
    {
        public required int Id { get; init; }
        public required UpdateMealContract Meal { get; init; }
    }
    internal record UpdateMealFailureAction : BaseFailureAction;
    internal record UpdateMealSuccessAction : BaseSuccessAction
    {
        public required Meal Meal { get; init; }
    }
}
