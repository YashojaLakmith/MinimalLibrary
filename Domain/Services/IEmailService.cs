using Domain.Exceptions;

namespace Domain.Services
{
    public interface IEmailService
    {
        /// <exception cref="ValidationFailedException"/>
        /// <exception cref="RecordNotFoundException"/>
        Task SendEmailToARegisteredUserAsync(string userId, string subject, string body, CancellationToken cancellationToken = default);
    }
}
