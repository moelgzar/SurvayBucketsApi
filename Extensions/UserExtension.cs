using System.Security.Claims;

namespace SurvayBucketsApi.Extensions;

public static class UserExtension
{

    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
