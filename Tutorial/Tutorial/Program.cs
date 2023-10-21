using System.Windows.Forms;
namespace Tutorial
{
    class MainEntry
    {
        // main
        static void Main(string[] args)
        {
            Form form = new ElfinForm();
            Application.Run(form);
        }
    }
}