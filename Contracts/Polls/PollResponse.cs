using System.ComponentModel.DataAnnotations;

namespace SurvayBucketsApi.Contracts.Polls;

public record PollResponse(

     int Id,
     string Title,
     string Summray,
     bool IsPublished,
     DateOnly StartsAt,
     DateOnly EndsAt);
