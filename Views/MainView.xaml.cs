using ISEPresenter.ViewModels;
using Microsoft.PowerShell.Host.ISE;
using System;
using System.Collections.Generic;
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


namespace ISEPresenter.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MainView : UserControl, IAddOnToolHostObject
    {
        private readonly MainViewModel _MainViewModel;


        public MainView()
        {
            InitializeComponent();

            _MainViewModel = new MainViewModel(this);
            DataContext = _MainViewModel;
        }


        /// <summary>
        /// Reference to the ISE object model.
        /// </summary>
        public ObjectModelRoot HostObject
        {
            get;
            set;
        }


        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            _MainViewModel.Play();
        }


        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            _MainViewModel.Pause();
        }


        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            _MainViewModel.Stop();
        }
    }
}
