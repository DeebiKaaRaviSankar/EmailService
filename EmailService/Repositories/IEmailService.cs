using EmailService.Models;
namespace EmailService.Models{
public interface IMailService
{
    public Task sendEmailAsync(MailRequest mailrequest);
} 
}