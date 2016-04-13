using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{/// <summary>
/// subclass of exception class
/// </summary>
    public class NotDefinedException: Exception
    {/// <summary>
    /// not defined exception
    /// </summary>
    /// <param name="message"></param>
        public NotDefinedException(String message)
            : base(message) { 
        }
    }
    /// <summary>
    /// subclass of exception class
    /// </summary>
    public class EmptyException: Exception
    {/// <summary>
    /// empty exception
    /// </summary>
    /// <param name="message"></param>
        public EmptyException(String message)
             : base(message) {
        }
    }
    /// <summary>
    /// subclass of exception class
    /// </summary>
    public class NoCardException : Exception
    {/// <summary>
    /// no card exception
    /// </summary>
    /// <param name="message"></param>
        public NoCardException(String message)
             : base(message)
        {
        }
        /// <summary>
        /// another no card exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public NoCardException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
