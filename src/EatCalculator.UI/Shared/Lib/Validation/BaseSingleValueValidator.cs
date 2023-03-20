using FluentValidation;

namespace EatCalculator.UI.Shared.Lib.Validation
{
    public class BaseSingleValueValidator<T> : AbstractValidator<T>
    {
        public BaseSingleValueValidator(Action<IRuleBuilderInitial<T, T>>? rule = null)
        {
            rule?.Invoke(RuleFor(x => x));
        }

        public Func<T, IEnumerable<string>> Validation
            => ValidateValue;

        private IEnumerable<string> ValidateValue(T arg)
        {
            var result = Validate(arg);
            if (result.IsValid)
                return new string[0];
            return result.Errors.Select(e => e.ErrorMessage);
        }
    }
}
