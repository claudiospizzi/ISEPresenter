using System;


namespace ISEPresenter.Models
{
    /// <summary>
    /// General interface for remote control devices.
    /// </summary>
    public interface IRemoteControlDevice
    {
        #region Events

        /// <summary>
        /// Key down event for the run action.
        /// </summary>
        event EventHandler Run;

        /// <summary>
        /// Key down event for the clear action.
        /// </summary>
        event EventHandler Clear;

        /// <summary>
        /// Key down event for the back action.
        /// </summary>
        event EventHandler Back;

        /// <summary>
        /// Key down event for the forward action.
        /// </summary>
        event EventHandler Forward;

        #endregion


        #region Properties

        /// <summary>
        /// The device name.
        /// </summary>
        string DeviceName { get; }

        /// <summary>
        /// Description of the run button.
        /// </summary>
        string RunDescription { get; }

        /// <summary>
        /// Description of the clear button.
        /// </summary>
        string ClearDescription { get; }

        /// <summary>
        /// Description of the back button.
        /// </summary>
        string BackDescription { get; }

        /// <summary>
        /// Description of the forward button.
        /// </summary>
        string ForwardDescription { get; }

        #endregion


        #region Public Methods

        /// <summary>
        /// Register and enable the hock.
        /// </summary>
        void RegisterHock();

        /// <summary>
        /// Disable and unregister the hock
        /// </summary>
        void UnregisterHock();

        #endregion
    }
}
