
namespace Homework
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this._groupBox = new System.Windows.Forms.GroupBox();
            this._shapeData = new System.Windows.Forms.DataGridView();
            this._shapeTypeComboBox = new System.Windows.Forms.ComboBox();
            this._createButton = new System.Windows.Forms.Button();
            this._canvases = new System.Windows.Forms.DataGridView();
            this._canvas1 = new System.Windows.Forms.Button();
            this._canvas2 = new System.Windows.Forms.Button();
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._description = new System.Windows.Forms.ToolStripMenuItem();
            this._about = new System.Windows.Forms.ToolStripMenuItem();
            this._deleteButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this._shapeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._information = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._shapeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._canvases)).BeginInit();
            this._menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _groupBox
            // 
            this._groupBox.Controls.Add(this._shapeData);
            this._groupBox.Controls.Add(this._shapeTypeComboBox);
            this._groupBox.Controls.Add(this._createButton);
            this._groupBox.Location = new System.Drawing.Point(795, 36);
            this._groupBox.Name = "_groupBox";
            this._groupBox.Size = new System.Drawing.Size(301, 557);
            this._groupBox.TabIndex = 0;
            this._groupBox.TabStop = false;
            this._groupBox.Text = "資料顯示";
            // 
            // _shapeData
            // 
            this._shapeData.AllowUserToResizeColumns = false;
            this._shapeData.AllowUserToResizeRows = false;
            this._shapeData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._shapeData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._deleteButton,
            this._shapeType,
            this._information});
            this._shapeData.Location = new System.Drawing.Point(6, 67);
            this._shapeData.Name = "_shapeData";
            this._shapeData.ReadOnly = true;
            this._shapeData.RowHeadersVisible = false;
            this._shapeData.RowHeadersWidth = 51;
            this._shapeData.RowTemplate.Height = 27;
            this._shapeData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._shapeData.Size = new System.Drawing.Size(289, 484);
            this._shapeData.TabIndex = 2;
            this._shapeData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClickDeleteButton);
            // 
            // _shapeTypeComboBox
            // 
            this._shapeTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._shapeTypeComboBox.FormattingEnabled = true;
            this._shapeTypeComboBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._shapeTypeComboBox.Items.AddRange(new object[] {
            "矩形",
            "線"});
            this._shapeTypeComboBox.Location = new System.Drawing.Point(101, 24);
            this._shapeTypeComboBox.Name = "_shapeTypeComboBox";
            this._shapeTypeComboBox.Size = new System.Drawing.Size(156, 23);
            this._shapeTypeComboBox.TabIndex = 1;
            // 
            // _createButton
            // 
            this._createButton.Location = new System.Drawing.Point(6, 24);
            this._createButton.Name = "_createButton";
            this._createButton.Size = new System.Drawing.Size(64, 37);
            this._createButton.TabIndex = 0;
            this._createButton.Text = "新增";
            this._createButton.UseVisualStyleBackColor = true;
            this._createButton.Click += new System.EventHandler(this.ClickCreateButton);
            // 
            // _canvases
            // 
            this._canvases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._canvases.Location = new System.Drawing.Point(3, 36);
            this._canvases.Name = "_canvases";
            this._canvases.RowHeadersWidth = 51;
            this._canvases.RowTemplate.Height = 27;
            this._canvases.Size = new System.Drawing.Size(141, 557);
            this._canvases.TabIndex = 1;
            // 
            // _canvas1
            // 
            this._canvas1.Location = new System.Drawing.Point(12, 49);
            this._canvas1.Name = "_canvas1";
            this._canvas1.Size = new System.Drawing.Size(121, 70);
            this._canvas1.TabIndex = 2;
            this._canvas1.UseVisualStyleBackColor = true;
            // 
            // _canvas2
            // 
            this._canvas2.Location = new System.Drawing.Point(12, 135);
            this._canvas2.Name = "_canvas2";
            this._canvas2.Size = new System.Drawing.Size(121, 70);
            this._canvas2.TabIndex = 3;
            this._canvas2.UseVisualStyleBackColor = true;
            // 
            // _menuStrip
            // 
            this._menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._description});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(1108, 27);
            this._menuStrip.TabIndex = 4;
            this._menuStrip.Text = "menuStrip1";
            // 
            // _description
            // 
            this._description.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._about});
            this._description.Name = "_description";
            this._description.Size = new System.Drawing.Size(53, 23);
            this._description.Text = "說明";
            // 
            // _about
            // 
            this._about.Name = "_about";
            this._about.Size = new System.Drawing.Size(122, 26);
            this._about.Text = "關於";
            // 
            // _deleteButton
            // 
            this._deleteButton.FillWeight = 50F;
            this._deleteButton.HeaderText = "刪除";
            this._deleteButton.MinimumWidth = 6;
            this._deleteButton.Name = "_deleteButton";
            this._deleteButton.ReadOnly = true;
            this._deleteButton.Width = 50;
            // 
            // _shapeType
            // 
            this._shapeType.FillWeight = 75F;
            this._shapeType.HeaderText = "形狀";
            this._shapeType.MinimumWidth = 6;
            this._shapeType.Name = "_shapeType";
            this._shapeType.ReadOnly = true;
            this._shapeType.Width = 75;
            // 
            // _information
            // 
            this._information.FillWeight = 150F;
            this._information.HeaderText = "資訊";
            this._information.MinimumWidth = 6;
            this._information.Name = "_information";
            this._information.ReadOnly = true;
            this._information.Width = 150;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 605);
            this.Controls.Add(this._canvas2);
            this.Controls.Add(this._canvas1);
            this.Controls.Add(this._canvases);
            this.Controls.Add(this._groupBox);
            this.Controls.Add(this._menuStrip);
            this.MainMenuStrip = this._menuStrip;
            this.Name = "Form1";
            this.Text = "Form1";
            this._groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._shapeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._canvases)).EndInit();
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _groupBox;
        private System.Windows.Forms.ComboBox _shapeTypeComboBox;
        private System.Windows.Forms.Button _createButton;
        private System.Windows.Forms.DataGridView _shapeData;
        private System.Windows.Forms.DataGridView _canvases;
        private System.Windows.Forms.Button _canvas1;
        private System.Windows.Forms.Button _canvas2;
        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _description;
        private System.Windows.Forms.ToolStripMenuItem _about;
        private System.Windows.Forms.DataGridViewButtonColumn _deleteButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn _shapeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn _information;
    }
}

