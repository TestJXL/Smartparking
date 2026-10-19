using Parking.Application;
using Parking.Application.Persistence;
using Parking.Controllers;
using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.Core.Interfaces;
using Parking.Core.Views;

// A working example of a GUI-less application: using a real Controller + EF + SQLite.
var path = args.Length > 0 ? args[0] : Path.Combine(AppContext.BaseDirectory, "smartpark-demo.db");
var store = new EfParkingStore(path);
store.Initialize();
var clock = new DemoClock();
var service = new ParkingApplicationService(store, clock);
var ui = new ParkingControllers(service, new ConsoleShell());
Console.WriteLine($"SQLite: {Path.GetFullPath(path)}");
Console.WriteLine($"Available / Total:  {Ok(ui.Dashboard.Load())["AvailableCount"]}/{Ok(ui.Dashboard.Load())["TotalCount"]}");
var plate = "D" + Guid.NewGuid().ToString("N")[..7].ToUpperInvariant();
ui.Dashboard.OpenArrival();
Ok(ui.Arrival.SubmitPlate(plate));
Ok(ui.Arrival.SubmitDetails(new ParkingRequest { Size = VehicleSize.Small, IsEV = false }));
var grid = Ok(ui.SpaceSelection.Load());
if (grid.SelectedSpaceId == null) { Console.WriteLine("There are no available parking spaces."); return; }
var entry = Ok(ui.SpaceSelection.Next());
Console.WriteLine($"Check-in Complete, Ticket Number {entry.Ticket!.SessionId}");
clock.UtcNow = clock.UtcNow.AddMinutes(61);
Ok(ui.Arrival.SubmitPlate(plate));
var quote = Ok(ui.Checkout.Load());
Console.WriteLine($"Parking {quote.Duration.TotalMinutes} minutes, amount ${quote.AmountCents / 100m:F2}");
var receipt = Ok(ui.Checkout.Confirm());
Console.WriteLine($"Payment Successful, Check-out Time {receipt.ExitUtc:O}");
ui.Checkout.Done();
Ok(ui.Arrival.SubmitPlate("@admin"));
Console.WriteLine($"Number of Parking History Entries: {Ok(ui.History.Load()).Count}");
ui.Management.Logout();

static T Ok<T>(OperationResult<T> r) => r.Success ? r.Value! : throw new InvalidOperationException(r.Message);
sealed class ConsoleShell : IShellView { public void ShowScreen(ScreenId screen) => Console.WriteLine($"Page → {screen}"); }
sealed class DemoClock : IClock { public DateTime UtcNow { get; set; } = DateTime.UtcNow; }
