using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace Kopigi.NetCore.UWP.UI
{
    public static class UIHelper
    {
        /// <summary>
        /// Permet de savoir sur quel type de device l'application tourne
        /// </summary>
        /// <returns></returns>
        public static TypeDeviceRun GetTypeDeviceRun()
        {
            switch (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                case "Windows.Mobile":
                    return TypeDeviceRun.Mobile;
                case "Windows.Desktop":
                    return UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Mouse ? TypeDeviceRun.Desktop : TypeDeviceRun.Tablet;
                case "Windows.Universal":
                    return TypeDeviceRun.IoT;
                case "Windows.Team":
                    return TypeDeviceRun.SurfaceHub;
                case "Windows.Xbox":
                    return TypeDeviceRun.Xbox;
                default:
                    return TypeDeviceRun.Other;
            }
        }
    }

    public enum TypeDeviceRun
    {
        Desktop,
        Tablet,
        Mobile,
        IoT,
        SurfaceHub,
        Xbox,
        Other
    }
}
