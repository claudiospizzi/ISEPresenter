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
        #region Members

        private readonly MainViewModel _MainViewModel;

        #endregion


        #region Properties

        /// <summary>
        /// Reference to the ISE object model.
        /// </summary>
        public ObjectModelRoot HostObject
        {
            get;
            set;
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Initialize user control and the view model.
        /// </summary>
        public MainView()
        {
            InitializeComponent();

            _MainViewModel = new MainViewModel(this);

            DataContext = _MainViewModel;
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Pass-throught method for play command.
        /// </summary>
        /// <param name="sender">The sender button.</param>
        /// <param name="e">Event arguments.</param>
        private void PlayCommands(object sender, RoutedEventArgs e)
        {
            _MainViewModel.PlayCommand();
        }

        /// <summary>
        /// Pass-throught method for pause command.
        /// </summary>
        /// <param name="sender">The sender button.</param>
        /// <param name="e">Event arguments.</param>
        private void PauseCommands(object sender, RoutedEventArgs e)
        {
            _MainViewModel.PauseCommand();
        }

        /// <summary>
        /// Pass-throught method for stop command.
        /// </summary>
        /// <param name="sender">The sender button.</param>
        /// <param name="e">Event arguments.</param>
        private void StopCommands(object sender, RoutedEventArgs e)
        {
            _MainViewModel.StopCommand();
        }

        #endregion
    }
}
