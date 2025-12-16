using System.ComponentModel.DataAnnotations;

namespace SurvayBucketsApi.Contracts.Requests;

public record CrearePollRequest(
string Title , 
     string Summray,
     bool IsPublished,
     DateOnly StartsAt,
     DateOnly EndsAt);
