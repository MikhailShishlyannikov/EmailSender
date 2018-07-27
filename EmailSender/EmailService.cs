namespace EmailSender
{
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using Infractructure;
    using Lib;
    using Log;

    /// <summary>
    /// The service for creating and sending letters
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly ISmtpClient _smtpClient;
        private readonly MailMessage _mailMessage;
        private readonly Settings _settings;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="settings">The instance of <see cref="Settings"/></param>
        /// <param name="smtpClient">The instance of <see cref="ISmtpClient"/></param>
        /// <param name="mailMessage">The instance of <see cref="MailMessage"/></param>
        /// <param name="logger">The instance of <see cref="ILogger"/></param>
        public EmailService(Settings settings, ISmtpClient smtpClient, MailMessage mailMessage, ILogger logger)
        {
            _settings = settings;

            _smtpClient = smtpClient;
            _smtpClient.Credentials = new NetworkCredential(_settings.UserName, _settings.Password);
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.Host = _settings.Host;
            _smtpClient.Port = _settings.Post;
            _smtpClient.EnableSsl = true;

            _mailMessage = mailMessage;
            _logger = logger;
        }

        /// <summary>
        /// Sends message
        /// </summary>
        /// <param name="message">Sent message</param>
        public void Send(MailMessage message)
        {
            _smtpClient.Send(message);

            _logger.Info($"The mail to {_settings.EmailTo} sent.");
        }

        /// <summary>
        /// Creates new instence of the <see cref="MailMessage"/> and attaches file for sending
        /// </summary>
        /// <param name="filePath">Path to file for sending</param>
        /// <returns>Returns new <see cref="MailMessage"/> with attachment</returns>
        public MailMessage GetMessage(string filePath)
        {
            _mailMessage.From = new MailAddress(_settings.EmailFrom);
            _mailMessage.To.Add(new MailAddress(_settings.EmailTo));
            _mailMessage.BodyEncoding = Encoding.UTF8;
            _mailMessage.Subject = _settings.EmailSubject;
            _mailMessage.Body = _settings.EmailBody;

            var attachment = new Attachment(filePath);
            _mailMessage.Attachments.Add(attachment);

            _logger.Info($"The mail to {_settings.EmailTo} created.");

            return _mailMessage;
        }
    }
}
