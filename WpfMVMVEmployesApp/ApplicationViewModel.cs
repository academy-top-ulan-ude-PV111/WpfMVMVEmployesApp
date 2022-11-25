using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVMVEmployesApp
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        // file dialogs
        private IFileService fileService;
        private IDialogService dialogService;

        // collection of employes
        public ObservableCollection<Employe> Employes { get; set; }

        // current select employe
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

        // commands
        // command add employe
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

        // command delete employ
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
                            {
                                int position = Employes.IndexOf(SelectEmploye);
                                Employes.Remove(employe);
                                if (position < Employes.Count)
                                    SelectEmploye = Employes[position];
                                else
                                    SelectEmploye = Employes[position - 1];
                            }
                                
                        },
                        obj => Employes.Count > 0
                        );
                }
                return removeEmployeCommand;
            }
        }

        // command copy employe
        private AppCommand copyEmployeCommand;
        public AppCommand CopyEmployeCommand
        {
            get
            {
                if(copyEmployeCommand is null)
                {
                    copyEmployeCommand = new AppCommand(
                        obj =>
                        {
                            if (obj is Employe employe)
                            {
                                if (employe is not null)
                                {
                                    Employe employeCopy = new Employe(employe.Name, employe.BirthDay, employe.Position);
                                    int position = Employes.IndexOf(employe);
                                    Employes.Insert(position + 1, employeCopy);
                                    SelectEmploye = employeCopy;
                                }
                            }
                        }
                        );
                }

                return copyEmployeCommand;
            }
        }

        // command save employes to file
        private AppCommand saveCommand;
        public AppCommand SaveCommand
        {
            get
            {
                if(saveCommand is null)
                {
                    saveCommand = new AppCommand(
                        obj =>
                        {
                            try
                            {
                                if(dialogService.Path != null && dialogService.Path != "")
                                {
                                    fileService.Save(dialogService.Path, Employes.ToList());
                                    dialogService.ShowMsg($"Employes save to file {dialogService.Path}");
                                }
                                else
                                {
                                    if (dialogService.SaveFileDialog())
                                    {
                                        fileService.Save(dialogService.Path, Employes.ToList());
                                        dialogService.ShowMsg($"Employes save to file {dialogService.Path}");
                                    }
                                }
                                
                            }
                            catch(Exception ex) 
                            {
                                dialogService.ShowMsg(ex.Message);
                            }
                        }
                        );
                }
                
                return saveCommand;
            }
        }

        // command open file and read employes
        private AppCommand openCommand;
        public AppCommand OpenCommand
        {
            get
            {
                if(openCommand is null)
                {
                    openCommand = new AppCommand(
                        obj =>
                        {
                            try
                            {
                                if(dialogService.OpenFileDialog())
                                {
                                    var employes = fileService.Open(dialogService.Path);
                                    Employes.Clear();
                                    foreach (var employe in employes)
                                        Employes.Add(employe);
                                    dialogService.ShowMsg($"Employes read from file {dialogService.Path}");
                                }
                            }
                            catch (Exception ex)
                            {
                                dialogService.ShowMsg(ex.Message);
                            }
                        }
                        );
                }
                return openCommand;
            }
        }

        // constructor
        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
        }

        // interface INotifyPropertyChange
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
        }
    }
}
