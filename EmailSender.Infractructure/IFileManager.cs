namespace EmailSender.Infractructure
{
    /// <summary>
    /// The manager for deleting sent files
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Deletes the sent file
        /// </summary>
        /// <param name="filePath">Path to the sent file</param>
        void Delete(string filePath);
    }
}
