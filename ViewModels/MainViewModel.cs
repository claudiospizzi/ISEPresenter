using ISEPresenter.Models;
using Stateless;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;


namespace ISEPresenter.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private StateMachine<PresenterState, PresenterTrigger> _StateMachine;


        private IReadOnlyCollection<IRemoteControlDevice> _RemoteControlDeviceList;


        private IRemoteControlDevice _RemoteControlDevice;


        private bool _ConfigurationSelectNextExpression;


        public bool ButtonPlayEnabled
        {
            get { return _StateMachine.IsInState(PresenterState.Ready) || _StateMachine.IsInState(PresenterState.Paused); }
        }


        public string ButtonPlayLabel
        {
            get { return _StateMachine.IsInState(PresenterState.Paused) ? "Resume" : "Play"; }
        }


        public bool ButtonPauseEnabled
        {
            get { return _StateMachine.IsInState(PresenterState.Running); }
        }


        public bool ButtonStopEnabled
        {
            get { return _StateMachine.IsInState(PresenterState.Running) || _StateMachine.IsInState(PresenterState.Paused); }
        }


        public string ButtonStopLabel
        {
            get { return _StateMachine.IsInState(PresenterState.Paused) ? "Cancel" : "Stop"; }
        }


        public ObservableCollection<string> RemoteControlDeviceNameList
        {
            get;
            private set;
        }


        public string RemoteControlDeviceName
        {
            get { return _RemoteControlDevice.DeviceName; }
            set { SwitchRemoteControlDevice(value); }
        }


        public bool RemoteControlDeviceEnabled
        {
            get { return _StateMachine.IsInState(PresenterState.Ready); }
        }


        public string RemoteControlRunDescription
        {
            get { return _RemoteControlDevice.RunDescription; }
        }


        public string RemoteControlClearDescription
        {
            get { return _RemoteControlDevice.ClearDescription; }
        }


        public string RemoteControlBackDescription
        {
            get { return _RemoteControlDevice.BackDescription; }
        }


        public string RemoteControlForwardDescription
        {
            get { return _RemoteControlDevice.ForwardDescription; }
        }


        public bool ConfigurationSelectNextExpression
        {
            get { return _ConfigurationSelectNextExpression; }
            set { _ConfigurationSelectNextExpression = value; OnPropertyChanged(); }
        }


        public bool ConfigurationEnabled
        {
            get { return _StateMachine.IsInState(PresenterState.Ready); }
        }


        public bool StatusProgressBarEnabled
        {
            get { return _StateMachine.IsInState(PresenterState.Running); }
        }


        public double StatusProgressBarValue
        {
            get { return _StateMachine.IsInState(PresenterState.Paused) ? 1 : 0; }
        }


        public string StatusLabelValue
        {
            get { return _StateMachine.State.ToString(); }
        }

        public MainViewModel()
        {
            // Enumerate all devices and show them in the combo box
            _RemoteControlDeviceList = new ReadOnlyCollection<IRemoteControlDevice>(RemoteControlDeviceBase.GetDeviceList().ToList()); 
            RemoteControlDeviceNameList = new ObservableCollection<string>(_RemoteControlDeviceList.Select(d => d.DeviceName));

            // Select the first device in the list as the active device
            SwitchRemoteControlDevice(_RemoteControlDeviceList.First());

            // Initialize the defualt configuraiton
            ConfigurationSelectNextExpression = false;

            // Initialize the state machine
            InitializeStateMachine();
        }


        public void Play()
        {
            if (_StateMachine.IsInState(PresenterState.Ready))
            {
                _StateMachine.Fire(PresenterTrigger.Start);
            }

            if (_StateMachine.IsInState(PresenterState.Paused))
            {
                _StateMachine.Fire(PresenterTrigger.Resume);
            }
        }


        public void Pause()
        {
            if (_StateMachine.IsInState(PresenterState.Running))
            {
                _StateMachine.Fire(PresenterTrigger.Suspend);
            }
        }


        public void Stop()
        {
            if (_StateMachine.IsInState(PresenterState.Running))
            {
                _StateMachine.Fire(PresenterTrigger.Stop);
            }

            if (_StateMachine.IsInState(PresenterState.Paused))
            {
                _StateMachine.Fire(PresenterTrigger.Cancel);
            }
        }


        private void InitializeStateMachine()
        {
            _StateMachine = new StateMachine<PresenterState, PresenterTrigger>(PresenterState.Ready);

            _StateMachine.OnTransitioned(s => OnStateChanged());

            _StateMachine.Configure(PresenterState.Ready)
                .OnEntryFrom(PresenterTrigger.Stop, s => StopTrigger())
                .OnEntryFrom(PresenterTrigger.Cancel, s => CancelTrigger())
                .Permit(PresenterTrigger.Start, PresenterState.Running);

            _StateMachine.Configure(PresenterState.Running)
                .OnEntryFrom(PresenterTrigger.Start, s => StartTrigger())
                .OnEntryFrom(PresenterTrigger.Resume, s => ResumeTrigger())
                .Permit(PresenterTrigger.Stop, PresenterState.Ready)
                .Permit(PresenterTrigger.Suspend, PresenterState.Paused);

            _StateMachine.Configure(PresenterState.Paused)
                .OnEntryFrom(PresenterTrigger.Suspend, s => SuspendTrigger())
                .Permit(PresenterTrigger.Resume, PresenterState.Running)
                .Permit(PresenterTrigger.Cancel, PresenterState.Ready);
        }


        private void StartTrigger()
        {
            Debug.WriteLine("StartTrigger()");
        }


        private void StopTrigger()
        {
            Debug.WriteLine("StopTrigger()");
        }


        private void SuspendTrigger()
        {
            Debug.WriteLine("SuspendTrigger()");
        }


        private void ResumeTrigger()
        {
            Debug.WriteLine("ResumeTrigger()");
        }


        private void CancelTrigger()
        {
            Debug.WriteLine("CancelTrigger()");
        }


        private void OnStateChanged()
        {
            OnPropertyChanged("ButtonPlayEnabled");
            OnPropertyChanged("ButtonPlayLabel");
            OnPropertyChanged("ButtonPauseEnabled");
            OnPropertyChanged("ButtonStopEnabled");
            OnPropertyChanged("ButtonStopLabel");

            OnPropertyChanged("RemoteControlDeviceEnabled");

            OnPropertyChanged("ConfigurationEnabled");

            OnPropertyChanged("StatusProgressBarEnabled");
            OnPropertyChanged("StatusProgressBarValue");
            OnPropertyChanged("StatusLabel");
        }


        private void SwitchRemoteControlDevice(string deviceName)
        {
            SwitchRemoteControlDevice(_RemoteControlDeviceList.Where<IRemoteControlDevice>(d => d.DeviceName == deviceName).First<IRemoteControlDevice>());
        }


        private void SwitchRemoteControlDevice(IRemoteControlDevice device)
        {
            _RemoteControlDevice = device;


            OnPropertyChanged("RemoteControlDeviceName");
            OnPropertyChanged("RemoteControlPlayDescription");
            OnPropertyChanged("RemoteControlClearDescription");
            OnPropertyChanged("RemoteControlBackDescription");
            OnPropertyChanged("RemoteControlForwardDescription");
        }


        ///// <summary>
        ///// Update the current state to the necessary target state.
        ///// </summary>
        ///// <param name="state">The new state.</param>
        //private void SetState(PresenterState state)
        //{
        //    _State = state;

        //    // Change notification!
        //}


    }
}
