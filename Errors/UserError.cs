using SurvayBucketsApi.Abstractions;

namespace SurvayBucketsApi.Errors;

public static class  UserError
{
    public static readonly  Error UserCredintial = new("User.invalid", "password or user incorrect " , StatusCodes.Status401Unauthorized);
    public static readonly  Error UserID = new("UserID.invalid", "  user ID incorrect ", StatusCodes.Status401Unauthorized);
    public static readonly Error UserRefreshTokenNotFound = new("UserRefreshTokenNotFound.invalid", "UserRefreshTokenNotFound error ", StatusCodes.Status401Unauthorized);
    public static readonly Error UserEmailExits = new("UserEmail already exists by a nother one  ", " Enter another Email  ", StatusCodes.Status409Conflict);
    public static readonly Error EmailNotConfirmed = new("EmailNotConfirmed.invalid", "Email Not Confirmed  error ", StatusCodes.Status401Unauthorized);
    public static readonly Error DuplicatConfirmation = new("EmailConfirmedDuplicate.invalid", "Email  Confirmed  Already ", StatusCodes.Status400BadRequest);
    public static readonly Error invalidCode = new("code.invalid", "  code incorrect ", StatusCodes.Status401Unauthorized);
    public static readonly Error UserNotFound = new("User.notfound", "  user not found  ", StatusCodes.Status401Unauthorized);
    public static readonly Error UserDisabled = new("User.UserDisabled", "  user   User disabled , please cpntact your adminitrator   ", StatusCodes.Status401Unauthorized);
    public static readonly Error NotAllowedRoles = new("NotAllowedRoles.invalid", "Not Role exist  ", StatusCodes.Status400BadRequest);

    public static readonly Error DuplicatEmail = new("EmailDuplicate.invalid", "Email    Already exist", StatusCodes.Status400BadRequest);

    public static readonly Error LockedUser = new("User.UserDisabled", "  user   User disabled , please cpntact your adminitrator   ", StatusCodes.Status401Unauthorized);

    //public static  Error UserCredintial()
    //{
    //    return new Error("UserCredintialInvalid", "password or Emil incorrect  ");
    //} 

}
