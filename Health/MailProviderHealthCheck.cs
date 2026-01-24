using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using SurvayBucketsApi.Settings;

namespace SurvayBucketsApi.Health;

public class MailProviderHealthCheck(IOptions<MailSettings> MailSetting) : IHealthCheck
{
    private readonly MailSettings _maillsettings = MailSetting.Value;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {

        try
        {
            using var smtp = new SmtpClient();


            smtp.Connect(_maillsettings.Host, _maillsettings.Port, SecureSocketOptions.StartTls , cancellationToken);

            smtp.Authenticate(_maillsettings.Mail, _maillsettings.Password , cancellationToken);

            return await Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception ex)
        {
            return await Task.FromResult(HealthCheckResult.Unhealthy(exception: ex));

        }



    }
}
