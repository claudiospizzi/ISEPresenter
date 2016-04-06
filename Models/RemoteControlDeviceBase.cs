using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ISEPresenter.Models
{
    public abstract class RemoteControlDeviceBase : IRemoteControlDevice
    {
        public string DeviceName
        {
            get;
            protected set;
        }


        public string RunDescription
        {
            get;
            protected set;
        }


        public string ClearDescription
        {
            get;
            protected set;
        }


        public string BackDescription
        {
            get;
            protected set;
        }


        public string ForwardDescription
        {
            get;
            protected set;
        }


        public static IEnumerable<IRemoteControlDevice> GetDeviceList()
        {
            return typeof(RemoteControlDeviceBase).Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(RemoteControlDeviceBase)))
                .Select(t => (IRemoteControlDevice)Activator.CreateInstance(t));
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
