using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users;
public class UserErrors
{
    public static Error NotFound = new Error("User.NotFound", "The user with the specified identifier was not found");
}
