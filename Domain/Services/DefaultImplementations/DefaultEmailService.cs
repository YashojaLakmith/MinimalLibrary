using Domain.DataAccess;
using Domain.Exceptions;
using Domain.Options;
using Domain.Validations;

using MailKit.Net.Smtp;

using MimeKit;
using MimeKit.Text;

namespace Domain.Services.DefaultImplementations
{
    public class DefaultEmailService : IEmailService
    {
        private readonly IUserDataAccess _userData;
        private readonly IEmailClientOptions _emailOptions;
        private readonly IInputDataValidations _dataValidations;

        public DefaultEmailService(IUserDataAccess userData, IEmailClientOptions emailOptions, IInputDataValidations dataValidations)
        {
            _userData = userData;
            _emailOptions = emailOptions;
            _dataValidations = dataValidations;
        }
        public async Task SendEmailToARegisteredUserAsync(string userId, string subject, string body, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidateUserId(userId);

            var user = await _userData.GetUserByIdAsync(userId, cancellationToken) ?? throw new RecordNotFoundException($"User with id: {userId} not found.");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailOptions.MailBoxClientName, _emailOptions.MailBoxClientAddress));
            message.To.Add(new MailboxAddress(user.UserName, user.EmailAddress));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Plain) { Text =  body };

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailOptions.SMTPHost, _emailOptions.SMTPPort, cancellationToken: cancellationToken);
            await client.AuthenticateAsync(_emailOptions.SMTPUsername, _emailOptions.SMTPPassword, cancellationToken);
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}
