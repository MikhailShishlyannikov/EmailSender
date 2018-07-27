namespace EmailSender
{
    using System;
    using System.IO;
    using System.Net.Mail;
    using DIContainer;
    using Infractructure;
    using Lib;
    using Log;

    internal class Program
    {
        private static void Main()
        {
            var container = new Container();
            container.RegisterSelf<Settings>(new Settings());
            container.RegisterSelf<MailMessage>(new MailMessage());
            container.Register<ILogger, ConsoleLogger>();
            container.Register<ISmtpClient, SmtpClientWrapper>();
            container.RegisterSelf<SmtpClient>(new SmtpClient());
            container.Register<IFileSystemWatcher, FileSystemWatcherWrapper>();
            container.RegisterSelf<FileSystemWatcher>(new FileSystemWatcher());
            container.Register<IFileManager, FileManager>();
            container.Register<IEmailService, EmailService>();

            var changeAnalyzer = (ChangeAnalyzer)container.GetInstance(typeof(ChangeAnalyzer));

            changeAnalyzer.Run();

            Console.ReadLine();
        }
    }
}
