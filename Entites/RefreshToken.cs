namespace SurvayBucketsApi.Entites;

[Owned]
public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpireDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? RevokeOn { get; set; }



    public bool IsExpired()
    {
        return DateTime.UtcNow > ExpireDate;
    }


    public bool IsActive()
    {
        return RevokeOn == null && !IsExpired();
    }


}
