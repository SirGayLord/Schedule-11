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
    public partial class FormElem : Form
    {
        public FormElem()
        {
            InitializeComponent();
        }

        private int objid;
        
        public static bool editObj(Elem obj)
        {
            FormElem fm = new FormElem();
            fm.objid = obj.Id;

            fm.textRemark.Text = obj.remark;

            fm.comboRoom.Items.Clear();
            foreach (var item in DataModule.DM.rooms.FindAll())
            {
                fm.comboRoom.Items.Add(item);
                if (obj.room != null)
                    if (obj.room.Id == item.Id)
                        fm.comboRoom.SelectedItem = item;
            }

            fm.comboGroup.Items.Clear();
            foreach (var item in DataModule.DM.groups.FindAll())
            {
                fm.comboGroup.Items.Add(item);
                if (obj.group != null)
                    if (obj.group.Id == item.Id)
                        fm.comboGroup.SelectedItem = item;
            }

            fm.comboTeacher.Items.Clear();
            foreach (var item in DataModule.DM.teachers.FindAll())
            {
                fm.comboTeacher.Items.Add(item);
                if (obj.teacher != null)
                    if (obj.teacher.Id == item.Id)
                        fm.comboTeacher.SelectedItem = item;
            }

            fm.comboPair.Items.Clear();
            foreach (var item in Pair.getAllPairs())
            {
                fm.comboPair.Items.Add(item);
                if (obj.pair != null)
                    if (obj.pair.isPairEqual(item))
                        fm.comboPair.SelectedItem = item;
            }

            fm.comboDisc.Items.Clear();
            foreach (var item in DataModule.DM.discs.FindAll())
            {
                fm.comboDisc.Items.Add(item);
                if (obj.disc != null)
                    if (obj.disc.Id == item.Id)
                        fm.comboDisc.SelectedItem = item;
            }

            if (fm.ShowDialog() == DialogResult.OK)
            {
                obj.remark = fm.textRemark.Text.Trim();
                obj.room = (Room)fm.comboRoom.SelectedItem;
                obj.pair = (Pair)fm.comboPair.SelectedItem;
                obj.group = (Group)fm.comboGroup.SelectedItem;
                obj.disc = (Discipline)fm.comboDisc.SelectedItem;
                obj.teacher = (Teacher)fm.comboTeacher.SelectedItem;

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
            if (comboRoom.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана аудитория");
                return;
            }

            if (comboPair.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана пара");
                return;
            }

            if (comboGroup.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана группа");
                return;
            }

            if (comboDisc.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана дисциплина");
                return;
            }

            if (comboTeacher.SelectedItem == null)
            {
                MessageBox.Show("Не выбран преподаватель");
                return;
            }

            // Проверка корректности, чтобы не было пересечений занятий
            Pair pair = (Pair)comboPair.SelectedItem;

            Room room = (Room)comboRoom.SelectedItem;
            Group group = (Group)comboGroup.SelectedItem;
            Teacher teacher = (Teacher)comboTeacher.SelectedItem;

            foreach (Elem el in DataModule.DM.elems.FindAll()) {
                if (el.Id != objid)
                    if (el.pair.isPairEqual(pair)) 
                    { 
                        if (el.room.Id == room.Id)
                        {
                            MessageBox.Show("Найдено занятие в данной аудитории в это же время");
                            return;
                        }
                        if (el.group.Id == group.Id)
                        {
                            MessageBox.Show("Найдено занятие у данной группы в это же время");
                            return;
                        }
                        if (el.teacher.Id == teacher.Id)
                        {
                            MessageBox.Show("Найдено занятие у данного преподавателя в это же время");
                            return;
                        }
                    }
            }            

            DialogResult = DialogResult.OK;
            Hide();
        }
    }
}
