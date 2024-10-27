namespace Bookify.Domain.Users;
public interface IUserRepositiory
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(User user);
}
