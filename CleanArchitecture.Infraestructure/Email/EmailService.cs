namespace CleanArchitecture.Infraestructure.Email;

public class EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger) : IEmailService
{
    public async Task<bool> SendEmailAsync(Application.Models.Email email)
    {
        var apiKey = Environment.GetEnvironmentVariable(emailSettings.Value.ApiKey) ?? emailSettings.Value.ApiKey;
        var client = new SendGridClient(apiKey);

        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;

        var from = new EmailAddress(emailSettings.Value.FromAddress, emailSettings.Value.FromName);

        var msg = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);

        var response = await client.SendEmailAsync(msg);

        if(response.StatusCode == System.Net.HttpStatusCode.Accepted || 
               response.StatusCode == System.Net.HttpStatusCode.OK)
            return true;
        
        logger.LogError($"Error sending email to {email.To}");
        return false;
    }
}