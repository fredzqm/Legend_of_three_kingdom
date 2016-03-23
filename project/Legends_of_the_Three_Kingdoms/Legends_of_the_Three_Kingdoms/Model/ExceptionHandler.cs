using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends_of_the_Three_Kingdoms.Model
{
    public class NotDefineException: Exception
    {
        public NotDefineException(String message)
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
    }

}
