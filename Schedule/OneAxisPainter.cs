using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Schedule
{
    public class OneAxisPainter
    {
        Pen pen;
        Font fnt;
        Brush brushhard;
        Brush brushsoft;
        Brush brushok;
        Brush brushf;

        public OneAxisPainter()
        {
             pen = new Pen(new SolidBrush(Color.Black));
             fnt = new Font("Arial", 14);
             brushhard = new SolidBrush(Color.FromArgb(255, 128, 128));
             brushsoft = new SolidBrush(Color.FromArgb(255, 255, 128));
             brushok = new SolidBrush(Color.FromArgb(255, 255, 255));
             brushf = new SolidBrush(Color.Black);

        }

        public delegate bool FuncElemMatch(Elem el) ;

        private List<Elem> elems = null;
        private Type axistype;

        public void setFilt(IEnumerable<Elem> list, FuncElemMatch filt, Type axistype)
        {
            elems = list.Where((el)=>filt(el)).ToList();
            this.axistype = axistype;
        }

        public void setFiltTwo(IEnumerable<Elem> list, List<Axis> filt1, List<Axis> filt2)
        {
            elems = new List<Elem>() ;            
            foreach (var elem in list)
            {
                if ((filt1.Exists((filt) => filt.isMatchElem(elem))) &&
                   (filt2.Exists((filt) => filt.isMatchElem(elem))))
                    elems.Add(elem);
            }
            this.axistype = null;
        }

        public int TotalHeight(Graphics g)
        {
            if (elems == null) return 1000;

            Size sz = CalcSize(g);
            return (sz.Height + SPACE) * elems.Count + SPACE;
        }

        public Size CalcSize(Graphics g)
        {
            if (elems == null) new Size(100,100);

            int maxw = 0;
            int h = 0;

            bool once = true;
            foreach (var el in elems)
            {                
                string[] lines = el.buildLinesOneAxis(axistype);

                foreach (var line in lines)
                {
                    SizeF sz = g.MeasureString(line, fnt);
                    if (once) h += (int)sz.Height;
                    if (maxw < (int)sz.Width) maxw = (int)sz.Width;
                }
                once = false;                
            }

            Size res = new Size();
            res.Width = maxw ;
            res.Height = h;

            return res;
        }

        private static int SPACE = 10 ;                

        public void PaintTo(Graphics g)
        {
            if (elems == null) return;

            Size sz = CalcSize(g);
            //StreamWriter stm = new StreamWriter("main.log");
            //stm.WriteLine(elems.Count.ToString("D"));

            int top = 0;
            foreach (var el in elems)
            {
                Brush brush = brushok;
                if (el.isSoftDeny()) brush = brushsoft;
                if (el.isHardDeny()) brush = brushhard;
                
                string[] lines = el.buildLinesOneAxis(axistype) ;

                string str = "";
                foreach (var line in lines)
                    str += line + Environment.NewLine;
                
                Rectangle rect = new Rectangle(10, top, sz.Width, sz.Height);
                g.FillRectangle(brush, rect);
                g.DrawRectangle(pen, rect);

                g.DrawString(str, fnt, brushf, 10, top);

                top += (sz.Height + SPACE);

                /*
                int h = 0 ;
                int maxw = 0 ;
                string str = "" ;
                foreach(var line in lines) {
                    SizeF sz = g.MeasureString(line,fnt) ;
                    h+=(int)sz.Height ;
                    if (maxw<(int)sz.Width) maxw = (int)sz.Width ;
                    str+=line+Environment.NewLine ;
                }

                Rectangle rect = new Rectangle(10, top, maxw, h) ;
                g.FillRectangle(brush, rect);
                g.DrawRectangle(pen, rect);
                
                g.DrawString(str, fnt, brushf, 10, top);
                
                top+=(h+SPACE) ;
                 */
            }

           // stm.Close();
        }
    }
}

