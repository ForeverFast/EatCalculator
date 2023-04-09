namespace Client.Core.Features.Days.CreateDayDialog.Models
{
    internal sealed class CreateDayViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? MealCount { get; set; }
    }
}
