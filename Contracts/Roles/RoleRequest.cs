using Microsoft.Identity.Client;

namespace SurvayBucketsApi.Contracts.Roles;

public record RoleRequest (
    
    string Name ,
    IList<string> Permissions
    
    );

