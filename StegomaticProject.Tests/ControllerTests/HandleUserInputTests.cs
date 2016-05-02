using NUnit.Framework;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegomaticProject.StegoSystemModel.Miscellaneous;
using StegomaticProject.CustomExceptions;
using StegomaticProject.StegoSystemController;

namespace StegomaticProject.Tests.ControllerTests
{
    [TestFixture]
    public class HandleUserInputTests
    {
        private HandleUserInput _testUserInput;
        private string _pathOfTempFile;

        [OneTimeSetUp]
        public void InitialSetup()
        {
            // Finds the directory upon where the exe/assembly file lies and combines that with a filename.
            // In the following tests tempFile.txt will be created on that location.

            _testUserInput = new HandleUserInput();
            string pathOfProjectExecutableFile = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            string directoryOfExecutableFile = Path.GetDirectoryName(pathOfProjectExecutableFile);
            _pathOfTempFile = Path.Combine(directoryOfExecutableFile, "tempFile.txt");
            _pathOfTempFile = _pathOfTempFile.Remove(0, 6);
            // Removes the first six characters, which are invalid and show "file:/" - we only want the path.
        }

        private void CreateFileCloseStream()
        {
            FileStream fs = File.Create(_pathOfTempFile);
            fs.Close();
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
            CreateFileCloseStream();
            File.SetAttributes(_pathOfTempFile, FileAttributes.Normal);

            try
            {
                _testUserInput.File(_pathOfTempFile);
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
            CreateFileCloseStream();
            File.SetAttributes(_pathOfTempFile, FileAttributes.ReadOnly);

            try
            {
                _testUserInput.File(_pathOfTempFile);
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
                _testUserInput.File(_pathOfTempFile);
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
