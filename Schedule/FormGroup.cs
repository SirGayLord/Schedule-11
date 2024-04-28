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
    public partial class FormGroup : Form
    {
        public FormGroup()
        {
            InitializeComponent();
        }

        public static bool editObj(Group obj)
        {
            FormGroup fm = new FormGroup();
            fm.textName.Text = obj.GroupName;
            fm.textCount.Text = obj.Count.ToString("D");
            fm.clbSoft.Items.Clear();
            foreach (var item in Pair.getAllPairs())
                fm.clbSoft.Items.Add(item, obj.isPairSoftDeny(item));
            fm.clbHard.Items.Clear();
            foreach (var item in Pair.getAllPairs())
                fm.clbHard.Items.Add(item, obj.isPairHardDeny(item));

            if (fm.ShowDialog() == DialogResult.OK)
            {
                obj.GroupName = fm.textName.Text.Trim();
                obj.Count = Int32.Parse(fm.textCount.Text.Trim());
                obj.softlimits.Clear();
                foreach (var item in fm.clbSoft.CheckedItems)
                    obj.softlimits.Add((Pair)item);
                obj.hardlimits.Clear();
                foreach (var item in fm.clbHard.CheckedItems)
                    obj.hardlimits.Add((Pair)item);

                return true;
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
