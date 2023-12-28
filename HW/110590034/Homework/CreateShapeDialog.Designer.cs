
namespace Homework.View
{
    partial class CreateShapeDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._label1 = new System.Windows.Forms.Label();
            this._label2 = new System.Windows.Forms.Label();
            this._label3 = new System.Windows.Forms.Label();
            this._label4 = new System.Windows.Forms.Label();
            this._leftTextBox = new System.Windows.Forms.TextBox();
            this._topTextBox = new System.Windows.Forms.TextBox();
            this._rightTextBox = new System.Windows.Forms.TextBox();
            this._bottomTextBox = new System.Windows.Forms.TextBox();
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _label1
            // 
            this._label1.AutoSize = true;
            this._label1.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._label1.Location = new System.Drawing.Point(47, 31);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(129, 21);
            this._label1.TabIndex = 0;
            this._label1.Text = "左上角座標X";
            // 
            // _label2
            // 
            this._label2.AutoSize = true;
            this._label2.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._label2.Location = new System.Drawing.Point(218, 31);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(129, 21);
            this._label2.TabIndex = 1;
            this._label2.Text = "左上角座標Y";
            // 
            // _label3
            // 
            this._label3.AutoSize = true;
            this._label3.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._label3.Location = new System.Drawing.Point(47, 162);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(129, 21);
            this._label3.TabIndex = 2;
            this._label3.Text = "右下角座標X";
            // 
            // _label4
            // 
            this._label4.AutoSize = true;
            this._label4.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._label4.Location = new System.Drawing.Point(218, 162);
            this._label4.Name = "_label4";
            this._label4.Size = new System.Drawing.Size(129, 21);
            this._label4.TabIndex = 3;
            this._label4.Text = "右下角座標Y";
            // 
            // _leftTextBox
            // 
            this._leftTextBox.Location = new System.Drawing.Point(51, 69);
            this._leftTextBox.Name = "_leftTextBox";
            this._leftTextBox.Size = new System.Drawing.Size(125, 22);
            this._leftTextBox.TabIndex = 4;
            // 
            // _topTextBox
            // 
            this._topTextBox.Location = new System.Drawing.Point(222, 69);
            this._topTextBox.Name = "_topTextBox";
            this._topTextBox.Size = new System.Drawing.Size(125, 22);
            this._topTextBox.TabIndex = 5;
            // 
            // _rightTextBox
            // 
            this._rightTextBox.Location = new System.Drawing.Point(51, 204);
            this._rightTextBox.Name = "_rightTextBox";
            this._rightTextBox.Size = new System.Drawing.Size(125, 22);
            this._rightTextBox.TabIndex = 6;
            // 
            // _bottomTextBox
            // 
            this._bottomTextBox.Location = new System.Drawing.Point(222, 204);
            this._bottomTextBox.Name = "_bottomTextBox";
            this._bottomTextBox.Size = new System.Drawing.Size(125, 22);
            this._bottomTextBox.TabIndex = 7;
            // 
            // _okButton
            // 
            this._okButton.Font = new System.Drawing.Font("新細明體", 11F);
            this._okButton.Location = new System.Drawing.Point(78, 250);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 8;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Font = new System.Drawing.Font("新細明體", 11F);
            this._cancelButton.Location = new System.Drawing.Point(247, 250);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 9;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // CreateShapeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 313);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._bottomTextBox);
            this.Controls.Add(this._rightTextBox);
            this.Controls.Add(this._topTextBox);
            this.Controls.Add(this._leftTextBox);
            this.Controls.Add(this._label4);
            this.Controls.Add(this._label3);
            this.Controls.Add(this._label2);
            this.Controls.Add(this._label1);
            this.Name = "CreateShapeForm";
            this.Text = "CreateShape";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _label1;
        private System.Windows.Forms.Label _label2;
        private System.Windows.Forms.Label _label3;
        private System.Windows.Forms.Label _label4;
        private System.Windows.Forms.TextBox _leftTextBox;
        private System.Windows.Forms.TextBox _topTextBox;
        private System.Windows.Forms.TextBox _rightTextBox;
        private System.Windows.Forms.TextBox _bottomTextBox;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Button _cancelButton;
    }
}