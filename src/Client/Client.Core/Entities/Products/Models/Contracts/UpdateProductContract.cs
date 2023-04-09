namespace Client.Core.Entities.Products.Models.Contracts
{
    internal record UpdateProductContract
    {
        public required string Title { get; init; }
        public string? Description { get; set; }

        public required double Grams { get; init; }
        public required double Protein { get; init; }
        public required double Fat { get; init; }
        public required double Carbohydrate { get; init; }
    }
}
