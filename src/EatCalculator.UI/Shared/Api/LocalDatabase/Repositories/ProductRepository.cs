using DALQueryChain.EntityFramework.Repositories;
using EatCalculator.UI.Shared.Api.LocalDatabase.Context;
using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Shared.Api.LocalDatabase.Repositories
{
    public class ProductRepository : BaseRepository<EatCalculatorDbContext, Product>
    {
        public ProductRepository(EatCalculatorDbContext context) : base(context)
        {

        }
    }
}
