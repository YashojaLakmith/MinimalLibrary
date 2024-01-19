using Domain.Options;

namespace API.Options
{
    public class SMTPEmailOptions : IEmailClientOptions
    {
        public string SMTPHost { get; }

        public int SMTPPort {  get; }

        public string SMTPUsername {  get; }

        public string SMTPPassword {  get; }

        public string MailBoxClientName {  get; }

        public string MailBoxClientAddress {  get; }
    }
}
