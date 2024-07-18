using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}

