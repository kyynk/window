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
        // save
        public virtual async Task Save()
        {
            // Convert List<Shapes> and _panelMaxX to JSON
            string shapesJson = JsonConvert.SerializeObject(_pages.GetPages(), Formatting.Indented);
            string panelMaxXJson = JsonConvert.SerializeObject(_panelMaxX, Formatting.Indented);

            // Combine List<Shapes> and _panelMaxX into a single JSON object
            JObject jsonToUpload = new JObject
            {
                ["Shapes"] = JToken.Parse(shapesJson),
                ["PanelMaxX"] = JToken.Parse(panelMaxXJson)
            };

            // Create a temporary file to store JSON
            string tempFilePath = "110590034.json"; // You can use a unique file name

            // Write JSON to the temporary file
            File.WriteAllText(tempFilePath, jsonToUpload.ToString());

            try
            {
                // Upload the JSON file to Google Drive
                var uploadedFile = await _drive.UploadFileAsync(tempFilePath, "application/json");

                // Output the file ID and other information if needed
                Console.WriteLine("File ID: " + uploadedFile.Id);
                Console.WriteLine("File Title: " + uploadedFile.Title);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
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


    }
}
