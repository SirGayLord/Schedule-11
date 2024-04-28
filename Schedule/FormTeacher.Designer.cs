namespace Schedule
{
    partial class FormTeacher
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textFIO = new System.Windows.Forms.TextBox();
            this.textContact = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.clbSoft = new System.Windows.Forms.CheckedListBox();
            this.clbHard = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(437, 471);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(350, 471);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ФИО";
            // 
            // textFIO
            // 
            this.textFIO.Location = new System.Drawing.Point(16, 30);
            this.textFIO.Name = "textFIO";
            this.textFIO.Size = new System.Drawing.Size(496, 20);
            this.textFIO.TabIndex = 3;
            // 
            // textContact
            // 
            this.textContact.Location = new System.Drawing.Point(16, 80);
            this.textContact.Name = "textContact";
            this.textContact.Size = new System.Drawing.Size(496, 20);
            this.textContact.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Контакт";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Мягкие запреты";
            // 
            // clbSoft
            // 
            this.clbSoft.CheckOnClick = true;
            this.clbSoft.FormattingEnabled = true;
            this.clbSoft.Location = new System.Drawing.Point(16, 142);
            this.clbSoft.Name = "clbSoft";
            this.clbSoft.Size = new System.Drawing.Size(227, 319);
            this.clbSoft.TabIndex = 7;
            // 
            // clbHard
            // 
            this.clbHard.CheckOnClick = true;
            this.clbHard.FormattingEnabled = true;
            this.clbHard.Location = new System.Drawing.Point(283, 142);
            this.clbHard.Name = "clbHard";
            this.clbHard.Size = new System.Drawing.Size(229, 319);
            this.clbHard.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(280, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Жесткие запреты";
            // 
            // FormTeacher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 506);
            this.Controls.Add(this.clbHard);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.clbSoft);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textContact);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textFIO);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FormTeacher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Преподаватель";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textFIO;
        private System.Windows.Forms.TextBox textContact;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox clbSoft;
        private System.Windows.Forms.CheckedListBox clbHard;
        private System.Windows.Forms.Label label4;
    }
}