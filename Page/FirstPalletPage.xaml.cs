using Packup.Model;

namespace Packup.Page;

public partial class FirstPalletPage : ContentPage, IPalletBody
{
	public FirstPalletPage()
	{
		InitializeComponent();
	}

	private int GetValue(Picker picker) =>
		Convert.ToInt32(picker.SelectedItem);

    public int Number => GetValue(palletPicker);

    public int Levels => GetValue(levelsPicker);

    public int Boxes => GetValue(boxesPicker);
}