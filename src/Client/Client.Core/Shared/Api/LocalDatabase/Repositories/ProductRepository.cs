using Client.Core.Shared.Api.LocalDatabase.Context;
using DALQueryChain.EntityFramework.Repositories;

namespace Client.Core.Shared.Api.LocalDatabase.Repositories
{
    public class ProductRepository : BaseRepository<ClientEatCalculatorDbContext, Product>
    {
        public ProductRepository(ClientEatCalculatorDbContext context) : base(context)
        {

        }
    }
}
