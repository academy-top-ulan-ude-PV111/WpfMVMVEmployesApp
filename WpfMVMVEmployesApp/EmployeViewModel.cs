using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVMVEmployesApp
{
    public class EmployeViewModel : INotifyPropertyChanged
    {
        private Employe? employe;

        public EmployeViewModel(Employe? employe)
        {
            this.employe = employe;
        }

        public string Name
        {
            get => employe.Name;
            set
            {
                employe.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public DateTime BirthDay
        {
            get => employe.BirthDay;
            set
            {
                employe.BirthDay = value;
                OnPropertyChanged(nameof(BirthDay));
            }
        }

        public string Position
        {
            get => employe.Position;
            set
            {
                employe.Position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
        }
    }
}
