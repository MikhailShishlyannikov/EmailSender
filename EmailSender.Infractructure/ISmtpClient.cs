namespace EmailSender.Infractructure
{
    using System.Net;
    using System.Net.Mail;

    /// <summary>
    /// Allows applications to send e-mail by using the Simple Mail Transfer Protocol (SMTP)
    /// </summary>
    public interface ISmtpClient
    {
        /// <summary>
        /// Gets or sets the credentials used to authenticate the sender
        /// </summary>
        ICredentialsByHost Credentials { get; set; }

        /// <summary>
        /// Specifies how outgoing email messages will be handled.
        /// </summary>
        SmtpDeliveryMethod DeliveryMethod { get; set; }

        /// <summary>
        /// Gets or sets the name or IP address of the host used for SMTP transactions
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// Gets or sets the port used for SMTP transactions
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Specify whether the SmtpClient uses Secure Sockets Layer (SSL) to encrypt the connection
        /// </summary>
        bool EnableSsl { get; set; }

        /// <summary>
        /// Sends the specified message to an SMTP server for delivery.
        /// </summary>
        /// <param name="mailMessage">A <see cref="MailMessage"/> that contains the message to send.</param>
        void Send(MailMessage mailMessage);

        /// <summary>
        /// Sends a QUIT message to the SMTP server, gracefully ends the TCP connection, and releases all 
        /// resources used by the current instance of the <see cref="SmtpClient"/> class.
        /// </summary>
        void Dispose();
    }
}
