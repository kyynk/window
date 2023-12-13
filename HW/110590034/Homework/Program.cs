using System;
using System.Windows.Forms;
using Homework.PresentationModel;

namespace Homework.View
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Model.Model model = new Model.Model();
            FormPresentationModel pModel = new FormPresentationModel(model);
            Form1 form = new Form1(pModel);
            Application.Run(form);
        }
    }
}
