namespace EmailSender.Test
{
    using System.Net.Mail;
    using Infractructure;
    using Lib;
    using Log;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class EmailServiceTest
    {
        [Test]
        public void Send_CallMethodSendOfSmtpClient_CallOnceTime()
        {
            var settings = new Settings();
            var smtpClientMoq = new Mock<ISmtpClient>();
            var mailMessage = new MailMessage();
            var loggerMoq = new Mock<ILogger>();

            var emailService = new EmailService(
                settings,
                smtpClientMoq.Object,
                mailMessage,
                loggerMoq.Object);

            emailService.Send(It.IsAny<MailMessage>());

            smtpClientMoq.Verify(s => s.Send(It.IsAny<MailMessage>()), Times.Once);
        }
    }
}
