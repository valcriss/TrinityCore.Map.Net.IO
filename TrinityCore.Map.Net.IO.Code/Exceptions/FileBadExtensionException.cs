using System;

namespace TrinityCore.Map.Net.IO.Exceptions
{
    public class FileBadExtensionException : Exception
    {
        #region Public Constructors

        public FileBadExtensionException(string expectedExtension)
            : base("Provided file extension does not match expected extension (" + expectedExtension + ")")
        {
        }

        #endregion Public Constructors
    }
}