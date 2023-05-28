using Client.Core.Shared.Api.Models;

namespace Client.Core.Tests.Data
{
    internal static class ProductData
    {
        public static readonly List<Product> Products = new()
        {
            new Product
            {
                Id = 1,
                Order = 1,
                Title = "Гречка",
                Grams = 100.0,
                Protein = 4.2,
                Fat = 1.1,
                Carbohydrate = 20.6,
            },
            new Product
            {
                Id = 2,
                Order = 2,
                Title = "Рис",
                Grams = 100.0,
                Protein = 2.2,
                Fat = 0.5,
                Carbohydrate = 24.69,
            },
            new Product
            {
                Id = 3,
                Order = 3,
                Title = "Курица",
                Grams = 100.0,
                Protein = 21,
                Fat = 8.2,
                Carbohydrate = 0.6,
            },
            new Product
            {
                Id = 4,
                Order = 4,
                Title = "Индейка",
                Grams = 100.0,
                Protein = 26.8,
                Fat = 7.3,
                Carbohydrate = 0.0,
            },
            new Product
            {
                Id = 5,
                Order = 5,
                Title = "Творог",
                Grams = 100.0,
                Protein = 16.0,
                Fat = 5.0,
                Carbohydrate = 3.0,
            },
            new Product
            {
                Id = 6,
                Order = 6,
                Title = "Протеин",
                Grams = 100.0,
                Protein = 80.3,
                Fat = 6.2,
                Carbohydrate = 5.7,
            },
            new Product
            {
                Id = 7,
                Order = 7,
                Title = "Минтай",
                Grams = 100.0,
                Protein = 15.9,
                Fat = 0.9,
                Carbohydrate = 0.0,
            },
            new Product
            {
                Id = 8,
                Order = 8,
                Title = "Овсянка",
                Grams = 100.0,
                Protein = 12.3,
                Fat = 6.2,
                Carbohydrate = 61.8,
            },
            new Product
            {
                Id = 9,
                Order = 9,
                Title = "Масло льняное",
                Grams = 100.0,
                Protein = 0.0,
                Fat = 99.8,
                Carbohydrate = 0.0,
            },
            new Product
            {
                Id = 10,
                Order = 10,
                Title = "Масло оливковое",
                Grams = 100.0,
                Protein = 0.0,
                Fat = 99.8,
                Carbohydrate = 0.0,
            },
            new Product
            {
                Id = 11,
                Order = 11,
                Title = "Яйцо",
                Grams = 100.0,
                Protein = 12.7,
                Fat = 11.5,
                Carbohydrate = 0.7,
            },
            new Product
            {
                Id = 12,
                Order = 12,
                Title = "Макароны из твёрдых сортов",
                Grams = 100.0,
                Protein = 3.6,
                Fat = 0.7,
                Carbohydrate = 21.4,
            },
            new Product
            {
                Id = 13,
                Order = 13,
                Title = "Булгур",
                Grams = 100.0,
                Protein = 13.0,
                Fat = 1.5,
                Carbohydrate = 68.0,
            },
        };
    }
}
