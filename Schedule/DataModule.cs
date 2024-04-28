using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Schedule
{
    public class DataModule
    {
        public LiteDatabase db;

        public LiteCollection<Teacher> teachers;
        public LiteCollection<Group> groups;
        public LiteCollection<Room> rooms;
        public LiteCollection<Discipline> discs;
        public LiteCollection<Elem> elems;

        public static DataModule DM;

        public DataModule()
        {
            db = new LiteDatabase(@"shedule_base.db");

            var teachers = db.GetCollection<Teacher>("teachers").FindAll();
            var groups = db.GetCollection<Group>("groups").FindAll();
            var rooms = db.GetCollection<Room>("rooms").FindAll();
            var discs = db.GetCollection<Discipline>("disciplines").FindAll();
            var elems = db.GetCollection<Elem>("elems").FindAll();

        }
    }
}
