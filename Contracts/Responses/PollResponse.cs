using System.ComponentModel.DataAnnotations;

namespace SurvayBucketsApi.Contracts.Responses;

public record PollResponse(

     int id,
     string Title,
     string Description);

