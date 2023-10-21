using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBinding_SimpleControl
{
    public partial class Form1 : Form
    {
        PresentationModel presentationModel;
        public Form1(PresentationModel presentationModel)
        {
            this.presentationModel = presentationModel;
            InitializeComponent();
            addButton.Enabled = false;
        }

        private void categoryTextBox_TextChanged(object sender, EventArgs e)
        {
            // presentationModel可能有變化
            presentationModel.categoryTextBoxTextChanged(categoryTextBox.Text);
            // 不論presentationModel有沒有變化，一律更新畫面
            refreshControls();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            presentationModel.clickEditButton();
            // 不論presentationModel有沒有變化，一律更新畫面
            refreshControls();
        }

         private void refreshControls()
         {
             // 更新畫面：由presentationModel決定Enabled及Text
             addButton.Enabled = presentationModel.IsAddButtonEnabled;
             addButton.Text = presentationModel.AddButtonText;
         }

    }
}
