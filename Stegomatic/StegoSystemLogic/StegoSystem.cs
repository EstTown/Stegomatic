using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using StegomaticProject.StegoSystemLogic.Miscellaneous;

namespace StegomaticProject.StegoSystemLogic
{
    public class StegoSystem : IStegoSystem
    {
        public string DecodeMessageFromImage(Bitmap coverImage, string encryptionKey)
        {
            bool decrypt = true;
            if (encryptionKey == null)
            {
                decrypt = false;
            }
            throw new NotImplementedException();
        }

        public Bitmap EncodeMessageInImage(Bitmap coverImage, bool encrypt = true, bool compress = true, bool confound = true)
        {
            HandleUserInput 
            try
            {
                
            }
            catch (Exception)
            {
                    
                throw;
            }



            throw new NotImplementedException();
        }
    }
}
