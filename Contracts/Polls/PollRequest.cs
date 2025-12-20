using System.ComponentModel.DataAnnotations;

namespace SurvayBucketsApi.Contracts.Polls;

public record PollRequest(
string Title , 
     string Summray,
     bool IsPublished,
     DateOnly StartsAt,
     DateOnly EndsAt);
