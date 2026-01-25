
using SurvayBucketsApi.Abstractions.Const;

namespace SurvayBucketsApi.Authorization.Filter;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequrment>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequrment requirement)
    {


        var user = context.User.Identity;
        if (user is null || !user.IsAuthenticated)
            return;

        var haspermission = context.User.Claims.Any(c => c.Value == requirement.Permission && c.Type == Permissions.Type);
        if (!haspermission)
            return;

        context.Succeed(requirement);
        return;

        //if (context.User.Identity is not { IsAuthenticated: true } ||
        //    !context.User.Claims.Any(x => x.Value == requirement.Permission && x.Type == Permissions.Type))
        //    return;

        context.Succeed(requirement);
        return;

    }
}
