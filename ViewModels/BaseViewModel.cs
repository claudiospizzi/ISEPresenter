using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ISEPresenter.ViewModels
{
    /// <summary>
    /// Base view model to support the property changed event on all view models.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region Private Methods

        /// <summary>
        /// Method to inform all subscribers that a property has changed.
        /// </summary>
        /// <param name="propertyName">The property name which has changed.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
