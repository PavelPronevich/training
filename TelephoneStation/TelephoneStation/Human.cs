using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public class Human
    {
        public string Name { get; private set; }
        public string SecondName { get; private set; }
        public string Surname { get; private set; }
        public int Age { get; private set; }
        public Guid ID { get; private set; }
        private double Cash { get; set; }
        public Human(string name, string secondName, string surname, int age):this(name,secondName,surname,age,100)
        {
        }
        public Human(string name, string secondName, string surname, int age, double cash)
        {
            this.Name = name;
            this.SecondName = secondName;
            this.Surname = surname;
            this.Age = age;
            this.Cash = cash;
            this.ID = Guid.NewGuid();
        }
        
        public void AddCash(int monney)
        {
            this.Cash += monney;
        }

    }
}
