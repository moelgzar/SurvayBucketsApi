using System.ComponentModel.DataAnnotations;

namespace SurvayBucketsApi.Authorization;

public class JwtOptions
{
    public static string SectionName = "Jwt";

    [Required]
    public string Key { get; init; } = string.Empty;
    [Required]
    public string Issuer  { get; init; } = string.Empty;
    [Required]
    public string Audiance  { get; init; } = string.Empty;
    [Range(1, int.MaxValue , ErrorMessage ="the value must be grater than 1 ") ]
    public int ExpireDate { get; init; }


}
