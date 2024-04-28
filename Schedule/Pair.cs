using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Schedule
{
    public class Pair: Axis
    {
        public int WeekDay { get; set; }
        public int PairN { get; set; }
                
        private static Dictionary<int,String> pairnames ;

        private static List<Pair> allPairs ;

        public static List<Pair> getAllPairs() 
        {
            if (allPairs == null) 
            {
                allPairs = new List<Pair>() ;
                for (int i=0; i<=6; i++) 
                {
                    foreach(var p in pairnames.Keys)
                        allPairs.Add(new Pair() { WeekDay = i, PairN = p }) ;
                }

            }
            return allPairs ;
        }

        static Pair() 
        {
            pairnames = new Dictionary<int, string>();

            XDocument doc = XDocument.Load("pairs.xml");
            foreach (var elem in doc.Element("pairs").Elements("pair"))
            {
                pairnames.Add(Int32.Parse(elem.Element("n").Value), elem.Element("text").Value);
            }            
        }      

        public String getWeekDayAsStr()
        {
            String str="";
            if (WeekDay == 0) str = "ПН";
            if (WeekDay == 1) str = "ВТ";
            if (WeekDay == 2) str = "СР";
            if (WeekDay == 3) str = "ЧТ";
            if (WeekDay == 4) str = "ПТ";
            if (WeekDay == 5) str = "СБ";
            if (WeekDay == 6) str = "ВС";                       
            return str; 
        }

        public String getText()
        {
            return getWeekDayAsStr() + " " + getPairHours();
        }

        public String getPairHours()
        {
            if (pairnames.ContainsKey(PairN)) return pairnames[PairN]; else return "" ;
        }

        public bool isPairEqual(Pair p)
        {
            return (WeekDay == p.WeekDay) && (PairN == p.PairN);
        }

        public override string ToString()
        {
            return getText();
        }

        public string Name()
        {
            return getText();
        }

        public string getLimits()
        {
            return "";
        }

        public bool isPairSoftDeny(Pair pair)
        {
            return false;
        }

        public bool isPairHardDeny(Pair pair)
        {
            return false;
        }

        public bool isMatchElem(Elem elem)
        {
            return elem.pair.isPairEqual(this);
        }
    }
}
