using System;

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
