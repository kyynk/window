using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{
    public partial class Form1 : Form
    {
        private readonly Model _model;
        private const string DELETE = "刪除";

        public Form1(Model model)
        {
            InitializeComponent();
            _model = model;
        }

        // click create button
        private void ClickCreateButton(object sender, EventArgs e)
        {
            _model.Create(_shapeTypeComboBox.Text);
            _shapeData.Rows.Add(DELETE, _model.GetNewShapeType(), _model.GetNewShapePosition());
        }

        // click delete button
        private void ClickDeleteButton(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DataGridViewRow rowToDelete = _shapeData.Rows[e.RowIndex];
                _shapeData.Rows.Remove(rowToDelete);
                _model.Delete(e.RowIndex);
            }
        }
    }
}
