using Microsoft.PowerShell.Host.ISE;
using System;
using System.Collections.Generic;
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

        #endregion


        #region Constructors

        /// <summary>
        /// Initialize all settings with default values.
        /// </summary>
        public ExecutionViewModel()
        {
            _Statements = new List<IScriptExtent>();
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

            IEnumerable<Ast> astBlocks = astRoot.FindAll((a) => a is NamedBlockAst, false);

            foreach (Ast astBlock in astBlocks)
            {
                foreach (StatementAst astStatement in ((NamedBlockAst)astBlock).Statements)
                {
                    if (astStatement.Extent.Text != "break")
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

            _ParseToken = null;
            _ParseError = null;
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
                _Host.CurrentPowerShellTab.Invoke(_Host.CurrentPowerShellTab.Files.SelectedFile.Editor.SelectedText);

                if (_SelectNextStatementAfterRun && (_StatementIndex + 1) != _Statements.Count)
                {
                    MoveForward();
                }
            }
        }

        #endregion
    }
}
