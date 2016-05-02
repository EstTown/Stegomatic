using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using StegomaticProject.CustomExceptions;

namespace StegomaticProject.StegoSystemModel.Miscellaneous
{
    public class HandleUserInput
    {
        public void File(string path)
        {
            FileInfo pathToCheck = new FileInfo(path);
            if (!pathToCheck.Exists)
            {
                throw new NotifyUserException("Invalid path: " + pathToCheck.FullName);
            }
            else if (pathToCheck.IsReadOnly)
            {
                throw new NotifyUserException("ReadOnly path: " + pathToCheck.FullName);
            }
        }

        public void Message(string message)
        {

        }
    }
}
