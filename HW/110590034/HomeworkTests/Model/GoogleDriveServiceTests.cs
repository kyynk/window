using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Google.Apis.Drive.v2;
using System.Net.Http;

namespace Homework.Model.Tests
{
    [TestClass()]
    public class GoogleDriveServiceTests
    {
        private GoogleDriveService _drive;
        PrivateObject _privateDrive;

        // setup
        [TestInitialize()]
        public void Initialize()
        {
            _drive = new GoogleDriveService(Constant.Constant.PROJECT_NAME, Constant.Constant.SECRET_FILE_NAME);
            _privateDrive = new PrivateObject(_drive);
        }

        // test constructor
        [TestMethod()]
        public void GoogleDriveServiceTest()
        {
            _drive = new GoogleDriveService(Constant.Constant.PROJECT_NAME, Constant.Constant.SECRET_FILE_NAME);
            _privateDrive = new PrivateObject(_drive);

            Assert.IsNotNull(_privateDrive.GetField("_applicationName"));
            Assert.IsNotNull(_privateDrive.GetField("_clientSecretFileName"));
            Assert.IsNotNull(_privateDrive.GetField("_credential"));
            Assert.IsNotNull(_privateDrive.GetField("_timeStamp"));
            Assert.IsNotNull(_privateDrive.GetField("_service"));
        }

        // test create new service
        [TestMethod()]
        public void CreateNewServiceTest()
        {
            _drive.CreateNewService(Constant.Constant.PROJECT_NAME, Constant.Constant.SECRET_FILE_NAME);

            Assert.IsNotNull(_privateDrive.GetField("_credential"));
            Assert.IsNotNull(_privateDrive.GetField("_timeStamp"));
            Assert.IsNotNull(_privateDrive.GetField("_service"));
        }


        // test check crdential time stamp
        [TestMethod()]
        public void CheckCredentialTimeStampTest()
        {
            _privateDrive.SetField("_timeStamp", _drive.UNIXNowTimeStamp - 4000);
            _drive.CheckCredentialTimeStamp();

            Assert.IsNotNull(_privateDrive.GetField("_credential"));
            Assert.IsNotNull(_privateDrive.GetField("_service"));
        }

        // test upload file async
        [TestMethod()]
        public void UploadFileAsyncTest()
        {
            
        }

        // test upload file
        [TestMethod()]
        public void UploadFileTest()
        {
            
        }

        // test download file
        [TestMethod()]
        public void DownloadFileTest()
        {
            string tempFilePath = "110590034.json";
            string shapesJson = JsonConvert.SerializeObject("test shapes", Formatting.Indented);
            string panelMaxXJson = JsonConvert.SerializeObject("test width", Formatting.Indented);

            // Combine List<Shapes> and _panelMaxX into a single JSON object
            JObject jsonToUpload = new JObject
            {
                ["Shapes"] = JToken.Parse(shapesJson),
                ["PanelMaxX"] = JToken.Parse(panelMaxXJson)
            };
            File.WriteAllText(tempFilePath, jsonToUpload.ToString());

            Task<Google.Apis.Drive.v2.Data.File> task = _drive.UploadFileAsync(tempFilePath, "application/json");
        }

        // test delete file
        [TestMethod()]
        public void DeleteFileTest()
        {
            
        }

        // test update file
        [TestMethod()]
        public void UpdateFileTest()
        {
            
        }

        // test get file by title
        [TestMethod()]
        public void GetFileByTitleTest()
        {
            
        }

        // test get file id by title
        [TestMethod()]
        public void GetFileIdByTitleTest()
        {
            
        }

        // test download file and get content
        [TestMethod()]
        public void DownloadFileAndGetContentTest()
        {
            
        }
    }
}