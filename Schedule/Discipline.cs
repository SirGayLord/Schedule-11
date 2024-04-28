using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    public class Discipline
    {
        public int Id { get; set; }
        public String DiscName { get; set ;}

        public override string ToString()
        {
            return DiscName;
        }
    }
}
