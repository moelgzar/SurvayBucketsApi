namespace SurvayBucketsApi.services;

public interface INotificationService
{
    Task SendNewPollsNotifications(int? pollid = null);
}
