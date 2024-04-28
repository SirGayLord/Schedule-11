using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    public interface Axis
    {
        String Name();
        String getLimits();
        bool isPairSoftDeny(Pair p) ;
        bool isPairHardDeny(Pair p) ;
        bool isMatchElem(Elem elem);
    }
}
