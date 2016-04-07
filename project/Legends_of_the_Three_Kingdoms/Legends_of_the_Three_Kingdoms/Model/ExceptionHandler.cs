using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    public class NotDefinedException: Exception
    {
        public NotDefinedException(String message)
            : base(message) { 
        }
    }

    public class EmptyException: Exception
    {
        public EmptyException(String message)
             : base(message) {
        }
    }

    public class NoCardException : Exception
    {
        public NoCardException(String message)
             : base(message)
        {
        }

        public NoCardException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
