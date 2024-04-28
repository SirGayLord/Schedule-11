using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Schedule
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private DataModule DM;
        private OneAxisPainter painterone;
        private OneAxisPainter paintertwo;
        private List<Axis> filt1;
        private List<Axis> filt2;

        private void Pairs2Grid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("День", typeof(String)));
            dt.Columns.Add(new DataColumn("Пара", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Часы", typeof(String)));
            dt.Columns.Add(new DataColumn("O", typeof(Pair)));

            foreach (var pair in Pair.getAllPairs())
            {
                DataRow row = dt.NewRow();
                row["День"] = pair.getWeekDayAsStr();
                row["Пара"] = pair.PairN;
                row["Часы"] = pair.getPairHours();
                row["O"] = pair;
                dt.Rows.Add(row);
            }

            dgvPairs.DataSource = dt;
            dgvPairs.Columns[3].Visible = false;
        }

        private void Discs2Grid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Дисциплина", typeof(String)));
            dt.Columns.Add(new DataColumn("O", typeof(Discipline)));
            
            foreach (var item in DM.discs.FindAll())
            {
                DataRow row = dt.NewRow();
                row["Дисциплина"] = item.DiscName;
                row["O"] = item;
                dt.Rows.Add(row);
            }

            dgvDiscs.DataSource = dt;
            dgvDiscs.Columns[1].Visible = false;
        }

        private void Elems2Grid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Пара", typeof(String)));
            dt.Columns.Add(new DataColumn("Группа", typeof(String)));
            dt.Columns.Add(new DataColumn("Аудитория", typeof(String)));
            dt.Columns.Add(new DataColumn("Дисциплина", typeof(String)));
            dt.Columns.Add(new DataColumn("Преподаватель", typeof(String)));
            dt.Columns.Add(new DataColumn("O", typeof(Elem)));

            foreach (var item in DM.elems.FindAll())
            {
                bool z = true ;
                if (comboFiltGroups.SelectedItem != null)
                    if (((Group)comboFiltGroups.SelectedItem).Id != item.group.Id) z = false;
                if (comboFiltRooms.SelectedItem != null)
                    if (((Room)comboFiltRooms.SelectedItem).Id != item.room.Id) z = false;
                if (comboFiltTeachers.SelectedItem != null)
                    if (((Teacher)comboFiltTeachers.SelectedItem).Id != item.teacher.Id) z = false;

                if (z)
                {
                    DataRow row = dt.NewRow();
                    row["Пара"] = item.pair.getText();
                    row["Группа"] = item.group.GroupName;
                    row["Аудитория"] = item.room.RoomN;
                    row["Преподаватель"] = item.teacher.FIO;
                    row["Дисциплина"] = item.disc.DiscName;
                    row["O"] = item;
                    dt.Rows.Add(row);
                }
            }

            dgvTable.DataSource = dt;
            dgvTable.Columns[5].Visible = false;
        }

        private void Axis2Grid(IEnumerable<Axis> axis, DataGridView dgv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Описание", typeof(string)));
            dt.Columns.Add(new DataColumn("Ограничения", typeof(string)));
            dt.Columns.Add(new DataColumn("O", typeof(Axis)));

            foreach (var item in axis)
            {
                DataRow row = dt.NewRow();
                row["Описание"] = item.Name();
                row["Ограничения"] = item.getLimits();
                row["O"] = item;
                dt.Rows.Add(row);
            }
                        
            dgv.DataSource = dt;
            dgv.Columns[2].Visible = false;

            UpdateFilters();
        }

        void UpdateFilters()
        {
            comboFiltGroups.Items.Clear();
            comboFiltGroups.Items.AddRange(DM.groups.FindAll().ToArray());

            comboFiltRooms.Items.Clear();
            comboFiltRooms.Items.AddRange(DM.rooms.FindAll().ToArray());

            comboFiltTeachers.Items.Clear();
            comboFiltTeachers.Items.AddRange(DM.teachers.FindAll().ToArray());
        }

        public T getObj<T>(DataGridView dgv, T def)
        {
            if (dgv.CurrentRow == null) return def;
            return (T)(dgv.CurrentRow.DataBoundItem as DataRowView).Row["O"];
        }

        private void butAddTeacher_Click(object sender, EventArgs e)
        {
            Teacher obj = new Teacher();
            if (FormTeacher.editObj(obj))
            {
                DM.teachers.Insert(obj);
                Axis2Grid(DM.teachers.FindAll(),dgvTeachers);
            }
        }

        private void tabPage7_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            /*
            Graphics g = e.Graphics;

            Pen pen = new Pen(new SolidBrush(Color.Black));
            Font fnt = new Font("Arial", 14);
            Brush brush = new SolidBrush(Color.FromArgb(255, 128, 128));
            Brush brush2 = new SolidBrush(Color.FromArgb(255, 255, 128));
            Brush brushf = new SolidBrush(Color.Black);

            g.FillRectangle(brush, 10, 10, 200, 120);
            g.DrawRectangle(pen, 10, 10, 200, 120);
            g.DrawString("Кузьмина А.М." + Environment.NewLine + "Физика" + Environment.NewLine + "ВТ 8:00" +
             Environment.NewLine + "Б-2" + Environment.NewLine + "101", fnt, brushf, 10, 10);
            //g.DrawString("Кузьмина А.М.", fnt, brush, 10, 10);

            g.FillRectangle(brush2, 50, 160, 200, 120);
            g.DrawRectangle(pen, 50, 160, 200, 120);
            g.DrawString("Михайлов Н.В." + Environment.NewLine + "Математика" + Environment.NewLine + "9:40" +
             Environment.NewLine + "Б-2" + Environment.NewLine + "102", fnt, brushf, 50, 160);
            
            */
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            
            try
            {
            
                DM = new DataModule();
                DataModule.DM = DM;
                Pairs2Grid();
                Axis2Grid(DM.teachers.FindAll(), dgvTeachers);
                Axis2Grid(DM.groups.FindAll(), dgvGroups);
                Axis2Grid(DM.rooms.FindAll(), dgvRooms);
                Discs2Grid();
                Elems2Grid();

                filt1 = new List<Axis>();
                filt2 = new List<Axis>();

                painterone = new OneAxisPainter();
                paintertwo = new OneAxisPainter();

                panelOneAxis.Left = 0;
                panelOneAxis.Top = 0;
                panelOneAxis.Width = panelOneAxisParent.Width-100;
                panelOneAxis.Height = 2000;

                panelTwoAxis.Left = 0;
                panelTwoAxis.Top = 0;
                panelTwoAxis.Width = panelTwoAxisParent.Width - 100;
                panelTwoAxis.Height = 2000;
            }
            finally
            {
                
            }
        }

        private void butEditTeacher_Click(object sender, EventArgs e)
        {
            Teacher obj = getObj<Teacher>(dgvTeachers,null);
            if (obj == null)
            {
                MessageBox.Show("Нет преподавателя в списке");
                return;

            }
            if (FormTeacher.editObj(obj))
            {
                DM.teachers.Update(obj);
                Axis2Grid(DM.teachers.FindAll(), dgvTeachers);
            }
        }

        private void butDelTeacher_Click(object sender, EventArgs e)
        {
            Teacher obj = getObj<Teacher>(dgvTeachers, null);
            if (obj == null)
            {
                MessageBox.Show("Нет преподавателя в списке");
                return;

            }

            string str = "";
            if (!FormInput.InputQuery("Введите 1 для удаления записи", ref str)) return;

            if (str.Equals("1"))
            {
                DM.teachers.Delete(obj.Id);
                Axis2Grid(DM.teachers.FindAll(), dgvTeachers);
            }

        }

        private void butAddGroup_Click(object sender, EventArgs e)
        {
            Group obj = new Group();
            if (FormGroup.editObj(obj))
            {
                DM.groups.Insert(obj);
                Axis2Grid(DM.groups.FindAll(), dgvGroups);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            
        }

        private void butEditGroup_Click(object sender, EventArgs e)
        {
            Group obj = getObj<Group>(dgvGroups, null);
            if (obj == null)
            {
                MessageBox.Show("Нет группы в списке");
                return;

            }
            if (FormGroup.editObj(obj))
            {
                DM.groups.Update(obj);
                Axis2Grid(DM.groups.FindAll(), dgvGroups);
            }
        }

        private void butDelGroup_Click(object sender, EventArgs e)
        {
            Group obj = getObj<Group>(dgvGroups, null);
            if (obj == null)
            {
                MessageBox.Show("Нет группы в списке");
                return;

            }

            string str = "";
            if (!FormInput.InputQuery("Введите 1 для удаления записи", ref str)) return;

            if (str.Equals("1"))
            {
                DM.groups.Delete(obj.Id);
                Axis2Grid(DM.groups.FindAll(), dgvGroups);
            }
        }

        private void butAddRoom_Click(object sender, EventArgs e)
        {
            Room obj = new Room();
            if (FormRoom.editObj(obj))
            {
                DM.rooms.Insert(obj);
                Axis2Grid(DM.rooms.FindAll(), dgvRooms);
            }
        }

        private void butEditRoom_Click(object sender, EventArgs e)
        {
            Room obj = getObj<Room>(dgvRooms, null);
            if (obj == null)
            {
                MessageBox.Show("Нет аудитории в списке");
                return;

            }
            if (FormRoom.editObj(obj))
            {
                DM.rooms.Update(obj);
                Axis2Grid(DM.rooms.FindAll(), dgvRooms);
            }
        }

        private void butDelRoom_Click(object sender, EventArgs e)
        {
            Room obj = getObj<Room>(dgvRooms, null);
            if (obj == null)
            {
                MessageBox.Show("Нет аудитории в списке");
                return;
            }

            string str = "";
            if (!FormInput.InputQuery("Введите 1 для удаления записи", ref str)) return;

            if (str.Equals("1"))
            {
                DM.rooms.Delete(obj.Id);
                Axis2Grid(DM.rooms.FindAll(), dgvRooms);
            }
        }

        private void butAddDisc_Click(object sender, EventArgs e)
        {

            string str = "";
            if (!FormInput.InputQuery("Введите название дисциплины", ref str)) return;

            Discipline obj = new Discipline() { DiscName = str };
            DM.discs.Insert(obj);
            Discs2Grid();
        }

        private void butEditDisc_Click(object sender, EventArgs e)
        {
            Discipline obj = getObj<Discipline>(dgvDiscs, null);
            if (obj == null)
            {
                MessageBox.Show("Нет дисциплины в списке");
                return;
            }

            string str = obj.DiscName;
            if (!FormInput.InputQuery("Введите новое название дисциплины", ref str)) return;

            obj.DiscName = str;
            DM.discs.Update(obj);
            Discs2Grid();
        }

        private void butDelDisc_Click(object sender, EventArgs e)
        {
            Discipline obj = getObj<Discipline>(dgvDiscs, null);
            if (obj == null)
            {
                MessageBox.Show("Нет дисциплины в списке");
                return;
            }

            string str = "";
            if (!FormInput.InputQuery("Введите 1 для удаления записи", ref str)) return;

            if (str.Equals("1"))
            {
                DM.discs.Delete(obj.Id);
                Discs2Grid();
            }
        }

        private void butAddElem_Click(object sender, EventArgs e)
        {
            Elem obj = new Elem();
                        
            if (FormElem.editObj(obj))
            {
                DM.elems.Insert(obj);
                Elems2Grid();
            }
        }

        private void butFiltClear_Click(object sender, EventArgs e)
        {
            comboFiltGroups.SelectedIndex = -1;
            comboFiltRooms.SelectedIndex = -1;
            comboFiltTeachers.SelectedIndex = -1;
            Elems2Grid();
        }

        private void comboFiltTeachers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Elems2Grid();
        }

        private void comboFiltGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            Elems2Grid();
        }

        private void comboFiltRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            Elems2Grid();
        }
                
        private void panel14_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panelOneAxis_Paint(object sender, PaintEventArgs e)
        {
            if (painterone == null) return;

            panelOneAxis.Height = 2*painterone.TotalHeight(e.Graphics);
            painterone.PaintTo(e.Graphics);
        }

        private const int IDX_PAIR = 0 ;
        private const int IDX_TEACHER = 1 ;
        private const int IDX_GROUP = 2;
        private const int IDX_ROOM = 3;

        private void comboTypesOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboFiltOne.Items.Clear();
            if (comboTypesOne.SelectedIndex == IDX_PAIR)            
                comboFiltOne.Items.AddRange(Pair.getAllPairs().ToArray());
            if (comboTypesOne.SelectedIndex == IDX_TEACHER)
                comboFiltOne.Items.AddRange(DM.teachers.FindAll().ToArray());
            if (comboTypesOne.SelectedIndex == IDX_GROUP)
                comboFiltOne.Items.AddRange(DM.groups.FindAll().ToArray());
            if (comboTypesOne.SelectedIndex == IDX_ROOM)
                comboFiltOne.Items.AddRange(DM.rooms.FindAll().ToArray());
            
        }

        private void goPairOne(Pair pair)
        {
            painterone.setFilt(DM.elems.FindAll(),(el) => el.pair.isPairEqual(pair),typeof(Pair));
            panelOneAxis.Refresh();
        }

        private void goTeacherOne(Teacher teacher)
        {
            painterone.setFilt(DM.elems.FindAll(), (el) => el.teacher.Id == teacher.Id, typeof(Teacher));
            panelOneAxis.Refresh();
        }

        private void goGroupOne(Group group)
        {
            painterone.setFilt(DM.elems.FindAll(), (el) => el.group.Id == group.Id, typeof(Group));
            panelOneAxis.Refresh();
        }

        private void goRoomOne(Room room)
        {
            painterone.setFilt(DM.elems.FindAll(), (el) => el.room.Id == room.Id, typeof(Room));
            panelOneAxis.Refresh();
        }

        private void comboFiltOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboTypesOne.SelectedIndex == IDX_PAIR)
                goPairOne((Pair)comboFiltOne.SelectedItem);
            if (comboTypesOne.SelectedIndex == IDX_TEACHER)
                goTeacherOne((Teacher)comboFiltOne.SelectedItem);
            if (comboTypesOne.SelectedIndex == IDX_ROOM)
                goRoomOne((Room)comboFiltOne.SelectedItem);
            if (comboTypesOne.SelectedIndex == IDX_GROUP)
                goGroupOne((Group)comboFiltOne.SelectedItem);
        }

        private void butGoTeacherOne_Click(object sender, EventArgs e)
        {
            Teacher obj = getObj<Teacher>(dgvTeachers, null);
            if (obj == null)
            {
                MessageBox.Show("Нет преподавателя в списке");
                return;
            }

            goTeacherOne(obj);
            tabControl1.SelectedIndex = 6;
        }

        private void butGoGroupOne_Click(object sender, EventArgs e)
        {
            Group obj = getObj<Group>(dgvGroups, null);
            if (obj == null)
            {
                MessageBox.Show("Нет группы в списке");
                return;
            }

            goGroupOne(obj);
            tabControl1.SelectedIndex = 6;
        }

        private void butGoRoomOne_Click(object sender, EventArgs e)
        {
            Room obj = getObj<Room>(dgvRooms, null);
            if (obj == null)
            {
                MessageBox.Show("Нет аудитории в списке");
                return;
            }

            goRoomOne(obj);
            tabControl1.SelectedIndex = 6;
        }

        private void butGoPairOne_Click(object sender, EventArgs e)
        {
            Pair obj = getObj<Pair>(dgvPairs, null);
            
            goPairOne(obj);
            tabControl1.SelectedIndex = 6;
        }

        private void butEditElem_Click(object sender, EventArgs e)
        {
            Elem obj = getObj<Elem>(dgvTable, null);
            if (obj == null)
            {
                MessageBox.Show("Нет элемента в списке");
                return;

            }
            
            if (FormElem.editObj(obj))
            {
                DM.elems.Update(obj);
                Elems2Grid();
            }
        }

        private void butOnePng_Click(object sender, EventArgs e)
        {
            if (painterone == null) {
                MessageBox.Show("Не выбрана проекция") ;
                return ;
            }

            if (saveFileDialog1.ShowDialog()!=DialogResult.OK) return ;

            Bitmap bmpt = new Bitmap(100, 100);
            Graphics gt = Graphics.FromImage(bmpt);

            Bitmap bmp = new Bitmap(panelOneAxis.Width, painterone.TotalHeight(gt));
            Graphics g = Graphics.FromImage(bmp);
            painterone.PaintTo(g);

            bmp.Save(saveFileDialog1.FileName + ".png");
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        
        }

        private void filt1toEdit()
        {
            string str = "";
            foreach (var item in filt1)
                str += item.Name() + " ";
            textAxis1.Text = str;
        }

        private void filt2toEdit()
        {
            string str = "";
            foreach (var item in filt2)
                str += item.Name() + " ";
            textAxis2.Text = str;
        }               

        private void comboAxisType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filt1.Clear();
            filt1toEdit();
        }

        private void comboBox6comboAxisType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            filt2.Clear();
            filt2toEdit();
        }

        private void comboAxis1_SelectedIndexChanged(object sender, EventArgs e)
        {
                    
        }

        private void butSelAxis1_Click(object sender, EventArgs e)
        {
            if (comboAxisType1.SelectedIndex == -1)
            {
                MessageBox.Show("Не выбран тип");
                return;
            }

            if (comboAxisType1.SelectedIndex == IDX_PAIR)
                FormSelAxis.fillFilt(Pair.getAllPairs().ToList<Axis>(), filt1);            
            if (comboAxisType1.SelectedIndex == IDX_TEACHER)
                FormSelAxis.fillFilt(DM.teachers.FindAll().ToList<Axis>(), filt1);
            if (comboAxisType1.SelectedIndex == IDX_GROUP)
                FormSelAxis.fillFilt(DM.groups.FindAll().ToList<Axis>(), filt1);
            if (comboAxisType1.SelectedIndex == IDX_ROOM)
                FormSelAxis.fillFilt(DM.rooms.FindAll().ToList<Axis>(), filt1);
            
            filt1toEdit();

            paintertwo.setFiltTwo(DM.elems.FindAll(), filt1, filt2);
            panelTwoAxis.Refresh();
        }

        private void butSelAxis2_Click(object sender, EventArgs e)
        {
            if (comboAxisType2.SelectedIndex == -1)
            {
                MessageBox.Show("Не выбран тип");
                return;
            }

            if (comboAxisType2.SelectedIndex == IDX_PAIR)
                FormSelAxis.fillFilt(Pair.getAllPairs().ToList<Axis>(), filt2);
            if (comboAxisType2.SelectedIndex == IDX_TEACHER)
                FormSelAxis.fillFilt(DM.teachers.FindAll().ToList<Axis>(), filt2);
            if (comboAxisType2.SelectedIndex == IDX_GROUP)
                FormSelAxis.fillFilt(DM.groups.FindAll().ToList<Axis>(), filt2);
            if (comboAxisType2.SelectedIndex == IDX_ROOM)
                FormSelAxis.fillFilt(DM.rooms.FindAll().ToList<Axis>(), filt2);

            filt2toEdit();

            paintertwo.setFiltTwo(DM.elems.FindAll(), filt1, filt2);
            panelTwoAxis.Refresh();
        }

        private void panelTwoAxis_Paint(object sender, PaintEventArgs e)
        {
            if (paintertwo == null) return;

            panelTwoAxis.Height = 2 * paintertwo.TotalHeight(e.Graphics);
            paintertwo.PaintTo(e.Graphics);
        }

        private void butExportTwo_Click(object sender, EventArgs e)
        {
            if (paintertwo == null)
            {
                MessageBox.Show("Не выбрана проекция");
                return;
            }

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            Bitmap bmpt = new Bitmap(100, 100);
            Graphics gt = Graphics.FromImage(bmpt);

            Bitmap bmp = new Bitmap(panelTwoAxis.Width, paintertwo.TotalHeight(gt));
            Graphics g = Graphics.FromImage(bmp);
            paintertwo.PaintTo(g);

            bmp.Save(saveFileDialog1.FileName + ".png");
        }

        private void butBackup_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog()!=DialogResult.OK) return ;

            XDocument xml = new XDocument();
            var root = new XElement("schedule");
            var elems = new XElement("elems") ;
            foreach (var elem in DM.elems.FindAll())
            {
                var xel = new XElement("elem");
                xel.Add(new XElement("pair",elem.pair.Name())) ;
                xel.Add(new XElement("room", elem.room.Name()));
                xel.Add(new XElement("teacher", elem.teacher.Name()));
                xel.Add(new XElement("group", elem.group.Name()));
                xel.Add(new XElement("discipline", elem.disc.DiscName));
                elems.Add(xel);
            }
            root.Add(elems);
            xml.Add(root);
            xml.Save(saveFileDialog1.FileName + ".xml");
        }
                
    }
}
