using System;
using System.Collections.Generic;
using System.Text;

namespace offlineFirebase
{
    public class Persons
    {
        public Persons()
        {
        }

        public Persons(int personId, string name , int age)
        {
            PersonId = personId;
            Name = name;
            Age = age;
        }

        public int Age { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }
    }
}
