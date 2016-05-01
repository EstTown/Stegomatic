using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.Tests
{
    [TestFixture]
    public class GZipStreamCompressionTests
    {

        [Test]
        public void Compress_EHHEGHEGHEGINEEDSOMETHINGHERE_NoLossOfData()
        {

        }

        [Test]
        public void Decompress_EGEHEHEHEHH_NoLossOfData()
        {
            // TEST THESE WITH FANCY BYTE ARRAY? PERHAPS I SHOULD USE THE SPECIAL PROPERTYTHINGS: []
        }

        private byte[] CreateFancyByteArray()
        {
            throw new NotImplementedException();
        }
    }
}
