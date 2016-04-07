using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISEPresenter.Models
{
    public abstract class RemoteControlDeviceBase : IRemoteControlDevice
    {
        #region Events

        /// <summary>
        /// Key down event for the run action.
        /// </summary>
        public event EventHandler Run;

        /// <summary>
        /// Key down event for the clear action.
        /// </summary>
        public event EventHandler Clear;

        /// <summary>
        /// Key down event for the back action.
        /// </summary>
        public event EventHandler Back;

        /// <summary>
        /// Key down event for the forward action.
        /// </summary>
        public event EventHandler Forward;

        #endregion


        #region Members

        /// <summary>
        /// Keyboard event handler objects.
        /// </summary>
        private IKeyboardMouseEvents _KeyboardEvent;

        #endregion


        #region Properties

        /// <summary>
        /// The device name.
        /// </summary>
        public string DeviceName
        {
            get;
            protected set;
        }

        /// <summary>
        /// Description of the run button.
        /// </summary>

        public string RunDescription
        {
            get;
            protected set;
        }

        /// <summary>
        /// Description of the clear button.
        /// </summary>
        public string ClearDescription
        {
            get;
            protected set;
        }

        /// <summary>
        /// Description of the back button.
        /// </summary>
        public string BackDescription
        {
            get;
            protected set;
        }

        /// <summary>
        /// Description of the forward button.
        /// </summary>
        public string ForwardDescription
        {
            get;
            protected set;
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Initialize the app event hock.
        /// </summary>
        public RemoteControlDeviceBase()
        {
            _KeyboardEvent = Hook.AppEvents();
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Method to capture the key event and redirect it to the child classes.
        /// </summary>
        /// <param name="sender">The key event sender.</param>
        /// <param name="e">Information about the pressed key.</param>
        protected abstract void KeyboardEventKeyDown(Object sender, KeyEventArgs e);

        /// <summary>
        /// Fire the run event.
        /// </summary>
        public void OnRun()
        {
            Run?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fire the clear event.
        /// </summary>
        public void OnClear()
        {
            Clear?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fire the back event.
        /// </summary>
        public void OnBack()
        {
            Back?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fire the forward event.
        /// </summary>
        public void OnForward()
        {
            Forward?.Invoke(this, new EventArgs());
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Register this class to the key down event.
        /// </summary>
        public void RegisterHock()
        {
            _KeyboardEvent.KeyDown += KeyboardEventKeyDown;
        }

        /// <summary>
        /// Unregister this class from the key down event.
        /// </summary>
        public void UnregisterHock()
        {
            _KeyboardEvent.KeyDown -= KeyboardEventKeyDown;
        }

        #endregion
    }
}
