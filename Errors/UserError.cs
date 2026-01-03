using SurvayBucketsApi.Abstractions;

namespace SurvayBucketsApi.Errors;

public static class  UserError
{
    public static readonly  Error UserCredintial = new("User.invalid", "password or user incorrect " , StatusCodes.Status401Unauthorized);
    public static readonly  Error UserID = new("UserID.invalid", "  user ID incorrect ", StatusCodes.Status401Unauthorized);
    public static readonly Error UserRefreshTokenNotFound = new("UserRefreshTokenNotFound.invalid", "UserRefreshTokenNotFound error ", StatusCodes.Status401Unauthorized);


    //public static  Error UserCredintial()
    //{
    //    return new Error("UserCredintialInvalid", "password or Emil incorrect ");
    //} 

}
