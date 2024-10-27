using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Repositories;
internal sealed class UserRepository : Repository<User>, IUserRepositiory
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    public override void Add(User user)
    {
        foreach (Role role in user.Roles)
        {
            DbContext.Attach(role);
        }

        DbContext.Add(user);
    }
}
