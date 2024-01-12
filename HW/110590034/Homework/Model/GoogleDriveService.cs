using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Drive.v2;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Download;
using Google.Apis.Drive.v2.Data;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Homework.Model
{
    public class GoogleDriveService
    {
        private static readonly string[] SCOPES = new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive };
        private DriveService _service;
        private const int KB = 0x400;
        private const int DOWNLOAD_CHUNK_SIZE = 256 * KB;
        private int _timeStamp;
        private string _applicationName;
        private string _clientSecretFileName;
        private UserCredential _credential;

        /// <summary>
        /// 創造一個Google Drive Service
        /// </summary>
        /// <param name="applicationName">應用程式名稱</param>
        /// <param name="clientSecretFileName">ClientSecret檔案名稱</param>
        public GoogleDriveService(string applicationName, string clientSecretFileName)
        {
            _applicationName = applicationName;
            _clientSecretFileName = clientSecretFileName;
            this.CreateNewService(applicationName, clientSecretFileName);
        }

        // create new service
        public void CreateNewService(string applicationName, string clientSecretFileName)
        {
            const string USER = "user";
            const string CREDENTIAL_FOLDER = ".credential/";
            UserCredential credential;

            using (FileStream stream = new FileStream(clientSecretFileName, FileMode.Open, FileAccess.Read))
            {
                string credentialPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credentialPath = Path.Combine(credentialPath, CREDENTIAL_FOLDER + applicationName);
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, SCOPES, USER, CancellationToken.None, new FileDataStore(credentialPath, true)).Result;
            }

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            _credential = credential;
            DateTime now = DateTime.Now;
            _timeStamp = UNIXNowTimeStamp;
            _service = service;
        }

        public int UNIXNowTimeStamp
        {
            get
            {
                const int UNIX_START_YEAR = 1970;
                DateTime unixStartTime = new DateTime(UNIX_START_YEAR, 1, 1);
                return Convert.ToInt32((DateTime.Now.Subtract(unixStartTime).TotalSeconds));
            }
        }

        //Check and refresh the credential if credential is out-of-date
        public void CheckCredentialTimeStamp()
        {
            const int ONE_HOUR_SECOND = 3600;
            int nowTimeStamp = UNIXNowTimeStamp;

            if ((nowTimeStamp - _timeStamp) > ONE_HOUR_SECOND)
                this.CreateNewService(_applicationName, _clientSecretFileName);
        }
        
        // upload file async
        public async Task<Google.Apis.Drive.v2.Data.File> UploadFileAsync(string uploadFileName, string contentType, Action<IUploadProgress> uploadProgressEventHandler = null, Action<Google.Apis.Drive.v2.Data.File> responseReceivedEventHandler = null)
        {
            return await Task.Run(() => UploadFile(uploadFileName, contentType, uploadProgressEventHandler, responseReceivedEventHandler));
        }

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="uploadFileName">上傳的檔案名稱 </param>
        /// <param name="contentType">上傳的檔案種類，請參考MIME Type : http://www.iana.org/assignments/media-types/media-types.xhtml </param>
        /// <param name="uploadProgressEventHandeler"> 上傳進度改變時呼叫的函式</param>
        /// <param name="responseReceivedEventHandler">收到回應時呼叫的函式 </param>
        /// <returns>上傳成功，回傳上傳成功的 Google Drive 格式之File</returns>
        public Google.Apis.Drive.v2.Data.File UploadFile(string uploadFileName, string contentType, Action<IUploadProgress> uploadProgressEventHandeler = null, Action<Google.Apis.Drive.v2.Data.File> responseReceivedEventHandler = null)
        {
            using (FileStream uploadStream = new FileStream(uploadFileName, FileMode.Open, FileAccess.Read))
            {
                const char SPLASH = '\\';
                string title = "";

                this.CheckCredentialTimeStamp();
                if (uploadFileName.LastIndexOf(SPLASH) != -1)
                    title = uploadFileName.Substring(uploadFileName.LastIndexOf(SPLASH) + 1);
                else
                    title = uploadFileName;

                Google.Apis.Drive.v2.Data.File existingFile = GetFileByTitle(title);
                if (existingFile != null)
                {
                    // If the file exists, update it instead of creating a new one
                    Console.WriteLine("// If the file exists, update it instead of creating a new one");
                    return UpdateFile(uploadFileName, existingFile.Id, contentType);
                }
                else
                {
                    Console.WriteLine("wtf");
                    Google.Apis.Drive.v2.Data.File fileToInsert = new Google.Apis.Drive.v2.Data.File { Title = title };
                    FilesResource.InsertMediaUpload insertRequest = _service.Files.Insert(fileToInsert, uploadStream, contentType);
                    insertRequest.ChunkSize = FilesResource.InsertMediaUpload.MinimumChunkSize * 2;

                    if (uploadProgressEventHandeler != null)
                        insertRequest.ProgressChanged += uploadProgressEventHandeler;


                    if (responseReceivedEventHandler != null)
                        insertRequest.ResponseReceived += responseReceivedEventHandler;

                    try
                    {
                        insertRequest.Upload();
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                    finally
                    {
                        uploadStream.Close();
                    }

                    return insertRequest.ResponseBody;
                }
            }
        }



        /// <summary>
        /// 下載檔案
        /// </summary>
        /// <param name="fileToDownload">欲下載的檔案(Google Drive File) 一般會從List取得</param>
        /// <param name="downloadPath">下載到路徑</param>
        /// <param name="downloadProgressChangedEventHandeler">當下載進度改變時，呼叫這個函式</param>
        public void DownloadFile(Google.Apis.Drive.v2.Data.File fileToDownload, string downloadPath, Action<IDownloadProgress> downloadProgressChangedEventHandeler = null)
        {
            const string SPLASH = @"\";

            CheckCredentialTimeStamp();
            if (!String.IsNullOrEmpty(fileToDownload.DownloadUrl))
            {
                try
                {
                    Task<byte[]> downloadByte = _service.HttpClient.GetByteArrayAsync(fileToDownload.DownloadUrl);
                    byte[] byteArray = downloadByte.Result;
                    string downloadPosition = downloadPath + SPLASH + fileToDownload.Title;
                    System.IO.File.WriteAllBytes(downloadPosition, byteArray);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        /// <summary>
        /// 刪除符合FileID的檔案
        /// </summary>
        /// <param name="fileId">欲刪除檔案的FileID</param>
        public virtual void DeleteFile(string fileId)
        {
            CheckCredentialTimeStamp();
            try
            {
                _service.Files.Delete(fileId).Execute();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 更新指定FileID的檔案
        /// </summary>
        /// <param name="fileName">欲上傳至Google Drive並覆蓋在Google Drive上舊版檔案的檔案位置 </param>
        /// <param name="fileId">存在於Google Drive 舊版檔案的FileID </param>
        /// <param name="contentType">MIME Type</param>
        /// <returns>如更新成功，回傳更新後的Google Drive File</returns>
        public Google.Apis.Drive.v2.Data.File UpdateFile(string fileName, string fileId, string contentType)
        {
            CheckCredentialTimeStamp();
            try
            {
                Google.Apis.Drive.v2.Data.File file = _service.Files.Get(fileId).Execute();
                byte[] byteArray = System.IO.File.ReadAllBytes(fileName);
                MemoryStream stream = new MemoryStream(byteArray);
                FilesResource.UpdateMediaUpload request = _service.Files.Update(file, fileId, stream, contentType);
                request.NewRevision = true;
                request.Upload();

                Google.Apis.Drive.v2.Data.File updatedFile = request.ResponseBody;
                return updatedFile;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // find
        // Helper method to get a file by title
        public Google.Apis.Drive.v2.Data.File GetFileByTitle(string title)
        {
            FilesResource.ListRequest listRequest = _service.Files.List();
            listRequest.Q = "trashed=false"; // Exclude trashed files from the search

            FileList fileList = listRequest.Execute();

            foreach (var file in fileList.Items)
            {
                if (file.Title == title)
                {
                    return file; // Return the first file with the matching title
                }
            }

            return null; // Return null if no matching file is found
        }

        // get file id by title
        public string GetFileIdByTitle(string title)
        {
            FilesResource.ListRequest listRequest = _service.Files.List();
            listRequest.Q = "trashed=false"; // Exclude trashed files from the search

            FileList fileList = listRequest.Execute();

            foreach (var file in fileList.Items)
            {
                if (file.Title == title)
                {
                    return file.Id; // Return the first file with the matching title
                }
            }

            return ""; // Return null if no matching file is found
        }

        // download file and get content
        public string DownloadFileAndGetContent(string fileId)
        {
            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constant.Constant.MANY_FOLDER));
            string filePath = Path.Combine(solutionPath, Constant.Constant.PROJECT_NAME, Constant.Constant.BINARY, Constant.Constant.DEBUG, Constant.Constant.FILE_NAME);
            string downloadPath = Path.Combine(solutionPath, Constant.Constant.PROJECT_NAME, Constant.Constant.BINARY, Constant.Constant.DEBUG);


            // Download the file from Google Drive
            var fileToDownload = _service.Files.Get(fileId).Execute();

            //Console.WriteLine("whyhyyyy");
            DownloadFile(fileToDownload, downloadPath);

            //Console.WriteLine("whyhyyyy 2");

            // Deserialize JSON data
            string jsonContent = System.IO.File.ReadAllText(filePath);
            //var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonContent);

            //Console.WriteLine(jsonContent);

            //// Extract List<Shapes> and _panelMaxX from the JSON object
            //List<Shapes> shapesList = jsonObject["Shapes"].ToObject<List<Shapes>>();
            //int panelMaxX = jsonObject["PanelMaxX"].ToObject<int>();

            //Console.WriteLine(shapesList);
            //Console.WriteLine(panelMaxX);

            // Return the deserialized data
            return jsonContent;
        }
    }
}
