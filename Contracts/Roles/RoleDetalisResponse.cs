namespace SurvayBucketsApi.Contracts.Roles;

public record RoleDetalisResponse(

    string Id,
    string Name,
    bool IsDeleted , 
    IEnumerable<string> Permissions 

    );

