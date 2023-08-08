namespace Packup.Page;

public partial class OperationsPage : ContentPage
{
	public OperationsPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((ModelView.Core)BindingContext)?.LoadOperations();
    }
}