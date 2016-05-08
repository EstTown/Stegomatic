using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.CustomExceptions
{
    public class NotifyUserException : Exception
    {
        public string Title { get; private set; }

        public NotifyUserException(string message, string title = "Error") : base(message)
        {
            Title = title;
        }
    }
}
