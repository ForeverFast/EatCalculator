namespace Client.Core.Features.Products.CreateProductDialog.Models
{
    internal class CreateProductViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public double Grams { get; set; } = 100.0;
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbohydrate { get; set; }
    }
}
