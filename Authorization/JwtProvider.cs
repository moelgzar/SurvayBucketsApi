using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurvayBucketsApi.Entites;

namespace SurvayBucketsApi.Authorization;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly IOptions<JwtOptions> _options = options;



    public (string token, int ExperiesIn) GeneratedToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions)
    {
        Claim[] claims = [

         new(JwtRegisteredClaimNames.Sub , user.Id),
         new (JwtRegisteredClaimNames.Email , user.Email!),
         new (JwtRegisteredClaimNames.GivenName , user.FirstName),
         new (JwtRegisteredClaimNames .FamilyName , user.LastName),
         new (JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
         new (nameof(roles) , JsonSerializer.Serialize(roles) , JsonClaimValueTypes.JsonArray),
         new (nameof(permissions) ,JsonSerializer.Serialize(permissions) , JsonClaimValueTypes.JsonArray)


         ];


        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));


        var signingCredientials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        //int expiresIn = 30;

        var token = new JwtSecurityToken(

            issuer: _options.Value.Issuer,
            audience: _options.Value.Audiance,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.Value.ExpireDate),
            signingCredentials: signingCredientials

            );

        return (token: new JwtSecurityTokenHandler().WriteToken(token), ExperiesIn: _options.Value.ExpireDate * 60);
    }

    public string ValidateToken(string token)
    {

        var tokenhandler = new JwtSecurityTokenHandler();
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));

        try
        {
            tokenhandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero


            }, out SecurityToken validatedToken);


            var jwttoken = (JwtSecurityToken)validatedToken;

            return jwttoken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

        }
        catch
        {
            return null;
        }
    }
}


/* Steps to generate token 

1- install jwt provider frorm nugetpackge manger 
2- creaate new folider in project called Authoriation 
3- create class jwtprovider and Interface Ijetprovider 
4- create one methode called generatetokent retun tuble (token , expierIn) and take one argument (object from applicarionuser )
5- add these service in program as a singlton ( life time along program life  )
6- implement these methode 
       a- create array of claims to add it  
            
             Claim[] claims = [

         new(JwtRegisteredClaimNames.Sub , user.Id),
         new (JwtRegisteredClaimNames.Email , user.Email!),
         new (JwtRegisteredClaimNames.GivenName , user.FirstName),
         new (JwtRegisteredClaimNames .FamilyName , user.LastName),
         new (JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),

         ];

       b- create Symetricsecuritykey  it takes key string by byte 
                  var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8b97b38f46882134bba28b9005b6b7a70ea5168c6f6641369cc2f74c566452aa"));
        
        c- create SignnigCredentials to tell it what is the type of algoritm usage 
                    var signingCredientials = new SigningCredentials(symmetricSecurityKey , SecurityAlgorithms.Aes256Encryption);


        d- create the token with issuer , audience , expiresin , .. 
             var token = new JwtSecurityToken(

            issuer: "SurvayBasket app ",
            audience: "Usera",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresIn),
            signingCredentials: signingCredientials

            );



        e- retun token and expiresin 

        return (token: new JwtSecurityTokenHandler().WriteToken(token), ExperiesIn: expiresIn);


--------------------------------
How JWT Works in Practice
Login Flow:

1- User submits credentials (username/password)
2- Server validates credentials
3- Server generates JWT token with user claims
4- Token sent to client
5- Client stores token (usually in localStorage or memory)

API Request Flow:

1- Client sends token in HTTP header: Authorization: Bearer <token>
2- Server validates token signature
3- Server reads claims from token
4- Server processes request with user context
No database query needed!

    */






