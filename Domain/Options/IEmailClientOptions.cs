namespace Domain.Options
{
    /// <summary>
    /// Specifies properties to be used for email service.
    /// </summary>
    public interface IEmailClientOptions
    {
        /// <summary>
        /// Host address for the SMTP server.
        /// </summary>
        public string SMTPHost { get; }

        /// <summary>
        /// Specific port. Default if 0.
        /// </summary>
        public int SMTPPort { get; }

        /// <summary>
        /// Username used to authenticate.
        /// </summary>
        public string SMTPUsername { get; }

        /// <summary>
        /// Password for authentication.
        /// </summary>
        public string SMTPPassword { get; }

        /// <summary>
        /// Sender name.
        /// </summary>
        public string MailBoxClientName { get; }

        /// <summary>
        /// Sender address.
        /// </summary>
        public string MailBoxClientAddress {  get; }
    }
}
