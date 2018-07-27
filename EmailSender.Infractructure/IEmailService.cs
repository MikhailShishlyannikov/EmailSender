namespace EmailSender.Infractructure
{
    using System.Net.Mail;

    /// <summary>
    /// The service for creating and sending letters
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends message
        /// </summary>
        /// <param name="message">Sent message</param>
        MailMessage GetMessage(string filePath);

        /// <summary>
        /// Creates new instence of the <see cref="MailMessage"/> and attaches file for sending
        /// </summary>
        /// <param name="filePath">Path to file for sending</param>
        /// <returns>Returns new <see cref="MailMessage"/> with attachment</returns>
        void Send(MailMessage mailMessage);
    }
}
