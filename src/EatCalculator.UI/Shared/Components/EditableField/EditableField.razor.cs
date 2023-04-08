using EatCalculator.UI.Shared.Lib.Validation;

namespace EatCalculator.UI.Shared.Components
{
    public partial class EditableField : BaseComponent
    {
        #region Params

        [Parameter] public string Value { get; set; } = string.Empty;   
        [Parameter] public EventCallback<string> ValueChanged { get; set; }
        [Parameter] public bool IsEditMode { get; set; }
        [Parameter] public EventCallback<bool> IsEditModeChanged { get; set; }


        [Parameter] public Typo Typo { get; set; }

        [Parameter] public BaseSingleValueValidator<string>? Validation { get; set; }

        #endregion

        #region UI Fields

        private MudTextField<string>? _textField;
        private List<string> _validationMessages = new();
        private bool _isValidationTooltipVisible;

        private string _innerValue = string.Empty;

        #endregion

        #region LC Methods

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            _innerValue = Value;
        }

        #endregion

        #region Internal events

        private void OnStartEditButtonClick()
            => StartEdit();

        private void OnSubmitEditButtonClick()
            => SubmitEdit();

        private void OnCancelEditButtonClick()
            => CancelEdit();

        private void OnInnerValueChanged(string value)
        {   
            _innerValue = value;
            if (Validation == null)
                return;

            _isValidationTooltipVisible = false;
            _validationMessages.Clear();

            var validationResult = Validation.Validate(_innerValue);
            if (validationResult.IsValid)
                return;

            _isValidationTooltipVisible = true;
            _validationMessages.AddRange(validationResult.Errors.Select(x => x.ErrorMessage)); 
        }

        #endregion

        #region Private methods

        private void FireIsEditModeChange(bool value)
        {
            IsEditMode = value;
            IsEditModeChanged.InvokeAsync(IsEditMode).AndForget();
        }

        private void FireValueChange(string value)
        {
            Value = value;
            ValueChanged.InvokeAsync(Value).AndForget();
        }

        private void StartEdit()
            => FireIsEditModeChange(true);

        private void SubmitEdit()
        {
            if (_textField == null)
                return;

            if (Validation != null && !Validation.Validate(_innerValue).IsValid)
                return;

            FireIsEditModeChange(false);
            FireValueChange(_innerValue);
        }

        private void CancelEdit()
        {
            FireIsEditModeChange(false);
            _innerValue = Value;
        }

        #endregion
    }
}
