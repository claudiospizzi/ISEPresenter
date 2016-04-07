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

        private bool _SkipTopBreakStatement;

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

        /// <summary>
        /// This setting defines, if the a break which is on top of a file (line 1)
        /// will be skipped during execution.
        /// </summary>
        public bool SkipTopBreakStatement
        {
            get
            {
                return _SkipTopBreakStatement;
            }
            set
            {
                if (_SkipTopBreakStatement != value)
                {
                    _SkipTopBreakStatement = value;
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
            SkipTopBreakStatement       = true;
            SelectNextStatementAfterRun = true;
        }

        #endregion
    }
}
