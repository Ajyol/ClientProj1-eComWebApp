using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;

public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;
    private readonly ILogger<EmailService> _logger;

    public EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, ILogger<EmailService> logger)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUsername = smtpUsername;
        _smtpPassword = smtpPassword;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException(nameof(email), "Email address cannot be null or empty.");
        }

        try
        {
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpUsername),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                // Log sender and receiver
                _logger.LogInformation($"Sending email from {_smtpUsername} to {email}");

                await client.SendMailAsync(mailMessage);

                // Log success
                _logger.LogInformation($"Email sent successfully from {_smtpUsername} to {email}");
            }
        }
        catch (Exception ex)
        {
            // Log failure
            _logger.LogError($"Failed to send email from {_smtpUsername} to {email}: {ex.Message}");
            throw;
        }
    }
}
