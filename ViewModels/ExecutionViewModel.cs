using Microsoft.PowerShell.Host.ISE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation.Language;


namespace ISEPresenter.ViewModels
{
    /// <summary>
    /// All information about the current execution state.
    /// </summary>
    public class ExecutionViewModel : BaseViewModel
    {
        #region Members

        private ObjectModelRoot _Host;

        private bool _SelectNextStatementAfterRun;

        private int _StatementIndex;

        private IList<IScriptExtent> _Statements;

        private Token[] _ParseToken;

        private ParseError[] _ParseError;

        private string _ParserFile;

        //private string _Duration;

        #endregion


        #region Properties

        /// <summary>
        /// The current active statement.
        /// </summary>
        public IScriptExtent CurrentStatement
        {
            get
            {
                if (_StatementIndex < 0 || _StatementIndex >= _Statements.Count)
                {
                    return null;
                }

                return _Statements[_StatementIndex];
            }
        }

        /// <summary>
        /// The current statement index.
        /// </summary>
        public int CurrentStatementIndex
        {
            get
            {
                if (_StatementIndex < 0 || _StatementIndex >= _Statements.Count)
                {
                    return -1;
                }

                return _StatementIndex;
            }
        }

        /// <summary>
        /// The number of available statements.
        /// </summary>
        public int StatementCount
        {
            get
            {
                if (_StatementIndex < 0 || _StatementIndex >= _Statements.Count)
                {
                    return -1;
                }

                return _Statements.Count;
            }
        }

        /// <summary>
        /// The current file which has beed parsed.
        /// </summary>
        public string ParserFile
        {
            get
            {
                return _ParserFile;
            }
            private set
            {
                if (_ParserFile != value)
                {
                    _ParserFile = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Number of parsed tokens during the last initialization.
        /// </summary>
        public int TokenCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Number of syntax errors detected during the last 
        /// </summary>
        public int ErrorCount
        {
            get;
            private set;
        }

        /// <summary>
        /// The duration of the last execution.
        /// </summary>
        //public string Duration
        //{
        //    get
        //    {
        //        return _Duration;
        //    }
        //    private set
        //    {
        //        if (_Duration != value)
        //        {
        //            _Duration = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        #endregion


        #region Constructors

        /// <summary>
        /// Initialize all settings with default values.
        /// </summary>
        public ExecutionViewModel()
        {
            _Statements = new List<IScriptExtent>();

            TokenCount = -1;
            ErrorCount = -1;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public void Initialize(ObjectModelRoot host, bool selectNextStatementAfterRun, bool skipTopBreakStatement)
        {
            _Host = host;

            _SelectNextStatementAfterRun = selectNextStatementAfterRun;

            _StatementIndex = 0;

            _ParseToken = null;
            _ParseError = null;

            ScriptBlockAst astRoot = Parser.ParseInput(_Host.CurrentPowerShellTab.Files.SelectedFile.Editor.Text, out _ParseToken, out _ParseError);

            ParserFile = _Host.CurrentPowerShellTab.Files.SelectedFile.DisplayName;
            TokenCount = _ParseToken.Length;
            ErrorCount = _ParseError.Length;
            //Duration   = "n/a";

            IEnumerable<Ast> astBlocks = astRoot.FindAll((a) => a is NamedBlockAst, false);

            foreach (Ast astBlock in astBlocks)
            {
                foreach (StatementAst astStatement in ((NamedBlockAst)astBlock).Statements)
                {
                    if (astStatement.Extent.Text != "break" || !skipTopBreakStatement)
                    {
                        _Statements.Add(astStatement.Extent);
                    }
                }
            }
        }

        /// <summary>
        /// Reset the state to noting.
        /// </summary>
        public void Reset()
        {
            _Host = null;

            _StatementIndex = -1;
            _Statements.Clear();

            ParserFile = string.Empty;
            //Duration   = string.Empty;

            _ParseToken = null;
            _ParseError = null;

            TokenCount = -1;
            ErrorCount = -1;
        }

        /// <summary>
        /// Move the index to the previous statement.
        /// </summary>
        public void MoveBack()
        {
            _StatementIndex = (_StatementIndex + _Statements.Count - 1) % _Statements.Count;

            SelectCurrent();

            if ((CurrentStatement.StartLineNumber - 1) > 1)
            {
                _Host.CurrentPowerShellTab.Files.SelectedFile.Editor.EnsureVisible(CurrentStatement.StartLineNumber - 1);
            }
        }

        /// <summary>
        /// Move the index to the next statement.
        /// </summary>
        public void MoveForward()
        {
            _StatementIndex = (_StatementIndex + _Statements.Count + 1) % _Statements.Count;

            SelectCurrent();

            if ((CurrentStatement.StartLineNumber + 1) < _Host.CurrentPowerShellTab.Files.SelectedFile.Editor.LineCount)
            {
                _Host.CurrentPowerShellTab.Files.SelectedFile.Editor.EnsureVisible(CurrentStatement.StartLineNumber + 1);
            }
        }

        /// <summary>
        /// Select the current statement.
        /// </summary>
        public void SelectCurrent()
        {
            _Host.CurrentPowerShellTab.Files.SelectedFile.Editor.Select(CurrentStatement.StartLineNumber,
                                                                        CurrentStatement.StartColumnNumber,
                                                                        CurrentStatement.EndLineNumber,
                                                                        CurrentStatement.EndColumnNumber);
        }

        /// <summary>
        /// Select the current statement and execute it.
        /// </summary>
        public void ExecuteCurrent()
        {
            SelectCurrent();

            if (_Host.CurrentPowerShellTab.CanInvoke)
            {
                //Stopwatch watch = new Stopwatch();
                //watch.Start();

                _Host.CurrentPowerShellTab.Invoke(_Host.CurrentPowerShellTab.Files.SelectedFile.Editor.SelectedText);

                //watch.Stop();
                //Duration = string.Format("{0} s", Math.Round(watch.Elapsed.TotalSeconds, 2));

                if (_SelectNextStatementAfterRun && (_StatementIndex + 1) != _Statements.Count)
                {
                    MoveForward();
                }
            }
        }

        #endregion
    }
}
