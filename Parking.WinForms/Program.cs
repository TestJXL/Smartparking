using Parking.Application;
using Parking.Application.Persistence;
using Parking.Controllers;

namespace Parking.WinForms
{
    internal static class Program
    {
        ///  The main entry point for the application.
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Display a uniform pop-up message for unexpected exceptions to prevent the program from crashing immediately
            System.Windows.Forms.Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            System.Windows.Forms.Application.ThreadException += (sender, e) =>
                MessageBox.Show(e.Exception.Message, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // The assembly method is identical to Parking.Demo: SQLite file → EF storage → application service → controller
            ParkingApplicationService service;
            try
            {
                string databasePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SmartPark", "smartpark.db");
                var store = new EfParkingStore(databasePath);
                store.Initialize();
                service = new ParkingApplicationService(store);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open the parking database:\n" + ex.Message,
                    "SmartPark", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            System.Windows.Forms.Application.Run(new MainForm(shell => new ParkingControllers(service, shell)));
        }
    }
}
