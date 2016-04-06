using ISEPresenter.Properties;


namespace ISEPresenter.Models
{
    public class GenericKeyboard : RemoteControlDeviceBase
    {
        public GenericKeyboard()
        {
            DeviceName = Resources.RemoteControlDevice_GenericKeyboard_DeviceName;

            RunDescription     = Resources.RemoteControlDevice_GenericKeyboard_RunDescription;
            ClearDescription   = Resources.RemoteControlDevice_GenericKeyboard_ClearDescription;
            BackDescription    = Resources.RemoteControlDevice_GenericKeyboard_BackDescription;
            ForwardDescription = Resources.RemoteControlDevice_GenericKeyboard_ForwardDescription;
        }
    }
}
