using ISEPresenter.Models;
using Microsoft.PowerShell.Host.ISE;
using Stateless;
using System;
using System.Management.Automation.Language;


namespace ISEPresenter.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Enumerations

        /// <summary>
        /// Defines the possible application states.
        /// </summary>
        public enum State
        {
            Ready,
            Running,
            Paused
        }

        /// <summary>
        /// Defines the possible triggers to change between application states.
        /// </summary>
        public enum Trigger
        {
            Start,
            Stop,
            Suspend,
            Resume,
            Cancel
        }

        #endregion


        #region Members

        private IAddOnToolHostObject _Host;

        private StateMachine<State, Trigger> _StateMachine;

        #endregion


        #region Properties

        /// <summary>
        /// Access to the remote control view model.
        /// </summary>
        public RemoteControlViewModel RemoteControl
        {
            get;
            private set;
        }

        /// <summary>
        /// Access to the configuration view model.
        /// </summary>
        public ConfigurationViewModel Configuration
        {
            get;
            private set;
        }

        /// <summary>
        /// Based on the current state, return if the play command can be executed.
        /// </summary>
        public bool CommandPlayEnabled
        {
            get
            {
                return _StateMachine.CanFire(Trigger.Start) || _StateMachine.CanFire(Trigger.Resume);
            }
        }

        /// <summary>
        /// Based on the current state, return the play command label.
        /// </summary>
        public string CommandPlayLabel
        {
            get
            {
                return _StateMachine.IsInState(State.Paused) ? "Resume" : "Play";
            }
        }

        /// <summary>
        /// Based on the current state, return if the pause command can be executed.
        /// </summary>
        public bool CommandPauseEnabled
        {
            get
            {
                return _StateMachine.CanFire(Trigger.Suspend);
            }
        }

        /// <summary>
        /// Based on the current state, return the pause command label.
        /// </summary>
        public string CommandPauseLabel
        {
            get
            {
                return "Pause";
            }
        }

        /// <summary>
        /// Based on the current state, return if the stop command can be executed.
        /// </summary>
        public bool CommandStopEnabled
        {
            get
            {
                return _StateMachine.CanFire(Trigger.Stop) || _StateMachine.CanFire(Trigger.Cancel);
            }
        }

        /// <summary>
        /// Based on the current state, return the stop command label.
        /// </summary>
        public string CommandStopLabel
        {
            get
            {
                return _StateMachine.IsInState(State.Paused) ? "Resume" : "Play";
            }
        }

        /// <summary>
        /// Based on the current state, return if input to the remote device combo
        /// box and the configuration options is enabled.
        /// </summary>
        public bool InputEnabled
        {
            get
            {
                return _StateMachine.IsInState(State.Ready);
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Initialize this and all child view models.
        /// </summary>
        public MainViewModel(IAddOnToolHostObject host)
        {
            _Host = host;

            RemoteControl = new RemoteControlViewModel();
            Configuration = new ConfigurationViewModel();

            InitializeStateMachine();
            InitializeDeviceEvent();
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Define the state machine with all states, transitions (triggers) and
        /// add hocks to the states to execute the business logic.
        /// </summary>
        private void InitializeStateMachine()
        {
            _StateMachine = new StateMachine<State, Trigger>(State.Ready);

            _StateMachine.OnTransitioned(s => StateUpdate());

            _StateMachine.Configure(State.Ready)
                .OnEntryFrom(Trigger.Stop, s => StopTrigger())
                .OnEntryFrom(Trigger.Cancel, s => CancelTrigger())
                .Permit(Trigger.Start, State.Running);

            _StateMachine.Configure(State.Running)
                .OnEntryFrom(Trigger.Start, s => StartTrigger())
                .OnEntryFrom(Trigger.Resume, s => ResumeTrigger())
                .Permit(Trigger.Stop, State.Ready)
                .Permit(Trigger.Suspend, State.Paused);

            _StateMachine.Configure(State.Paused)
                .OnEntryFrom(Trigger.Suspend, s => SuspendTrigger())
                .Permit(Trigger.Resume, State.Running)
                .Permit(Trigger.Cancel, State.Ready);
        }

        /// <summary>
        /// Hock method executed after the state was changed.
        /// </summary>
        private void StateUpdate()
        {
            OnPropertyChanged("CommandPlayEnabled");
            OnPropertyChanged("CommandPlayLabel");

            OnPropertyChanged("CommandPauseEnabled");
            OnPropertyChanged("CommandPauseLabel");

            OnPropertyChanged("CommandStopEnabled");
            OnPropertyChanged("CommandStopLabel");

            OnPropertyChanged("InputEnabled");
        }

        /// <summary>
        /// Start the presentation mode.
        /// </summary>
        private void StartTrigger()
        {
            RemoteControl.CurrentDevice.RegisterHock();

            // ToDo: Parse file!
        }

        /// <summary>
        /// Stop a running presentation.
        /// </summary>
        private void StopTrigger()
        {
            RemoteControl.CurrentDevice.UnregisterHock();

            // ToDo: Clear parsed file!
        }

        /// <summary>
        /// Suspend a running presentation.
        /// </summary>
        private void SuspendTrigger()
        {
            RemoteControl.CurrentDevice.UnregisterHock();
        }

        /// <summary>
        /// Resume a suspended presentation.
        /// </summary>
        private void ResumeTrigger()
        {
            RemoteControl.CurrentDevice.RegisterHock();
        }

        /// <summary>
        /// Cancel a suspended presentation without resuming it.
        /// </summary>
        private void CancelTrigger()
        {
            RemoteControl.CurrentDevice.UnregisterHock();
        }

        /// <summary>
        /// Subscribe to the events run, clear, back and forward from all
        /// available devices.
        /// </summary>
        private void InitializeDeviceEvent()
        {
            foreach (IRemoteControlDevice device in RemoteControl.DeviceList)
            {
                device.Run     += RunAction;
                device.Clear   += ClearAction;
                device.Back    += BackAction;
                device.Forward += ForwardAction;
            }
        }

        private void RunAction(Object sender, EventArgs e)
        {
            if (_StateMachine.IsInState(State.Running))
            {
                // ToDo
                _Host.HostObject.CurrentPowerShellTab.Files.SelectedFile.Editor.Select(3, 1, 3, 23);
                string code = _Host.HostObject.CurrentPowerShellTab.Files.SelectedFile.Editor.Text;
                Token[] tokens;
                ParseError[] errors = null;

                ScriptBlockAst ast = Parser.ParseInput(code, out tokens, out errors);

                // System.Management.Automation.Language.NamedBlockAst  .Statements !!

                // TODO !!!!
            }
        }

        private void ClearAction(Object sender, EventArgs e)
        {
            if (_StateMachine.IsInState(State.Running))
            {
                // ToDo

            }
        }

        private void BackAction(Object sender, EventArgs e)
        {
            if (_StateMachine.IsInState(State.Running))
            {
                // ToDo

            }
        }

        private void ForwardAction(Object sender, EventArgs e)
        {
            if (_StateMachine.IsInState(State.Running))
            {
                // ToDo

            }
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Try to start a new or resume the current presentation.
        /// </summary>
        public void Play()
        {
            if (_StateMachine.CanFire(Trigger.Start))
            {
                _StateMachine.Fire(Trigger.Start);
            }
            else if (_StateMachine.CanFire(Trigger.Resume))
            {
                _StateMachine.Fire(Trigger.Resume);
            }
        }

        /// <summary>
        /// Try to pause a suspended presentation.
        /// </summary>
        public void Pause()
        {
            if (_StateMachine.CanFire(Trigger.Suspend))
            {
                _StateMachine.Fire(Trigger.Suspend);
            }
        }

        /// <summary>
        /// Stop or cancel a current presentation.
        /// </summary>
        public void Stop()
        {
            if (_StateMachine.CanFire(Trigger.Stop))
            {
                _StateMachine.Fire(Trigger.Stop);
            }
            else if (_StateMachine.CanFire(Trigger.Cancel))
            {
                _StateMachine.Fire(Trigger.Cancel);
            }
        }

        #endregion
    }
}
