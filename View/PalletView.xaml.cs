using Packup.Model;

namespace Packup.View;

public partial class PalletView : ContentView
{
	public static readonly BindableProperty PalletProperty;

	static PalletView()
	{
		PalletProperty = BindableProperty.Create("Pallet",
			typeof(PalletBody),
			typeof(PalletView),
			new PalletBody(0, 0, 0));
	}

	public PalletBody Pallet
	{
		get => (PalletBody)GetValue(PalletProperty);
		set => SetValue(PalletProperty, value);
	}

	public PalletView()
	{
		InitializeComponent();
	}
}