using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule
{
    
    
    public partial class FormInput : Form
    {
        public FormInput()
        {
            InitializeComponent();
        }

        static public bool InputQuery(string caption, ref String str) {
            FormInput fm = new FormInput();
            fm.Text = caption;
            fm.textBox1.Text = str;
            if (fm.ShowDialog() == DialogResult.OK)
            {
                str = fm.textBox1.Text;
                return true;
            }
            else
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Hide();
        }

        private void FormInput_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
