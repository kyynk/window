
namespace Homework
{
    partial class CreateShapeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._textBoxLeft = new System.Windows.Forms.TextBox();
            this._textBoxTop = new System.Windows.Forms.TextBox();
            this._textBoxRight = new System.Windows.Forms.TextBox();
            this._textBoxBottom = new System.Windows.Forms.TextBox();
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(131, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "左上角座標X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(517, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "左上角座標Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(131, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "右下角座標X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(517, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "右下角座標Y";
            // 
            // _textBoxLeft
            // 
            this._textBoxLeft.Location = new System.Drawing.Point(135, 145);
            this._textBoxLeft.Name = "_textBoxLeft";
            this._textBoxLeft.Size = new System.Drawing.Size(125, 22);
            this._textBoxLeft.TabIndex = 4;
            // 
            // _textBoxTop
            // 
            this._textBoxTop.Location = new System.Drawing.Point(521, 145);
            this._textBoxTop.Name = "_textBoxTop";
            this._textBoxTop.Size = new System.Drawing.Size(125, 22);
            this._textBoxTop.TabIndex = 5;
            // 
            // _textBoxRight
            // 
            this._textBoxRight.Location = new System.Drawing.Point(135, 278);
            this._textBoxRight.Name = "_textBoxRight";
            this._textBoxRight.Size = new System.Drawing.Size(125, 22);
            this._textBoxRight.TabIndex = 6;
            // 
            // _textBoxBottom
            // 
            this._textBoxBottom.Location = new System.Drawing.Point(521, 278);
            this._textBoxBottom.Name = "_textBoxBottom";
            this._textBoxBottom.Size = new System.Drawing.Size(125, 22);
            this._textBoxBottom.TabIndex = 7;
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(259, 381);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 8;
            this._okButton.Text = "ok";
            this._okButton.UseVisualStyleBackColor = true;
            // 
            // _cancelButton
            // 
            this._cancelButton.Location = new System.Drawing.Point(446, 381);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 9;
            this._cancelButton.Text = "cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // CreateShapeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._textBoxBottom);
            this.Controls.Add(this._textBoxRight);
            this.Controls.Add(this._textBoxTop);
            this.Controls.Add(this._textBoxLeft);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CreateShapeForm";
            this.Text = "CreateShape";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _textBoxLeft;
        private System.Windows.Forms.TextBox _textBoxTop;
        private System.Windows.Forms.TextBox _textBoxRight;
        private System.Windows.Forms.TextBox _textBoxBottom;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Button _cancelButton;
    }
}