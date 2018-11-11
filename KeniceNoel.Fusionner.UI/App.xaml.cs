using System.Windows;

namespace KeniceNoel.Fusionner.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string license;
        public App()
        {
            license =
                "NDA0MDJAMzEzNjJlMzMyZTMwQWgzcGVWSzIwVXlkL01sUUZDM2g2a0RJTXdzV0VWMEhmOCt1T2Qzb0kyUT0=;NDA0MDNAMzEzNjJlMzMyZTMwZnBoNlBtcDR2SkxMOVNCa2ZQeDk0RnphQmhxKy9tNlNhTndkK09VdG45MD0=;NDA0MDRAMzEzNjJlMzMyZTMwUCtONGN6V0E2UkFWS3EyU05iME5OTWpGMWFjd2kwY2tNWm9MYjRPNzVmdz0=;NDA0MDVAMzEzNjJlMzMyZTMwbjhNR3hmZFowNGVxYUJYcGIvZ2VXRG9seDNHOTRSZE1XSUtwWFNtQWw3Zz0=;NDA0MDZAMzEzNjJlMzMyZTMwVnQ4MVFHZ3hBSHVYRWJUNFNjdEI0UG4zbXBFNmJjNTNJSlhGaW56aFRIbz0=;NDA0MDdAMzEzNjJlMzMyZTMwZGd4T0QxT0FsUUVCR3ZNZSt3T0FmeFZNcmk5TlVCL1cxb3dxU3JzSWJNdz0=;NDA0MDhAMzEzNjJlMzMyZTMwS3J0MXdidnJoam11Q3QyYmtNUVRjYzlvUlFPcnJnckhHTDZKa3pQaGExMD0=;NDA0MDlAMzEzNjJlMzMyZTMwU0hKaVNtOW90SmFsWkM3R0NvMTljTnR0Y2pmaDEwaVJrZlNVS29Kdk5HRT0=;NDA0MTBAMzEzNjJlMzMyZTMwbW92ZEJNTmdvbmtRV24xd1NlL1MyaWJBeWJwd0FGcGxxWUY0dEJsU0hYOD0=";
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(license);

        }

    }
}
