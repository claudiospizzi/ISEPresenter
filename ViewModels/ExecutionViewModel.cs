using Microsoft.PowerShell.Host.ISE;
using System;
using System.Collections.Generic;
using System.Management.Automation.Language;
using System.Windows;


namespace ISEPresenter.ViewModels
{
    /// <summary>
    /// All information about the current execution state.
    /// </summary>
    public class ExecutionViewModel : BaseViewModel
    {
        #region Members

        private ObjectModelRoot _Host;

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
                    throw new Exception("Statement not available!");
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
                    throw new Exception("Statement index not available!");
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
                    throw new Exception("Statement count not available!");
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
        public void Initialize(ObjectModelRoot host)
        {
            _Host = host;

            _StatementIndex = 0;

            _ParseToken = null;
            _ParseError = null;

            ScriptBlockAst astRoot = Parser.ParseInput(_Host.CurrentPowerShellTab.Files.SelectedFile.Editor.Text, out _ParseToken, out _ParseError);

            IEnumerable<Ast> astBlocks = astRoot.FindAll((a) => a is NamedBlockAst, false);

            foreach (Ast astBlock in astBlocks)
            {
                foreach (StatementAst astStatement in ((NamedBlockAst)astBlock).Statements)
                {
                    _Statements.Add(astStatement.Extent);
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
            }
        }

        #endregion
    }
}
