using System.Diagnostics;

namespace Packup.Page;

public partial class StatePage : ContentPage
{
	public StatePage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((ModelView.Core)BindingContext).LoadMembers();
    }
}