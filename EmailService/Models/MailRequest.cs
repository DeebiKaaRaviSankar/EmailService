using System;
namespace EmailService.Models{
    public class MailRequest{
        public string? toEmail{get;set;}
        public string? subject{get;set;}
        public string? body{get;set;}
        public List<IFormFile>? attachments{get;set;}

    }
}