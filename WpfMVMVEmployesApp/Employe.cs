using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfMVMVEmployesApp
{
    internal class Employe
    {
        public string? Name { set; get; }
        public DateTime BirthDay { set; get; }
        public string? Position { set; get; }

        public Employe()
        {
            Name = default;
            BirthDay = default;
            Position = default;
        }
        public Employe(string? name, DateTime birthDay, string? position)
        {
            Name = name;
            BirthDay = birthDay;
            Position = position;
        }
    }

    internal class EmployesList
    {
        public List<Employe>? Employes { set; get; }
        public EmployesList()
        {
            Employes = new()
            {
                new("Bobby", new DateTime(2001, 5, 10), "Manager"),
                new("Timmy", new DateTime(1998, 11, 23), "Saler"),
                new("Jimmy", new DateTime(1988, 2, 3), "Driver"),
                new("Leopold", new DateTime(2000, 12, 30), "Boss"),
                new("Phill", new DateTime(1995, 4, 18), "Manager")
            };
        }

    }
}
