using Parking.Core.Enums;
namespace Parking.Core.Views;

// The controller returns a DTO; the view binds to the table or grid on its own and displays OperationResult.Message.
// Navigation is abstracted into an interface only: this can be used in WinForms, WPF, or the FakeShell in testing.
public interface IShellView
{
    void ShowScreen(ScreenId screen);
}
