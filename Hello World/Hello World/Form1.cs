using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hello_World
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            studentDataGridView.Rows.Add("110590034", "楊榮鈞");
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (IDTextBox.Text == "")
            {
                MessageBox.Show("Please enter ID");
            }
            else if (nameTextBox.Text == "")
            {
                MessageBox.Show("Please enter Name");
            }
            else
            {
                studentDataGridView.Rows.Add(IDTextBox.Text, nameTextBox.Text);
            }
        }
    }
}
