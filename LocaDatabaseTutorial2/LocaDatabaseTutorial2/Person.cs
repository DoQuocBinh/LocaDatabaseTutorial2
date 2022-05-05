using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocaDatabaseTutorial2
{
    public class Person
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }

        public String Name { get; set; }
        public int Age { get; set; }

    }
}
