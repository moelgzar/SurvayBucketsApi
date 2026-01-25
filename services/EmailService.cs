using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using SurvayBucketsApi.Settings;

namespace SurvayBucketsApi.services;

public class EmailService(IOptions<MailSettings> _maillsettings) : IEmailSender
{
    private readonly MailSettings _maillsettings = _maillsettings.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {

        var message = new MimeMessage

        {
            Sender = MailboxAddress.Parse(_maillsettings.Mail),

            Subject = subject
        };

        message.To.Add(MailboxAddress.Parse(email));

        var builder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };

        message.Body = builder.ToMessageBody();

        using var stmp = new SmtpClient();

        stmp.Connect(_maillsettings.Host, _maillsettings.Port, SecureSocketOptions.StartTls);

        stmp.Authenticate(_maillsettings.Mail, _maillsettings.Password);
        await stmp.SendAsync(message);
        stmp.Disconnect(true);



    }
}
