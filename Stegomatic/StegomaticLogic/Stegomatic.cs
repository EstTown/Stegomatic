﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StegomaticProject.StegomaticLogic
{
    public class Stegomatic : IStegomatic
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
            throw new NotImplementedException();
        }
    }
}
