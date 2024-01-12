using System;
using System.ComponentModel;
using System.Windows.Forms;
using Homework.State;
using Homework.Command;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace Homework.Model
{
    public partial class Model
    {
        public delegate void FormEnabledHandler(bool isEnabled);
        public event FormEnabledHandler _formEnabled;
        private string _fileId;
        const string SHAPES = "Shapes";
        const string WIDTH = "PanelMaxX";
        const string SHAPE_LIST = "ShapeList";
        const string SHAPE_NAME = "ShapeName";
        const string STRING_X = "X";
        const string STRING_Y = "Y";

        // save
        public virtual async Task Save()
        {
            // Check if there is a network connection
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                

                // Convert List<Shapes> and _panelMaxX to JSON
                string shapesJson = JsonConvert.SerializeObject(_pages.GetPages(), Formatting.Indented);
                string panelMaxXJson = JsonConvert.SerializeObject(_panelMaxX, Formatting.Indented);

                // Combine List<Shapes> and _panelMaxX into a single JSON object
                JObject jsonToUpload = new JObject
                {
                    [SHAPES] = JToken.Parse(shapesJson),
                    [WIDTH] = JToken.Parse(panelMaxXJson)
                };

                // Create a temporary file to store JSON
                const string tempFilePath = "110590034.json"; // You can use a unique file name

                // Write JSON to the temporary file
                File.WriteAllText(Constant.Constant.FILE_NAME, jsonToUpload.ToString());

                try
                {
                    // Upload the JSON file to Google Drive
                    var uploadedFile = await _drive.UploadFileAsync(Constant.Constant.FILE_NAME, Constant.Constant.FILE_TYPE);

                    // Output the file ID and other information if needed
                    //Console.WriteLine("File ID: " + uploadedFile.Id);
                    //Console.WriteLine("File Title: " + uploadedFile.Title);
                    _fileId = uploadedFile.Id;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    // Delete the temporary file after uploading to Google Drive
                    if (File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                    }
                }
                Thread.Sleep(Constant.Constant.DELAY_TIME);
            }
            else
            {
                //Console.WriteLine("No network connection available.");
                MessageBox.Show(Constant.Constant.NETWORK);
            }
        }

        // before load
        public void ClearNow()
        {
            // Use the loaded data as needed
            int length = _pages.GetPages().Count;
            //Console.WriteLine("line 69   len: " + length);
            InsertPageByIndex(new Shapes(), 0);

            for (int i = 0; i < length; i++)
            {
                RemovePageByIndex(1);
                //Console.WriteLine("for loop");
            }
            _commandManager.AllClear();
        }

        // reload shapes
        public void ReloadPages(string jsonString)
        {
            JObject jsonObject = JObject.Parse(jsonString);

            // Extract PanelMaxX
            int panelMaxX = jsonObject[WIDTH].Value<int>();
            //Console.WriteLine("PanelMaxX: " + panelMaxX);

            //Console.WriteLine("intit slide index " + PageIndex);
            // Extract Shape information
            foreach (var shapeList in jsonObject[SHAPES])
            {
                Shapes shapes = new Shapes();
                foreach (var shape in shapeList[SHAPE_LIST])
                {
                    const string POINT_ONE = "Point1";
                    const string POINT_TWO = "Point2";
                    string shapeName = shape[SHAPE_NAME].Value<string>();
                    double x1 = shape[POINT_ONE][STRING_X].Value<double>();
                    double y1 = shape[POINT_ONE][STRING_Y].Value<double>();
                    double x2 = shape[POINT_TWO][STRING_X].Value<double>();
                    double y2 = shape[POINT_TWO][STRING_Y].Value<double>();

                    //Console.WriteLine($"ShapeName: {shapeName}, Point1: ({x1}, {y1}), Point2: ({x2}, {y2})");
                    shapes.InsertShapeByIndex(_shapeFactory.CreateShape(shapeName, new Point(x1, y1), new Point(x2, y2)), shapes.ShapeList.Count);
                }
                shapes.ResizeForPanel((double)_panelMaxX / (double)panelMaxX);
                InsertPageByIndex(shapes, _pages.GetPages().Count);
                //Console.WriteLine("loop slide index " + PageIndex);
            }
            //Console.WriteLine("wait " + PageIndex);
            SelectPage(0);
            RemovePage();
            //Console.WriteLine("after rm slide index " + PageIndex);
            _commandManager.AllClear();
            _formEnabled(true);
        }

        // load
        public virtual void Load()
        {
            // Check if there is a network connection
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                //Console.WriteLine("search 1: " + _fileId);
                _fileId = _drive.GetFileIdByTitle(Constant.Constant.FILE_NAME);
                //Console.WriteLine("search 2: " + _fileId);

                if (_fileId != "")
                {
                    _formEnabled(false);
                    // Now you have the fileId, you can download and deserialize the file
                    var information = _drive.DownloadFileAndGetContent(_fileId);
                    ClearNow();
                    ReloadPages(information);
                    SelectPage(0);
                }
                else
                {
                    // File with the specified title not found
                    //Console.WriteLine("File not found on Google Drive.");
                    const string FILE_WRONG = "cannot load file, pls save first then load";
                    MessageBox.Show(FILE_WRONG);
                }
            }
            else
            {
                MessageBox.Show(Constant.Constant.NETWORK);
            }
        }

        // delete drive file
        public virtual void DeleteDriveFile()
        {
            if (_fileId == null)
                return;
            _drive.DeleteFile(_fileId);
        }
    }
}
