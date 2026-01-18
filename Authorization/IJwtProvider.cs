using SurvayBucketsApi.Entites;

namespace SurvayBucketsApi.Authorization;

public interface IJwtProvider
{
    (string token , int ExperiesIn) GeneratedToken(ApplicationUser user  , IEnumerable<string> roles, IEnumerable<string> permissions) ;

    string ValidateToken(string token);

}
