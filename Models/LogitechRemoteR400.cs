using ISEPresenter.Properties;


namespace ISEPresenter.Models
{
    public class LogitechRemoteR400 : RemoteControlDeviceBase
    {
        public LogitechRemoteR400()
        {
            DeviceName = Resources.RemoteControlDevice_LogitechRemoteR400_DeviceName;

            RunDescription     = Resources.RemoteControlDevice_LogitechRemoteR400_RunDescription;
            ClearDescription   = Resources.RemoteControlDevice_LogitechRemoteR400_ClearDescription;
            BackDescription    = Resources.RemoteControlDevice_LogitechRemoteR400_BackDescription;
            ForwardDescription = Resources.RemoteControlDevice_LogitechRemoteR400_ForwardDescription;
        }
    }
}
