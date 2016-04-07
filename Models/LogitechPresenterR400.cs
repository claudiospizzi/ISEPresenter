using ISEPresenter.Properties;
using System;
using System.Windows.Forms;


namespace ISEPresenter.Models
{
    /// <summary>
    /// Logitech Presenter R400 remote control device.
    /// </summary>
    public class LogitechPresenterR400 : RemoteControlDeviceBase
    {
        #region Constructors

        /// <summary>
        /// Initialize the Logitech Presenter R400.
        /// </summary>
        public LogitechPresenterR400()
        {
            DeviceName = Resources.RemoteControlDevice_LogitechPresenterR400_DeviceName;

            RunDescription     = Resources.RemoteControlDevice_LogitechPresenterR400_RunDescription;
            ClearDescription   = Resources.RemoteControlDevice_LogitechPresenterR400_ClearDescription;
            BackDescription    = Resources.RemoteControlDevice_LogitechPresenterR400_BackDescription;
            ForwardDescription = Resources.RemoteControlDevice_LogitechPresenterR400_ForwardDescription;
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Implement the custom key down handler method to match the Logitech
        /// Presenter R400 device.
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

                case Keys.Escape:
                    OnRun();
                    break;

                case Keys.OemPeriod:
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
