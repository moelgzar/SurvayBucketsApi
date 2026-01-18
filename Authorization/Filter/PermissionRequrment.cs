namespace SurvayBucketsApi.Authorization.Filter;

public class PermissionRequrment(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
