namespace UI.Lib.Validation
{
    public static class FluentValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> DisableSpecialChars<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder.Matches("^(?=.*[A-Za-z0-9]$)[A-Za-z][A-Za-z\\d.-]{0,63}$");
    }
}
