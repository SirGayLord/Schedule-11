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
    public partial class FormSelAxis : Form
    {
        public FormSelAxis()
        {
            InitializeComponent();
        }

        public static bool fillFilt(List<Axis> source, List<Axis> filt)
        {
            FormSelAxis fm = new FormSelAxis();
            foreach (var item in source)
                fm.clbAxis.Items.Add(item, filt.Exists((it) => it.Name().Equals(item.Name())));
                    
            if (fm.ShowDialog()==DialogResult.OK)
            {
                filt.Clear();
                foreach (var item in fm.clbAxis.CheckedItems)
                    filt.Add((Axis)item);
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Hide();
        }
    }
}
