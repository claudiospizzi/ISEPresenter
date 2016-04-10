using ISEPresenter.Models;


namespace ISEPresenter.ViewModels
{
    /// <summary>
    /// All configuration settings for the add-on.
    /// </summary>
    public class ConfigurationViewModel : BaseViewModel
    {
        #region Members

        private PresenterConfiguration _Configuration;

        #endregion


        #region Properties

        /// <summary>
        /// Get the whole configuration object.
        /// </summary>
        //public PresenterConfiguration Configuration
        //{
        //    get
        //    {
        //        return _Configuration;
        //    }
        //}

        /// <summary>
        /// This setting defines, if the next statement is selected inside the
        /// ISE as soon as the current statement was executed.
        /// </summary>
        public bool SelectNextStatementAfterRun
        {
            get
            {
                return _Configuration.SelectNextStatementAfterRun;
            }
            set
            {
                if (_Configuration.SelectNextStatementAfterRun != value)
                {
                    _Configuration.SelectNextStatementAfterRun = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This setting defines, if the a break which is on top of a file (line 1)
        /// will be skipped during execution.
        /// </summary>
        public bool SkipTopBreakStatement
        {
            get
            {
                return _Configuration.SkipTopBreakStatement;
            }
            set
            {
                if (_Configuration.SkipTopBreakStatement != value)
                {
                    _Configuration.SkipTopBreakStatement = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Initialize all settings with default values.
        /// </summary>
        public ConfigurationViewModel()
        {
            _Configuration = new PresenterConfiguration();
        }

        #endregion
    }
}
