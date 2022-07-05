using System;
using System;
using Microsoft.EntityFrameworkCore;
using EmailService.Models;
using Microsoft.AspNetCore.Mvc;
namespace MailServiceController;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    private readonly IMailService mailService;
    public MailController(IMailService mailService)
    {
        this.mailService = mailService;
    }
    [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromForm]MailRequest request)
    {
        try
        {
            await mailService.sendEmailAsync(request);
            return Ok("Mail Sent!");
        }
        catch (Exception ex)
        {
            throw;
        }
            
    }
}