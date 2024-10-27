using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Bookify.Infrastructure.Authorization;
internal sealed class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly AuthorizationOptions _authorizationpOtions;
    public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options)
    {
        _authorizationpOtions = options.Value;
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        AuthorizationPolicy policy = await base.GetPolicyAsync(policyName);

        if (policy is not null)
            return policy;

        AuthorizationPolicy permissionPolicy = new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(policyName))
            .Build();

        _authorizationpOtions.AddPolicy(policyName, permissionPolicy);

        return permissionPolicy;
    }
}
