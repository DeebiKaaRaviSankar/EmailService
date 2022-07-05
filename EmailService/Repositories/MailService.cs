using EmailService.Models;
using MimeKit;
// using System.Net.Mail;
using Microsoft.Extensions.Options;
using MailKit.Security;
using MailKit.Net.Smtp;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }
    public async Task sendEmailAsync(MailRequest request)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        email.To.Add(MailboxAddress.Parse(request.toEmail));
        email.Subject = request.subject;
        var builder = new BodyBuilder();
        if (request.attachments != null)
        {
            byte[] filebytes;
            foreach (var file in request.attachments)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    filebytes = ms.ToArray();
                }
                builder.Attachments.Add(file.FileName, filebytes, ContentType.Parse(file.ContentType));
            }
        }
        builder.HtmlBody = request.body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);




    }

}