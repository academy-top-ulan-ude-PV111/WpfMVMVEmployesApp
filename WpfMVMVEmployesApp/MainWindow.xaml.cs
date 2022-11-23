using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMVMVEmployesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationViewModel applicationViewModel;
        public MainWindow()
        {
            InitializeComponent();

            applicationViewModel = new ApplicationViewModel();
            applicationViewModel.Employes =  new ObservableCollection<Employe>(new EmployesList().Employes);
            this.DataContext = applicationViewModel;
        }
    }
}
