using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataGridDataBinding
{
    public partial class RaceCarDriverForm : Form
    {
        DriverManager driverManager;
        Random rand = new Random();
        public RaceCarDriverForm(DriverManager model)
        {
            InitializeComponent();
            this.driverManager = model;
            // Complex data binding
            // Note: 
            //    1. model.Drivers must implement IBindingList.
            //       Otherwise, adding/deleting elements to model.Drivers will not
            //       be reflected to dataGridView1.
            //    2. All public properties of model.Drivers will be displayed in the dataGridView.
            //       You can use Designer to adjust ordering of columns and make a specific column
            //           visible/invisible. Use the following steps:
            //       Click the top right > of dataGridView -> Choose Data Source -> Add Project Data
            //           -> object -> Expand to find DriverManager -> Finish
            //       Click the top right > of dataGridView -> Choose Data Source
            //           -> Expand DriverManagerBindingSource to find Drivers
            //       Click the top right > of dataGridView -> Edit Columns -> Modify ordering and visibility
            //    3. Designer only defines the dataGridView display properties. The real data binding
            //       should be done by the following code.
            dataGridView1.DataSource = driverManager.Drivers;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //
            // Add "刪除" column (DataGridViewButtonColumn)
            //
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Delete";
            buttonColumn.Text = "刪除";
            buttonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Insert(0, buttonColumn);
            //
            // Add the handler of "刪除" button
            //
            dataGridView1.CellContentClick += deleteCellButtonClick;
            //
            // Note: an example of binding combo box and check box can be found here:
            // https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-bind-objects-to-windows-forms-datagridview-controls?view=netframeworkdesktop-4.8
            //
        }

        private BindingManagerBase BindingManager {
            get { return BindingContext[driverManager.Drivers]; }
        }

        private void addDriverButtonClick(object sender, EventArgs e)
        {
            driverManager.AddDriver("Driver" + rand.Next(100).ToString(), rand.Next(1000));
            BindingManager.Position = BindingManager.Count - 1;
        }

        // 點擊"Delete Driver"時
        private void deleteDriverButtonClick(object sender, EventArgs e)
        {
            driverManager.DeleteDriver(BindingManager.Position);
            // Note: the following code also works, but it is NOT recommanded, 
            //       because we perfer the code to change Model not View.
            //dataGridView1.Rows.RemoveAt(BindingManager.Position);
        }

        // 點擊"刪除"cell時 
        private void deleteCellButtonClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0) // Note: header row is -1
                driverManager.DeleteDriver(e.RowIndex);
        }

        private void addWinsButtonClick(object sender, EventArgs e)
        {
            // Bad smell: avoid driverManager.Drivers.XXX
            // *** TRY THE FOLLOWING CODE ***
            //driverManager.Drivers[BindingManager.Position].AddWin();
            driverManager.AddWins(BindingManager.Position);
        }
    }
}
