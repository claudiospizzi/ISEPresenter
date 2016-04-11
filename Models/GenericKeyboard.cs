using ISEPresenter.Properties;
using System;
using System.Windows.Forms;


namespace ISEPresenter.Models
{
    /// <summary>
    /// Generic keyboard remote control device.
    /// </summary>
    public class GenericKeyboard : RemoteControlDeviceBase
    {
        #region Constructors

        /// <summary>
        /// Initialize a generic keyboard remote control.
        /// </summary>
        public GenericKeyboard()
        {
            DeviceName = Resources.RemoteControlDevice_GenericKeyboard_DeviceName;

            RunDescription     = Resources.RemoteControlDevice_GenericKeyboard_RunDescription;
            ClearDescription   = Resources.RemoteControlDevice_GenericKeyboard_ClearDescription;
            BackDescription    = Resources.RemoteControlDevice_GenericKeyboard_BackDescription;
            ForwardDescription = Resources.RemoteControlDevice_GenericKeyboard_ForwardDescription;
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Implement the custom key down handler method to match a generic keybord device.
        /// </summary>
        /// <param name="sender">The key event sender.</param>
        /// <param name="e">Information about the pressed key.</param>
        protected override void KeyboardEventKeyDown(Object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    OnRun();
                    break;

                case Keys.F8:
                    OnRun();
                    break;

                case Keys.Delete:
                    OnClear();
                    break;

                case Keys.PageUp:
                    OnBack();
                    break;

                case Keys.PageDown:
                    OnForward();
                    break;

                default:
                    return;
            }

            e.Handled = true;
        }

        #endregion
    }
}
