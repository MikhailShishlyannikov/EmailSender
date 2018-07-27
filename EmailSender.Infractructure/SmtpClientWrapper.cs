namespace EmailSender.Infractructure
{
    using System;
    using System.Net;
    using System.Net.Mail;

    /// <summary>
    /// Wrapper for <see cref="T:System.Net.Mail.SmtpClient" />
    /// </summary>
    public class SmtpClientWrapper : ISmtpClient, IDisposable
    {
        private readonly SmtpClient _smtpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpClientWrapper"/> class.
        /// </summary>
        /// <param name="smtpClient">The instance of <see cref="SmtpClient"/></param>
        public SmtpClientWrapper(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        /// <summary>
        /// Gets or sets the credentials used to authenticate the sender
        /// </summary>
        public ICredentialsByHost Credentials
        {
            get => _smtpClient.Credentials;
            set => _smtpClient.Credentials = value;
        }

        /// <summary>
        /// Specifies how outgoing email messages will be handled.
        /// </summary>
        public SmtpDeliveryMethod DeliveryMethod
        {
            get => _smtpClient.DeliveryMethod;
            set => _smtpClient.DeliveryMethod = value;
        }

        /// <summary>
        /// Gets or sets the name or IP address of the host used for SMTP transactions
        /// </summary>
        public string Host
        {
            get => _smtpClient.Host;
            set => _smtpClient.Host = value;
        }

        /// <summary>
        /// Gets or sets the port used for SMTP transactions
        /// </summary>
        public int Port
        {
            get => _smtpClient.Port;
            set => _smtpClient.Port = value;
        }

        /// <summary>
        /// Specify whether the SmtpClient uses Secure Sockets Layer (SSL) to encrypt the connection
        /// </summary>
        public bool EnableSsl
        {
            get => _smtpClient.EnableSsl;
            set => _smtpClient.EnableSsl = value;
        }

        /// <summary>
        /// Sends the specified message to an SMTP server for delivery.
        /// </summary>
        /// <param name="mailMessage">A <see cref="MailMessage"/> that contains the message to send.</param>
        public void Send(MailMessage mailMessage)
        {
            _smtpClient.SendMailAsync(mailMessage).Wait();
            mailMessage.Dispose();
        }

        /// <summary>
        /// Sends a QUIT message to the SMTP server, gracefully ends the TCP connection, and releases all 
        /// resources used by the current instance of the <see cref="SmtpClient"/> class.
        /// </summary>
        public void Dispose()
        {
            _smtpClient.Dispose();
        }
    }
}
