using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SurvayBucketsApi.Abstractions.Const;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Helpers;

namespace SurvayBucketsApi.services;

public class NotificationService(ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    IHttpContextAccessor httpContextAccessor, IEmailSender emailSender
    ) : INotificationService
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task SendNewPollsNotifications(int? pollid = null)
    {

        IEnumerable<Poll> polls = [];

        if (pollid.HasValue)
        {
            var poll = await _context.polls.SingleOrDefaultAsync(x => x.Id == pollid);

            polls = [poll!];
        }
        else
        {

            polls = await _context.polls.
                      Where(x => x.IsPublished && x.StartsAt == DateOnly.FromDateTime(DateTime.UtcNow))
                      .AsNoTracking()
                      .ToListAsync();

        }

        //TODO select members only  



        var users = await _userManager.GetUsersInRoleAsync(DefaultRole.Member);
        var orign = _httpContextAccessor.HttpContext?.Request.Headers.Origin;


        foreach (var pol in polls)
        {

            foreach (var user in users)
            {
                var placholders = new Dictionary<string, string>()
               {
                   {"{{name}}" , user.FirstName } ,
                   {"{{pollTill}}" , pol.Title } ,
                   {"{{endDate}}" , pol.EndsAt.ToString() } ,
                   {"{{url}}" ,  $"{orign}/polls/start/{pol.Id}" }
               };

                var emailbody = EmailBodyBuilder.GenerateEmailBody("PollNotification", placholders);
                await _emailSender.SendEmailAsync(user.Email!, $"New Poll {pol.Title}", emailbody);


            }

        }
    }
}
