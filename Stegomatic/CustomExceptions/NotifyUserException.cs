using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegomatic.CustomExceptions
{
    public class NotifyUserException : Exception
    {
        public NotifyUserException(string message) : base(message)
        {
        }
    }
}
