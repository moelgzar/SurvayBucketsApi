using SurvayBucketsApi.Entites;

namespace SurvayBucketsApi.Authorization;

public interface IJwtProvider
{
    (string token , int ExperiesIn) GeneratedToken(ApplicationUser user ) ;

    string ValidateToken(string token);

}
