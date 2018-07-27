namespace EmailSender.Infractructure
{
    using System.IO;

    /// <summary>
    /// Wrapper for <see cref="FileSystemWatcher"/>
    /// </summary>
    public class FileSystemWatcherWrapper : IFileSystemWatcher
    {
        private readonly FileSystemWatcher _fileSystemWatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemWatcherWrapper"/> class.
        /// </summary>
        /// <param name="fileSystemWatcher">The instance of <see cref="FileSystemWatcher"/></param>
        public FileSystemWatcherWrapper(FileSystemWatcher fileSystemWatcher)
        {
            _fileSystemWatcher = fileSystemWatcher;
        }

        /// <summary>
        /// Gets or sets directory for watching
        /// </summary>
        public string Path
        {
            get => _fileSystemWatcher.Path;
            set => _fileSystemWatcher.Path = value;
        }

        /// <summary>
        /// Gets or sets the filter string used to determine what files are monitored in a directory
        /// </summary>
        public string Filter
        {
            get => _fileSystemWatcher.Filter;
            set => _fileSystemWatcher.Filter = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the component is enabled
        /// </summary>
        public bool EnableRaisingEvents
        {
            get => _fileSystemWatcher.EnableRaisingEvents;
            set => _fileSystemWatcher.EnableRaisingEvents = value;
        }

        /// <summary>
        /// Occurs when a file or directory in the specified Path is created
        /// </summary>
        public event FileSystemEventHandler Created
        {
            add => _fileSystemWatcher.Created += value;
            remove => _fileSystemWatcher.Created -= value;
        }
    }
}
