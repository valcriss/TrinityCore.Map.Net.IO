using System;
using System.Collections.Generic;
using System.Text;

namespace TrinityCore.Map.Net.IO.Exceptions
{
    public class DirectoryInvalidException : Exception
    {
        #region Public Constructors

        public DirectoryInvalidException()
            : base("Provided directory is not valid (not exists or no map data found)")
        {
        }

        #endregion Public Constructors
    }
}
