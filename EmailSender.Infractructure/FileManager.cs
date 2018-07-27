namespace EmailSender.Infractructure
{
    using System.IO;

    /// <summary>
    /// The manager for deleting a sent files
    /// </summary>
    public class FileManager : IFileManager
    {
        /// <summary>
        /// Deletes the sent file
        /// </summary>
        /// <param name="filePath">Path to the sent file</param>
        public void Delete(string filePath)
        {
            File.Delete(filePath);
        }
    }
}
