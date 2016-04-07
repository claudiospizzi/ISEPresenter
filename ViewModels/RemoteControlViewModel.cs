using ISEPresenter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace ISEPresenter.ViewModels
{
    /// <summary>
    /// All information regarding the remote control inside one view model.
    /// </summary>
    public class RemoteControlViewModel : BaseViewModel
    {
        #region Members

        private IReadOnlyCollection<IRemoteControlDevice> _DeviceList;

        private IRemoteControlDevice _CurrentDevice;

        #endregion


        #region Properties

        /// <summary>
        /// List of all device names usable for the UI.
        /// </summary>
        public IReadOnlyCollection<IRemoteControlDevice> DeviceList
        {
            get
            {
                return _DeviceList;
            }
        }

        /// <summary>
        /// List of all device names usable for the UI.
        /// </summary>
        public ObservableCollection<string> DeviceNameList
        {
            get;
            private set;
        }

        /// <summary>
        /// The current device.
        /// </summary>
        public IRemoteControlDevice CurrentDevice
        {
            get
            {
                return _CurrentDevice;
            }
        }

        /// <summary>
        /// The current device name. Cen be used to set a new device
        /// </summary>
        public string CurrentDeviceName
        {
            get
            {
                return _CurrentDevice.DeviceName;
            }
            set
            {
                if (_CurrentDevice.DeviceName != value)
                {
                    SetCurrentDevice(_DeviceList.Where(d => d.DeviceName == value).First());
                }
            }
        }

        /// <summary>
        /// Property for the device run button description.
        /// </summary>
        public string CurrentDeviceRunDescription
        {
            get
            {
                return _CurrentDevice.RunDescription;
            }
        }

        /// <summary>
        /// Property for the device clear button description.
        /// </summary>
        public string CurrentDeviceClearDescription
        {
            get
            {
                return _CurrentDevice.ClearDescription;
            }
        }

        /// <summary>
        /// Property for the device back button description
        /// </summary>
        public string CurrentDeviceBackDescription
        {
            get
            {
                return _CurrentDevice.BackDescription;
            }
        }

        /// <summary>
        /// Property for the device forward button description
        /// </summary>
        public string CurrentDeviceForwardDescription
        {
            get
            {
                return _CurrentDevice.ForwardDescription;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Initialize the device list and the default device.
        /// </summary>
        public RemoteControlViewModel()
        {

            _DeviceList = new ReadOnlyCollection<IRemoteControlDevice>(GenerateDeviceList());
            _CurrentDevice = _DeviceList.Where(d => d is GenericKeyboard).First();

            DeviceNameList = new ObservableCollection<string>(_DeviceList.Select(d => d.DeviceName));
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Generate a list of all available devices. In fact all implementations
        /// of the abstract RemoteControlDeviceBase class.
        /// </summary>
        /// <returns></returns>
        private IList<IRemoteControlDevice> GenerateDeviceList()
        {
            return typeof(RemoteControlDeviceBase).Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(RemoteControlDeviceBase)))
                .Select(t => (IRemoteControlDevice)Activator.CreateInstance(t))
                .ToList();
        }

        /// <summary>
        /// Updates the current device and notifies all subscribers.
        /// </summary>
        /// <param name="device">The new device.</param>
        private void SetCurrentDevice(IRemoteControlDevice device)
        {
            _CurrentDevice.UnregisterHock();

            _CurrentDevice = device;

            OnPropertyChanged("CurrentDevice");
            OnPropertyChanged("CurrentDeviceName");
            OnPropertyChanged("CurrentDeviceRunDescription");
            OnPropertyChanged("CurrentDeviceClearDescription");
            OnPropertyChanged("CurrentDeviceBackDescription");
            OnPropertyChanged("CurrentDeviceForwardDescription");
        }

        #endregion
    }
}
