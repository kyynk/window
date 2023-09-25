
namespace Homework1
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
            this._textBoxResult = new System.Windows.Forms.TextBox();
            this._buttonClearEntry = new System.Windows.Forms.Button();
            this._buttonClear = new System.Windows.Forms.Button();
            this._button7 = new System.Windows.Forms.Button();
            this._button8 = new System.Windows.Forms.Button();
            this._button9 = new System.Windows.Forms.Button();
            this._buttonPlus = new System.Windows.Forms.Button();
            this._button4 = new System.Windows.Forms.Button();
            this._button5 = new System.Windows.Forms.Button();
            this._button6 = new System.Windows.Forms.Button();
            this._buttonMinus = new System.Windows.Forms.Button();
            this._button1 = new System.Windows.Forms.Button();
            this._button2 = new System.Windows.Forms.Button();
            this._button3 = new System.Windows.Forms.Button();
            this._buttonMultiply = new System.Windows.Forms.Button();
            this._button0 = new System.Windows.Forms.Button();
            this._buttonPoint = new System.Windows.Forms.Button();
            this._buttonEqual = new System.Windows.Forms.Button();
            this._buttonDivide = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _textBoxResult
            // 
            this._textBoxResult.Font = new System.Drawing.Font("Ink Free", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._textBoxResult.Location = new System.Drawing.Point(12, 37);
            this._textBoxResult.Name = "_textBoxResult";
            this._textBoxResult.ReadOnly = true;
            this._textBoxResult.Size = new System.Drawing.Size(311, 61);
            this._textBoxResult.TabIndex = 0;
            this._textBoxResult.Text = "0";
            this._textBoxResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _buttonClearEntry
            // 
            this._buttonClearEntry.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonClearEntry.Location = new System.Drawing.Point(178, 114);
            this._buttonClearEntry.Name = "_buttonClearEntry";
            this._buttonClearEntry.Size = new System.Drawing.Size(60, 60);
            this._buttonClearEntry.TabIndex = 3;
            this._buttonClearEntry.Text = "CE";
            this._buttonClearEntry.UseVisualStyleBackColor = true;
            this._buttonClearEntry.Click += new System.EventHandler(this.ClickButtonClearEntry);
            // 
            // _buttonClear
            // 
            this._buttonClear.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonClear.Location = new System.Drawing.Point(263, 114);
            this._buttonClear.Name = "_buttonClear";
            this._buttonClear.Size = new System.Drawing.Size(60, 60);
            this._buttonClear.TabIndex = 4;
            this._buttonClear.Text = "C";
            this._buttonClear.UseVisualStyleBackColor = true;
            this._buttonClear.Click += new System.EventHandler(this.ClickButtonClear);
            // 
            // _button7
            // 
            this._button7.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._button7.Location = new System.Drawing.Point(12, 192);
            this._button7.Name = "_button7";
            this._button7.Size = new System.Drawing.Size(60, 60);
            this._button7.TabIndex = 5;
            this._button7.Text = "7";
            this._button7.UseVisualStyleBackColor = true;
            this._button7.Click += new System.EventHandler(this.ClickButtonNumber);
            // 
            // _button8
            // 
            this._button8.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._button8.Location = new System.Drawing.Point(94, 192);
            this._button8.Name = "_button8";
            this._button8.Size = new System.Drawing.Size(60, 60);
            this._button8.TabIndex = 6;
            this._button8.Text = "8";
            this._button8.UseVisualStyleBackColor = true;
            this._button8.Click += new System.EventHandler(this.ClickButtonNumber);
            // 
            // _button9
            // 
            this._button9.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._button9.Location = new System.Drawing.Point(178, 192);
            this._button9.Name = "_button9";
            this._button9.Size = new System.Drawing.Size(60, 60);
            this._button9.TabIndex = 7;
            this._button9.Text = "9";
            this._button9.UseVisualStyleBackColor = true;
            this._button9.Click += new System.EventHandler(this.ClickButtonNumber);
            // 
            // _buttonPlus
            // 
            this._buttonPlus.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonPlus.Location = new System.Drawing.Point(263, 192);
            this._buttonPlus.Name = "_buttonPlus";
            this._buttonPlus.Size = new System.Drawing.Size(60, 60);
            this._buttonPlus.TabIndex = 8;
            this._buttonPlus.Text = "+";
            this._buttonPlus.UseVisualStyleBackColor = true;
            this._buttonPlus.Click += new System.EventHandler(this.ClickButtonOperation);
            // 
            // _button4
            // 
            this._button4.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._button4.Location = new System.Drawing.Point(12, 272);
            this._button4.Name = "_button4";
            this._button4.Size = new System.Drawing.Size(60, 60);
            this._button4.TabIndex = 9;
            this._button4.Text = "4";
            this._button4.UseVisualStyleBackColor = true;
            this._button4.Click += new System.EventHandler(this.ClickButtonNumber);
            // 
            // _button5
            // 
            this._button5.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._button5.Location = new System.Drawing.Point(94, 272);
            this._button5.Name = "_button5";
            this._button5.Size = new System.Drawing.Size(60, 60);
            this._button5.TabIndex = 10;
            this._button5.Text = "5";
            this._button5.UseVisualStyleBackColor = true;
            this._button5.Click += new System.EventHandler(this.ClickButtonNumber);
            // 
            // _button6
            // 
            this._button6.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._button6.Location = new System.Drawing.Point(178, 272);
            this._button6.Name = "_button6";
            this._button6.Size = new System.Drawing.Size(60, 60);
            this._button6.TabIndex = 11;
            this._button6.Text = "6";
            this._button6.UseVisualStyleBackColor = true;
            this._button6.Click += new System.EventHandler(this.ClickButtonNumber);
            // 
            // _buttonMinus
            // 
            this._buttonMinus.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonMinus.Location = new System.Drawing.Point(263, 272);
            this._buttonMinus.Name = "_buttonMinus";
            this._buttonMinus.Size = new System.Drawing.Size(60, 60);
            this._buttonMinus.TabIndex = 12;
            this._buttonMinus.Text = "-";
            this._buttonMinus.UseVisualStyleBackColor = true;
            this._buttonMinus.Click += new System.EventHandler(this.ClickButtonOperation);
            // 
            // _button1
            // 
            this._button1.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._button1.Location = new System.Drawing.Point(12, 352);
            this._button1.Name = "_button1";
            this._button1.Size = new System.Drawing.Size(60, 60);
            this._button1.TabIndex = 13;
            this._button1.Text = "1";
            this._button1.UseVisualStyleBackColor = true;
            this._button1.Click += new System.EventHandler(this.ClickButtonNumber);
            // 
            // _button2
            // 
            this._button2.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._button2.Location = new System.Drawing.Point(94, 352);
            this._button2.Name = "_button2";
            this._button2.Size = new System.Drawing.Size(60, 60);
            this._button2.TabIndex = 14;
            this._button2.Text = "2";
            this._button2.UseVisualStyleBackColor = true;
            this._button2.Click += new System.EventHandler(this.ClickButtonNumber);
            // 
            // _button3
            // 
            this._button3.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._button3.Location = new System.Drawing.Point(178, 352);
            this._button3.Name = "_button3";
            this._button3.Size = new System.Drawing.Size(60, 60);
            this._button3.TabIndex = 15;
            this._button3.Text = "3";
            this._button3.UseVisualStyleBackColor = true;
            this._button3.Click += new System.EventHandler(this.ClickButtonNumber);
            // 
            // _buttonMultiply
            // 
            this._buttonMultiply.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonMultiply.Location = new System.Drawing.Point(263, 352);
            this._buttonMultiply.Name = "_buttonMultiply";
            this._buttonMultiply.Size = new System.Drawing.Size(60, 60);
            this._buttonMultiply.TabIndex = 16;
            this._buttonMultiply.Text = "*";
            this._buttonMultiply.UseVisualStyleBackColor = true;
            this._buttonMultiply.Click += new System.EventHandler(this.ClickButtonOperation);
            // 
            // _button0
            // 
            this._button0.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._button0.Location = new System.Drawing.Point(12, 433);
            this._button0.Name = "_button0";
            this._button0.Size = new System.Drawing.Size(60, 60);
            this._button0.TabIndex = 17;
            this._button0.Text = "0";
            this._button0.UseVisualStyleBackColor = true;
            this._button0.Click += new System.EventHandler(this.ClickButtonNumber);
            // 
            // _buttonPoint
            // 
            this._buttonPoint.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonPoint.Location = new System.Drawing.Point(94, 433);
            this._buttonPoint.Name = "_buttonPoint";
            this._buttonPoint.Size = new System.Drawing.Size(60, 60);
            this._buttonPoint.TabIndex = 18;
            this._buttonPoint.Text = ".";
            this._buttonPoint.UseVisualStyleBackColor = true;
            this._buttonPoint.Click += new System.EventHandler(this.ClickButtonPoint);
            // 
            // _buttonEqual
            // 
            this._buttonEqual.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonEqual.Location = new System.Drawing.Point(178, 433);
            this._buttonEqual.Name = "_buttonEqual";
            this._buttonEqual.Size = new System.Drawing.Size(60, 60);
            this._buttonEqual.TabIndex = 19;
            this._buttonEqual.Text = "=";
            this._buttonEqual.UseVisualStyleBackColor = true;
            this._buttonEqual.Click += new System.EventHandler(this.ClickButtonEqual);
            // 
            // _buttonDivide
            // 
            this._buttonDivide.Font = new System.Drawing.Font("Ink Free", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonDivide.Location = new System.Drawing.Point(263, 433);
            this._buttonDivide.Name = "_buttonDivide";
            this._buttonDivide.Size = new System.Drawing.Size(60, 60);
            this._buttonDivide.TabIndex = 20;
            this._buttonDivide.Text = "/";
            this._buttonDivide.UseVisualStyleBackColor = true;
            this._buttonDivide.Click += new System.EventHandler(this.ClickButtonOperation);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 515);
            this.Controls.Add(this._buttonDivide);
            this.Controls.Add(this._buttonEqual);
            this.Controls.Add(this._buttonPoint);
            this.Controls.Add(this._button0);
            this.Controls.Add(this._buttonMultiply);
            this.Controls.Add(this._button3);
            this.Controls.Add(this._button2);
            this.Controls.Add(this._button1);
            this.Controls.Add(this._buttonMinus);
            this.Controls.Add(this._button6);
            this.Controls.Add(this._button5);
            this.Controls.Add(this._button4);
            this.Controls.Add(this._buttonPlus);
            this.Controls.Add(this._button9);
            this.Controls.Add(this._button8);
            this.Controls.Add(this._button7);
            this.Controls.Add(this._buttonClear);
            this.Controls.Add(this._buttonClearEntry);
            this.Controls.Add(this._textBoxResult);
            this.Name = "Form1";
            this.Text = "Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _textBoxResult;
        private System.Windows.Forms.Button _buttonClearEntry;
        private System.Windows.Forms.Button _buttonClear;
        private System.Windows.Forms.Button _button7;
        private System.Windows.Forms.Button _button8;
        private System.Windows.Forms.Button _button9;
        private System.Windows.Forms.Button _buttonPlus;
        private System.Windows.Forms.Button _button4;
        private System.Windows.Forms.Button _button5;
        private System.Windows.Forms.Button _button6;
        private System.Windows.Forms.Button _buttonMinus;
        private System.Windows.Forms.Button _button1;
        private System.Windows.Forms.Button _button2;
        private System.Windows.Forms.Button _button3;
        private System.Windows.Forms.Button _buttonMultiply;
        private System.Windows.Forms.Button _button0;
        private System.Windows.Forms.Button _buttonPoint;
        private System.Windows.Forms.Button _buttonEqual;
        private System.Windows.Forms.Button _buttonDivide;
    }
}

