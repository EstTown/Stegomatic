using NUnit.Framework;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegomaticProject.StegoSystemLogic.Miscellaneous;
using StegomaticProject.CustomExceptions;

namespace StegomaticProject.Tests
{
    [TestFixture]
    public class HandleUserInputTests
    {
        HandleUserInput TestUserInput;
        string _pathOfTempFile;

        [OneTimeSetUp]
        public void InitialSetup()
        {
            // Finds the directory upon where the exe/assembly file lies and combines that with a filename.
            // In the following tests tempFile.txt will be created on that location.

            TestUserInput = new HandleUserInput();
            string pathOfProjectExecutableFile = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            string directoryOfExecutableFile = Path.GetDirectoryName(pathOfProjectExecutableFile);
            _pathOfTempFile = Path.Combine(directoryOfExecutableFile, "tempFile.txt");
        }

        [TearDown]
        public void TearDownAfterEach()
        {
            // Deletes tempFile if it exists. 

            if (File.Exists(_pathOfTempFile))
            {
                File.SetAttributes(_pathOfTempFile, FileAttributes.Normal);
                File.Delete(_pathOfTempFile);
            }
        }

        [Test]
        public void File_ExistsAndNotReadOnly_NothingHappens()
        {
            File.Create(_pathOfTempFile);
            File.SetAttributes(_pathOfTempFile, FileAttributes.Normal);

            try
            {
                TestUserInput.File(_pathOfTempFile);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void File_ExistsAndIsReadOnly_ThrowsNotifyUserException()
        {
            File.Create(_pathOfTempFile);
            File.SetAttributes(_pathOfTempFile, FileAttributes.ReadOnly);

            try
            {
                TestUserInput.File(_pathOfTempFile);
            }
            catch (NotifyUserException)
            {
                Assert.Pass();
            }
            catch (Exception)
            {
            }
            Assert.Fail();
        }

        [Test]
        public void File_DoesNotExist_ThrowsNotifyUserException()
        {
            try
            {
                TestUserInput.File(_pathOfTempFile);
            }
            catch (NotifyUserException)
            {
                Assert.Pass();
            }
            catch (Exception)
            {
            }
            Assert.Fail();
        }


    }
}
