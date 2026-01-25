using SurvayBucketsApi.Abstractions;

namespace SurvayBucketsApi.Errors;

public static class RoleError
{
    public static readonly Error RoleNotFound = new("Role.invalid", "Role not found ", StatusCodes.Status404NotFound);
    //public static readonly  Error UserID = new("UserID.invalid", "  user ID incorrect ", StatusCodes.Status401Unauthorized);
    //public static readonly Error UserRefreshTokenNotFound = new("UserRefreshTokenNotFound.invalid", "UserRefreshTokenNotFound error ", StatusCodes.Status401Unauthorized);
    //public static readonly Error UserEmailExits = new("UserEmail already exists by a nother one  ", " Enter another Email  ", StatusCodes.Status409Conflict);
    //public static readonly Error EmailNotConfirmed = new("EmailNotConfirmed.invalid", "Email Not Confirmed  error ", StatusCodes.Status401Unauthorized);
    public static readonly Error DuplicatRole = new("RoleDuplicate.invalid", "Role Already exist  ", StatusCodes.Status409Conflict);
    public static readonly Error NotAllowedPermission = new("NotAllowedPermission.invalid", "NotAllowedPermission exist  ", StatusCodes.Status400BadRequest);
    public static readonly Error invalidName = new("Name.invalid", "  Name alrady exisit beefore in another role please changgethis name ", StatusCodes.Status409Conflict);
    //public static readonly Error UserNotFound = new("User.notfound", "  user   not found  ", StatusCodes.Status401Unauthorized);


    //public static  Error UserCredintial()
    //{
    //    return new Error("UserCredintialInvalid", "password or Emil incorrect  ");
    //} 

}
