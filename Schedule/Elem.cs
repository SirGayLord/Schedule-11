using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    public class Elem
    {
        public int Id { get; set; }
        public Pair pair { get; set; }
        public Teacher teacher { get; set; }
        public Room room { get; set; }
        public Group group { get; set; }
        public Discipline disc { get; set; }
        public String remark { get; set; }

        public bool isHardDeny()
        {
            if (group.isPairHardDeny(pair)) return true;
            if (room.isPairHardDeny(pair)) return true;
            if (teacher.isPairHardDeny(pair)) return true;
            return false;
        }

        public bool isSoftDeny()
        {
            if (group.isPairSoftDeny(pair)) return true;
            if (room.isPairSoftDeny(pair)) return true;
            if (teacher.isPairSoftDeny(pair)) return true;
            return false;
        }

        public string[] buildLinesOneAxis(Type axistype)
        {
            var res = new List<string>();
            if (axistype != typeof(Pair)) res.Add(pair.getText()) ;
            if (axistype != typeof(Teacher)) res.Add(teacher.Name());
            if (axistype != typeof(Room)) res.Add(room.Name());
            if (axistype != typeof(Group)) res.Add(group.Name());
            if (axistype != typeof(Discipline)) res.Add(disc.DiscName);
            return res.ToArray();
        }
    }
}
