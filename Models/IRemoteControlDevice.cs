using System;


namespace ISEPresenter.Models
{
    public interface IRemoteControlDevice : IDisposable
    {
        string DeviceName { get; }


        string RunDescription { get; }


        string ClearDescription { get; }


        string BackDescription { get; }


        string ForwardDescription { get; }
    }
}
