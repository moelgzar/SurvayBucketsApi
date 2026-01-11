namespace SurvayBucketsApi.Abstractions.Const;

public static class RegexPattern
{
    public  const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
}
