using System;


namespace ISEPresenter.Models
{
    /// <summary>
    /// All configuration settings for the presenter execution.
    /// </summary>
    public class PresenterConfiguration
    {
        # region Members

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
                _SelectNextStatementAfterRun = value;
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
                _SkipTopBreakStatement = value;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Initialize all settings with default values.
        /// </summary>
        public PresenterConfiguration()
        {
            _SelectNextStatementAfterRun = true;
            _SkipTopBreakStatement = true;
        }

        #endregion
    }
}
