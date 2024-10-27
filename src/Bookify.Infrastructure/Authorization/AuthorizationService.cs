using Bookify.Application.Abstractions.Caching;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization;
internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ICacheService _cacheService;

    public AuthorizationService(ApplicationDbContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cacheService = cacheService;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        string cacheKey = $"auth:roles-{identityId}";

        UserRolesResponse cacheRoles = await _cacheService.GetAsync<UserRolesResponse>(cacheKey);

        if (cacheRoles is not null)
            return cacheRoles;

        UserRolesResponse roles = await _dbContext.Set<User>()
            .Where(user => user.IdentityId == identityId)
            .Select(user => new UserRolesResponse
            {
                Id = user.Id,
                Roles = user.Roles.ToList()
            }).FirstAsync();

        await _cacheService.SetAsync(cacheKey, roles);

        return roles;
    }

    public async Task<HashSet<string>> GetPermissionForUserAsync(string identityId)
    {
        string cacheKey = $"auth:permissions-{identityId}";

        HashSet<string> cachePermissions = await _cacheService.GetAsync<HashSet<string>>(cacheKey);

        if (cachePermissions is not null)
            return cachePermissions;

        ICollection<Permission> permissions = await _dbContext.Set<User>()
            .Where(user => user.IdentityId == identityId)
            .SelectMany(user => user.Roles.Select(role => role.Permissions))
            .FirstAsync();

        HashSet<string> permissionSet = permissions.Select(p => p.Name).ToHashSet();

        await _cacheService.SetAsync(cacheKey, permissionSet);

        return permissionSet;
    }
}

