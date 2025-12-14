using System.ComponentModel.DataAnnotations;

namespace SurvayBucketsApi.Contracts.Requests;

public record CrearePollRequest(
     [RegularExpression("^[a-zA-Z0]*$" , ErrorMessage = "only english chars ")]
string Title , 
     string Description );
