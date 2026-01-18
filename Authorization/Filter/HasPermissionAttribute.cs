namespace SurvayBucketsApi.Authorization.Filter;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
} 
