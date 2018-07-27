namespace EmailSender
{
    using System;
    using System.IO;
    using Infractructure;
    using Lib;
    using Log;

    /// <summary>
    /// It monitors the target directory, initiates sending a message and deleting a file
    /// </summary>
    public class ChangeAnalyzer
    {
        private readonly IEmailService _emailService;
        private readonly IFileManager _fileManager;
        private readonly IFileSystemWatcher _fileSystemWatcher;
        private readonly Settings _settings;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeAnalyzer"/> class.
        /// </summary>
        /// <param name="fileSystemWatcherWrapper">The instance of <see cref="IFileSystemWatcher"/></param>
        /// <param name="emailService">The instance of <see cref="IEmailService"/></param>
        /// <param name="fileManager">The instance of <see cref="IFileManager"/></param>
        /// <param name="settings">The instance of <see cref="Settings"/></param>
        /// <param name="logger">The instance of <see cref="ILogger"/></param>
        public ChangeAnalyzer(
            IFileSystemWatcher fileSystemWatcherWrapper,
            IEmailService emailService,
            IFileManager fileManager,
            Settings settings,
            ILogger logger)
        {
            _settings = settings;
            _fileSystemWatcher = fileSystemWatcherWrapper;
            _emailService = emailService;

            _fileManager = fileManager;
            _logger = logger;
        }

        /// <summary>
        /// Initializes the <see cref="IFileSystemWatcher"/> and subscribes to the events
        /// </summary>
        public void Run()
        {
            _fileSystemWatcher.Path = _settings.DirectoryPath;
            _fileSystemWatcher.Filter = "*.txt";
            _fileSystemWatcher.EnableRaisingEvents = true;
            _fileSystemWatcher.Created += OnCreated;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                _logger.Info($"The file {e.Name} appeared in the target directory");
                var message = _emailService.GetMessage(e.FullPath);

                _emailService.Send(message);

                _fileManager.Delete(e.FullPath);
                _logger.Info($"The file {e.Name} was deleted");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                if (ex.InnerException != null) _logger.Fatal(ex.InnerException.Message);
                _logger.Warn($"The file {e.Name} will not be sent");
            }
        }
    }
}