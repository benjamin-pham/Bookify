using Bookify.Domain.Apartments;

namespace Bookify.Infrastructure.Repositories;
internal class ApartmentRepository : Repository<Apartment>, IApartmentRepository
{
    public ApartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
