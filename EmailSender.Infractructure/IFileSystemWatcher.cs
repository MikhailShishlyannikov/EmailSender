namespace EmailSender.Infractructure
{
    using System.IO;

    /// <summary>
    /// Listens to the system directory change notifications and
    /// raises events when a directory or file within a directory changes
    /// </summary>
    public interface IFileSystemWatcher
    {
        /// <summary>
        /// Gets or sets directory for watching
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Gets or sets the filter string used to determine what files are monitored in a directory
        /// </summary>
        string Filter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the component is enabled
        /// </summary>
        bool EnableRaisingEvents { get; set; }

        /// <summary>
        /// Occurs when a file or directory in the specified Path is created
        /// </summary>
        event FileSystemEventHandler Created;
    }
}
