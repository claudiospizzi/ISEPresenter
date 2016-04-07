using System;


namespace ISEPresenter.ViewModels
{
    /// <summary>
    /// All configuration settings for the add-on.
    /// </summary>
    public class ConfigurationViewModel : BaseViewModel
    {
        #region Members

        private bool _SelectNextStatementAfterRun;

        #endregion


        #region Properties

        /// <summary>
        /// This setting defines, if the next statement is selected inside the
        /// ISE as soon as the current statement was executed.
        /// </summary>
        public bool SelectNextStatementAfterRun
        {
            get
            {
                return _SelectNextStatementAfterRun;
            }
            set
            {
                if (_SelectNextStatementAfterRun != value)
                {
                    _SelectNextStatementAfterRun = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion


        #region Constructurs

        /// <summary>
        /// Initialize all settings with default values.
        /// </summary>
        public ConfigurationViewModel()
        {
            SelectNextStatementAfterRun = true;
        }

        #endregion
    }
}
