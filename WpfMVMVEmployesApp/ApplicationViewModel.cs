using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVMVEmployesApp
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Employe> Employes { get; set; }

        private Employe selectEmploye;

        public Employe SelectEmploye
        {
            get => selectEmploye;
            set
            {
                selectEmploye = value;
                OnPropertyChanged(nameof(SelectEmploye));
            }
        }


        private AppCommand addEmployeCommand;
        
        public AppCommand AddEmployeCommand
        {
            get
            {
                if (addEmployeCommand is null)
                    addEmployeCommand = new AppCommand(obj =>
                    {
                        Employe employeNew = new Employe();
                        int position = Employes.IndexOf(SelectEmploye);
                        Employes.Insert(position + 1, employeNew);
                        SelectEmploye = employeNew;
                    });
                    
                return addEmployeCommand;
                
            }
        }

        private AppCommand removeEmployeCommand;
        public AppCommand RemoveEmployeCommand
        {
            get
            {
                if(removeEmployeCommand is null)
                {
                    removeEmployeCommand = new AppCommand(
                        obj =>
                        {
                            if (obj is Employe employe)
                                Employes.Remove(employe);
                        },
                        obj => obj != null
                        );
                }
                return removeEmployeCommand;
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
