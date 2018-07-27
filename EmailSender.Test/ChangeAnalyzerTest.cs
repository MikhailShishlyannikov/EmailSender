namespace EmailSender.Test
{
    using System;
    using System.IO;
    using System.Net.Mail;
    using Infractructure;
    using Lib;
    using Log;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class ChangeAnalyzerTest
    {
        [Test]
        public void OnCreated_RaiseCreatedEvent_CallGetMessageMethod()
        {
            var emailServiceMoq = new Mock<IEmailService>();
            var fileManagerMoq = new Mock<IFileManager>();
            var fileSystemWatcherMoq = new Mock<IFileSystemWatcher>();
            var loggerMoq = new Mock<ILogger>();
            var settings = new Settings();

            var changeAnalyzer = new ChangeAnalyzer(
                fileSystemWatcherMoq.Object,
                emailServiceMoq.Object,
                fileManagerMoq.Object,
                settings,
                loggerMoq.Object);
            changeAnalyzer.Run();

            fileSystemWatcherMoq.Raise(
                m => m.Created += null,
                this,
                new FileSystemEventArgs(WatcherChangeTypes.Created, @"E:\ASP.NET\EmailSender\FilesForSending", "123.txt"));

            emailServiceMoq.Verify(s => s.GetMessage(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void OnCreated_RaiseCreatedEvent_CallSendMethod()
        {
            var emailServiceMoq = new Mock<IEmailService>();
            var fileManagerMoq = new Mock<IFileManager>();
            var fileSystemWatcherMoq = new Mock<IFileSystemWatcher>();
            var loggerMoq = new Mock<ILogger>();
            var settings = new Settings();

            var changeAnalyzer = new ChangeAnalyzer(
                fileSystemWatcherMoq.Object,
                emailServiceMoq.Object,
                fileManagerMoq.Object,
                settings,
                loggerMoq.Object);
            changeAnalyzer.Run();

            fileSystemWatcherMoq.Raise(
                m => m.Created += null,
                this,
                new FileSystemEventArgs(WatcherChangeTypes.Created, @"E:\ASP.NET\EmailSender\FilesForSending", "123.txt"));

            emailServiceMoq.Verify(s => s.Send(It.IsAny<MailMessage>()), Times.Once);
        }

        [Test]
        public void OnCreated_RaiseCreatedEvent_CallDeleteMethod()
        {
            var emailServiceMoq = new Mock<IEmailService>();
            var fileManagerMoq = new Mock<IFileManager>();
            var fileSystemWatcherMoq = new Mock<IFileSystemWatcher>();
            var loggerMoq = new Mock<ILogger>();
            var settings = new Settings();

            var changeAnalyzer = new ChangeAnalyzer(
                fileSystemWatcherMoq.Object,
                emailServiceMoq.Object,
                fileManagerMoq.Object,
                settings,
                loggerMoq.Object);
            changeAnalyzer.Run();

            fileSystemWatcherMoq.Raise(
                m => m.Created += null,
                this,
                new FileSystemEventArgs(WatcherChangeTypes.Created, @"E:\ASP.NET\EmailSender\FilesForSending", "123.txt"));

            fileManagerMoq.Verify(s => s.Delete(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void OnCreated_RaiseCreatedEventAndGetMessageMethodThrowsException_NotCallDeleteMethod()
        {
            var emailServiceMoq = new Mock<IEmailService>(MockBehavior.Strict);
            emailServiceMoq.Setup(m => m.GetMessage(It.IsAny<string>()))
                .Throws(new Exception());

            var fileManagerMoq = new Mock<IFileManager>();
            var fileSystemWatcherMoq = new Mock<IFileSystemWatcher>();
            var loggerMoq = new Mock<ILogger>();
            var settings = new Settings();

            var changeAnalyzer = new ChangeAnalyzer(
                fileSystemWatcherMoq.Object,
                emailServiceMoq.Object,
                fileManagerMoq.Object,
                settings,
                loggerMoq.Object);
            changeAnalyzer.Run();

            fileSystemWatcherMoq.Raise(
                m => m.Created += null,
                this,
                new FileSystemEventArgs(WatcherChangeTypes.Created, @"E:\ASP.NET\EmailSender\FilesForSending", "123.txt"));

            fileManagerMoq.Verify(s => s.Delete(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void OnCreated_RaiseCreatedEventAndSendMethodThrowsException_NotCallDeleteMethod()
        {
            var emailServiceMoq = new Mock<IEmailService>(MockBehavior.Strict);
            emailServiceMoq.Setup(m => m.Send(It.IsAny<MailMessage>()))
                .Throws(new Exception());

            var fileManagerMoq = new Mock<IFileManager>();
            var fileSystemWatcherMoq = new Mock<IFileSystemWatcher>();
            var loggerMoq = new Mock<ILogger>();
            var settings = new Settings();

            var changeAnalyzer = new ChangeAnalyzer(
                fileSystemWatcherMoq.Object,
                emailServiceMoq.Object,
                fileManagerMoq.Object,
                settings,
                loggerMoq.Object);
            changeAnalyzer.Run();

            fileSystemWatcherMoq.Raise(
                m => m.Created += null,
                this,
                new FileSystemEventArgs(WatcherChangeTypes.Created, @"E:\ASP.NET\EmailSender\FilesForSending", "123.txt"));

            fileManagerMoq.Verify(s => s.Delete(It.IsAny<string>()), Times.Never);
        }
    }
}
