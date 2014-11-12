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
        public int DateOfBirth { get; private set; }
        public Guid ID { get; private set; }
        public bool IsBusy { get; set; }
        private double Cash { get; set; }
        public Human(string name, string secondName, string surname, int dateOfBirth)
            : this(name, secondName, surname, dateOfBirth, 100) { }
        public Human(string name, string secondName, string surname, int dateOfBirth, double cash)
        {
            this.Name = name;
            this.SecondName = secondName;
            this.Surname = surname;
            this.DateOfBirth = dateOfBirth;
            this.Cash = cash;
            this.IsBusy = false;
            this.ID = Guid.NewGuid();
        }
        
        public void AddCash(int monney)
        {
            this.Cash += monney;
        }

    }
}
