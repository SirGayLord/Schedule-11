using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    public class Group: Axis
    {
        public int Id { get; set; }
        public String GroupName { get; set; }
        public int Count { get; set; }

        public List<Pair> hardlimits { get; set; }
        public List<Pair> softlimits { get; set; }

        public Group()
        {
            hardlimits = new List<Pair>();
            softlimits = new List<Pair>();
        }

        public string Name()
        {
            return GroupName;
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
            return elem.group.Id == Id;
        }
    }
}
