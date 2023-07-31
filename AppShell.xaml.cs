using Packup.Page;

namespace Packup;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("lastPallet", typeof(LastPalletPage));
        Routing.RegisterRoute("beginReport", typeof(BeginReportPage));
    }
}
