namespace EatCalculator.UI.Features.Products.CreateProductForm.Models
{
    internal class CreateProductViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public double Grams { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbohydrate { get; set; }
    }
}
