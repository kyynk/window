namespace Homework.View
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
            this._deleteButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this._shapeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._information = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._groupBox1 = new System.Windows.Forms.GroupBox();
            this._createButton = new System.Windows.Forms.Button();
            this._shapeTypeComboBox = new System.Windows.Forms.ComboBox();
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._description = new System.Windows.Forms.ToolStripMenuItem();
            this._about = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._addPageButton = new System.Windows.Forms.ToolStripButton();
            this._saveButton = new System.Windows.Forms.ToolStripButton();
            this._loadButton = new System.Windows.Forms.ToolStripButton();
            this._splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._canvas = new Homework.View.DoubleBufferedPanel();
            this._lineButton = new Homework.View.ToolStripBindingButton();
            this._rectangleButton = new Homework.View.ToolStripBindingButton();
            this._ellipseButton = new Homework.View.ToolStripBindingButton();
            this._defaultCursorButton = new Homework.View.ToolStripBindingButton();
            this._undoButton = new Homework.View.ToolStripBindingButton();
            this._redoButton = new Homework.View.ToolStripBindingButton();
            this._groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._shapeData)).BeginInit();
            this._groupBox1.SuspendLayout();
            this._menuStrip.SuspendLayout();
            this._toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer1)).BeginInit();
            this._splitContainer1.Panel1.SuspendLayout();
            this._splitContainer1.Panel2.SuspendLayout();
            this._splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer2)).BeginInit();
            this._splitContainer2.Panel1.SuspendLayout();
            this._splitContainer2.Panel2.SuspendLayout();
            this._splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _groupBox
            // 
            this._groupBox.Controls.Add(this._shapeData);
            this._groupBox.Controls.Add(this._groupBox1);
            this._groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBox.Location = new System.Drawing.Point(0, 0);
            this._groupBox.Margin = new System.Windows.Forms.Padding(2);
            this._groupBox.Name = "_groupBox";
            this._groupBox.Padding = new System.Windows.Forms.Padding(2);
            this._groupBox.Size = new System.Drawing.Size(243, 433);
            this._groupBox.TabIndex = 0;
            this._groupBox.TabStop = false;
            this._groupBox.Text = "資料顯示";
            // 
            // _shapeData
            // 
            this._shapeData.AllowUserToAddRows = false;
            this._shapeData.AllowUserToResizeColumns = false;
            this._shapeData.AllowUserToResizeRows = false;
            this._shapeData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._shapeData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._deleteButton,
            this._shapeType,
            this._information});
            this._shapeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._shapeData.Location = new System.Drawing.Point(2, 66);
            this._shapeData.Margin = new System.Windows.Forms.Padding(2);
            this._shapeData.Name = "_shapeData";
            this._shapeData.ReadOnly = true;
            this._shapeData.RowHeadersVisible = false;
            this._shapeData.RowHeadersWidth = 51;
            this._shapeData.RowTemplate.Height = 27;
            this._shapeData.Size = new System.Drawing.Size(239, 365);
            this._shapeData.TabIndex = 0;
            this._shapeData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClickDeleteButton);
            // 
            // _deleteButton
            // 
            this._deleteButton.FillWeight = 50F;
            this._deleteButton.HeaderText = "刪除";
            this._deleteButton.MinimumWidth = 6;
            this._deleteButton.Name = "_deleteButton";
            this._deleteButton.ReadOnly = true;
            this._deleteButton.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._deleteButton.Text = "刪除";
            this._deleteButton.UseColumnTextForButtonValue = true;
            this._deleteButton.Width = 50;
            // 
            // _shapeType
            // 
            this._shapeType.FillWeight = 75F;
            this._shapeType.HeaderText = "形狀";
            this._shapeType.MinimumWidth = 6;
            this._shapeType.Name = "_shapeType";
            this._shapeType.ReadOnly = true;
            this._shapeType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._shapeType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._shapeType.Width = 75;
            // 
            // _information
            // 
            this._information.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._information.FillWeight = 150F;
            this._information.HeaderText = "資訊";
            this._information.MinimumWidth = 6;
            this._information.Name = "_information";
            this._information.ReadOnly = true;
            this._information.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._information.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _groupBox1
            // 
            this._groupBox1.BackColor = System.Drawing.Color.Gainsboro;
            this._groupBox1.Controls.Add(this._createButton);
            this._groupBox1.Controls.Add(this._shapeTypeComboBox);
            this._groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this._groupBox1.Location = new System.Drawing.Point(2, 17);
            this._groupBox1.Name = "_groupBox1";
            this._groupBox1.Size = new System.Drawing.Size(239, 49);
            this._groupBox1.TabIndex = 2;
            this._groupBox1.TabStop = false;
            // 
            // _createButton
            // 
            this._createButton.Location = new System.Drawing.Point(5, 2);
            this._createButton.Margin = new System.Windows.Forms.Padding(2);
            this._createButton.Name = "_createButton";
            this._createButton.Size = new System.Drawing.Size(55, 42);
            this._createButton.TabIndex = 0;
            this._createButton.Text = "新增";
            this._createButton.UseVisualStyleBackColor = true;
            this._createButton.Click += new System.EventHandler(this.ClickCreateButton);
            // 
            // _shapeTypeComboBox
            // 
            this._shapeTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._shapeTypeComboBox.FormattingEnabled = true;
            this._shapeTypeComboBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._shapeTypeComboBox.Items.AddRange(new object[] {
            "矩形",
            "線",
            "圓"});
            this._shapeTypeComboBox.Location = new System.Drawing.Point(77, 14);
            this._shapeTypeComboBox.Margin = new System.Windows.Forms.Padding(2);
            this._shapeTypeComboBox.Name = "_shapeTypeComboBox";
            this._shapeTypeComboBox.Size = new System.Drawing.Size(118, 20);
            this._shapeTypeComboBox.TabIndex = 1;
            // 
            // _menuStrip
            // 
            this._menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._description});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this._menuStrip.Size = new System.Drawing.Size(849, 24);
            this._menuStrip.TabIndex = 4;
            this._menuStrip.Text = "menuStrip1";
            // 
            // _description
            // 
            this._description.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._about});
            this._description.Name = "_description";
            this._description.Size = new System.Drawing.Size(43, 20);
            this._description.Text = "說明";
            // 
            // _about
            // 
            this._about.Name = "_about";
            this._about.Size = new System.Drawing.Size(98, 22);
            this._about.Text = "關於";
            // 
            // _toolStrip
            // 
            this._toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lineButton,
            this._rectangleButton,
            this._ellipseButton,
            this._defaultCursorButton,
            this._addPageButton,
            this._undoButton,
            this._redoButton,
            this._saveButton,
            this._loadButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 24);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(849, 27);
            this._toolStrip.TabIndex = 5;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _addPageButton
            // 
            this._addPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._addPageButton.Image = global::Homework.Properties.Resources.AddPage;
            this._addPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._addPageButton.Name = "_addPageButton";
            this._addPageButton.Size = new System.Drawing.Size(24, 24);
            this._addPageButton.Text = "_addPageButton";
            this._addPageButton.ToolTipText = "_addPageButton";
            this._addPageButton.Click += new System.EventHandler(this.ClickAddPageButton);
            // 
            // _saveButton
            // 
            this._saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._saveButton.Image = global::Homework.Properties.Resources.Save;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(24, 24);
            this._saveButton.Text = "_saveButton";
            this._saveButton.ToolTipText = "_saveButton";
            // 
            // _loadButton
            // 
            this._loadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._loadButton.Image = global::Homework.Properties.Resources.Download;
            this._loadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._loadButton.Name = "_loadButton";
            this._loadButton.Size = new System.Drawing.Size(24, 24);
            this._loadButton.Text = "_loadButton";
            this._loadButton.ToolTipText = "_loadButton";
            // 
            // _splitContainer1
            // 
            this._splitContainer1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this._splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._splitContainer1.Location = new System.Drawing.Point(0, 51);
            this._splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this._splitContainer1.Name = "_splitContainer1";
            // 
            // _splitContainer1.Panel1
            // 
            this._splitContainer1.Panel1.BackColor = System.Drawing.Color.Silver;
            this._splitContainer1.Panel1.Controls.Add(this._flowLayoutPanel);
            // 
            // _splitContainer1.Panel2
            // 
            this._splitContainer1.Panel2.Controls.Add(this._splitContainer2);
            this._splitContainer1.Panel2MinSize = 300;
            this._splitContainer1.Size = new System.Drawing.Size(849, 433);
            this._splitContainer1.SplitterDistance = 116;
            this._splitContainer1.SplitterWidth = 3;
            this._splitContainer1.TabIndex = 6;
            // 
            // _flowLayoutPanel
            // 
            this._flowLayoutPanel.AccessibleName = "flowLayoutPanel";
            this._flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._flowLayoutPanel.Name = "_flowLayoutPanel";
            this._flowLayoutPanel.Size = new System.Drawing.Size(116, 433);
            this._flowLayoutPanel.TabIndex = 3;
            // 
            // _splitContainer2
            // 
            this._splitContainer2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this._splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this._splitContainer2.Location = new System.Drawing.Point(0, 0);
            this._splitContainer2.Margin = new System.Windows.Forms.Padding(2);
            this._splitContainer2.MinimumSize = new System.Drawing.Size(250, 90);
            this._splitContainer2.Name = "_splitContainer2";
            // 
            // _splitContainer2.Panel1
            // 
            this._splitContainer2.Panel1.BackColor = System.Drawing.Color.LightGray;
            this._splitContainer2.Panel1.Controls.Add(this._canvas);
            this._splitContainer2.Panel1MinSize = 50;
            // 
            // _splitContainer2.Panel2
            // 
            this._splitContainer2.Panel2.BackColor = System.Drawing.Color.Gainsboro;
            this._splitContainer2.Panel2.Controls.Add(this._groupBox);
            this._splitContainer2.Panel2MinSize = 200;
            this._splitContainer2.Size = new System.Drawing.Size(730, 433);
            this._splitContainer2.SplitterDistance = 484;
            this._splitContainer2.SplitterWidth = 3;
            this._splitContainer2.TabIndex = 0;
            // 
            // _canvas
            // 
            this._canvas.AccessibleName = "panel";
            this._canvas.BackColor = System.Drawing.Color.White;
            this._canvas.Location = new System.Drawing.Point(16, 85);
            this._canvas.Margin = new System.Windows.Forms.Padding(2);
            this._canvas.Name = "_canvas";
            this._canvas.Size = new System.Drawing.Size(448, 252);
            this._canvas.TabIndex = 0;
            // 
            // _lineButton
            // 
            this._lineButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._lineButton.Image = global::Homework.Properties.Resources.Line;
            this._lineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._lineButton.Name = "_lineButton";
            this._lineButton.Size = new System.Drawing.Size(24, 24);
            this._lineButton.Text = "_lineButton";
            this._lineButton.ToolTipText = "_lineButton";
            this._lineButton.Click += new System.EventHandler(this.ClickLineButton);
            // 
            // _rectangleButton
            // 
            this._rectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._rectangleButton.Image = global::Homework.Properties.Resources.Rectangle;
            this._rectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._rectangleButton.Name = "_rectangleButton";
            this._rectangleButton.Size = new System.Drawing.Size(24, 24);
            this._rectangleButton.Text = "_rectangleButton";
            this._rectangleButton.ToolTipText = "_rectangleButton";
            this._rectangleButton.Click += new System.EventHandler(this.ClickRectangleButton);
            // 
            // _ellipseButton
            // 
            this._ellipseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ellipseButton.Image = global::Homework.Properties.Resources.Ellipse;
            this._ellipseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ellipseButton.Name = "_ellipseButton";
            this._ellipseButton.Size = new System.Drawing.Size(24, 24);
            this._ellipseButton.Text = "_ellipseButton";
            this._ellipseButton.ToolTipText = "_ellipseButton";
            this._ellipseButton.Click += new System.EventHandler(this.ClickEllipseButton);
            // 
            // _defaultCursorButton
            // 
            this._defaultCursorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._defaultCursorButton.Image = global::Homework.Properties.Resources.DefaultCursor;
            this._defaultCursorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._defaultCursorButton.Name = "_defaultCursorButton";
            this._defaultCursorButton.Size = new System.Drawing.Size(24, 24);
            this._defaultCursorButton.Text = "_defaultCursorButton";
            this._defaultCursorButton.ToolTipText = "_defaultCursorButton";
            this._defaultCursorButton.Click += new System.EventHandler(this.ClickDefaultCursorButton);
            // 
            // _undoButton
            // 
            this._undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._undoButton.Image = global::Homework.Properties.Resources.Undo2;
            this._undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._undoButton.Name = "_undoButton";
            this._undoButton.Size = new System.Drawing.Size(24, 24);
            this._undoButton.Text = "_undoButton";
            this._undoButton.ToolTipText = "_undoButton";
            this._undoButton.Click += new System.EventHandler(this.ClickUndoButton);
            // 
            // _redoButton
            // 
            this._redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._redoButton.Image = global::Homework.Properties.Resources.Redo2;
            this._redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._redoButton.Name = "_redoButton";
            this._redoButton.Size = new System.Drawing.Size(24, 24);
            this._redoButton.Text = "_redoButton";
            this._redoButton.ToolTipText = "_redoButton";
            this._redoButton.Click += new System.EventHandler(this.ClickRedoButton);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 484);
            this.Controls.Add(this._splitContainer1);
            this.Controls.Add(this._toolStrip);
            this.Controls.Add(this._menuStrip);
            this.MainMenuStrip = this._menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this._groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._shapeData)).EndInit();
            this._groupBox1.ResumeLayout(false);
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this._splitContainer1.Panel1.ResumeLayout(false);
            this._splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer1)).EndInit();
            this._splitContainer1.ResumeLayout(false);
            this._splitContainer2.Panel1.ResumeLayout(false);
            this._splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer2)).EndInit();
            this._splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _groupBox;
        private System.Windows.Forms.ComboBox _shapeTypeComboBox;
        private System.Windows.Forms.Button _createButton;
        private System.Windows.Forms.DataGridView _shapeData;
        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _description;
        private System.Windows.Forms.ToolStripMenuItem _about;
        private Homework.View.ToolStripBindingButton _lineButton;
        private Homework.View.ToolStripBindingButton _rectangleButton;
        private Homework.View.ToolStripBindingButton _ellipseButton;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private Homework.View.DoubleBufferedPanel _canvas;
        private Homework.View.ToolStripBindingButton _defaultCursorButton;
        private System.Windows.Forms.SplitContainer _splitContainer1;
        private System.Windows.Forms.SplitContainer _splitContainer2;
        private Homework.View.ToolStripBindingButton _undoButton;
        private Homework.View.ToolStripBindingButton _redoButton;
        private System.Windows.Forms.GroupBox _groupBox1;
        private System.Windows.Forms.DataGridViewButtonColumn _deleteButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn _shapeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn _information;
        private System.Windows.Forms.ToolStripButton _addPageButton;
        private System.Windows.Forms.FlowLayoutPanel _flowLayoutPanel;
        private System.Windows.Forms.ToolStripButton _saveButton;
        private System.Windows.Forms.ToolStripButton _loadButton;
    }
}

