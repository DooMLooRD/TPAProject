namespace MEF
{
    public enum LogLevel
    {
        /// <summary>
        /// The highest level of logging (logs everything)
        /// </summary>
        Debug = 0,

        /// <summary>
        /// Log all informations except debug messages
        /// </summary>
        Informative = 1,

        /// <summary>
        /// Logs errors, warnings, and normal messages
        /// </summary>
        Normal = 2,

        /// <summary>
        /// Logs errors and warnings only
        /// </summary>
        Critical = 4,

        /// <summary>
        /// Logs nothing
        /// </summary>
        Nothing = 5
    }
}
