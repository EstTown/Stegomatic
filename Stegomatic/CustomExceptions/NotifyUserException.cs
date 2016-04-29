using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.CustomExceptions
{
    public class NotifyUserException : Exception
    {
        public NotifyUserException(string message) : base(message)
        {
        }
    }
}
