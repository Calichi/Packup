namespace Packup.Page;

public partial class SchedulingPage : ContentPage
{
	public SchedulingPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing(){
        base.OnAppearing();
		((ModelView.Core)BindingContext)?.LoadMembers();
    }
}