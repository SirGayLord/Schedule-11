using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    public class Teacher : Axis
    {
        public int Id { get; set; }
        public String FIO { get; set; }
        public String Contact { get; set; }

        public List<Pair> hardlimits { get; set; }
        public List<Pair> softlimits { get; set; }

        public Teacher()
        {
            hardlimits = new List<Pair>();
            softlimits = new List<Pair>();
        }

        public string Name()
        {
            return FIO;
        }

        public string getLimits()
        {
            StringBuilder sb = new StringBuilder();            
            foreach(var pair in hardlimits) 
                sb.Append(pair.getText()+"  ") ;
            return sb.ToString();
        }

        public bool isPairSoftDeny(Pair pair)
        {
            return softlimits.Exists((p) => p.isPairEqual(pair));
        }

        public bool isPairHardDeny(Pair pair)
        {
            return hardlimits.Exists((p) => p.isPairEqual(pair));
        }

        public override string ToString()
        {
            return Name();
        }

        public bool isMatchElem(Elem elem)
        {
            return elem.teacher.Id == Id;
        }
    }
}
