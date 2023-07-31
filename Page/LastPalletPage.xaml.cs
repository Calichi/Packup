using Packup.Model;

namespace Packup.Page;

public partial class LastPalletPage : ContentPage, IPalletBody
{
	public LastPalletPage()
	{
		InitializeComponent();
	}

    private int GetValue(Picker picker) =>
    Convert.ToInt32(picker.SelectedItem);

    public int Number => GetValue(palletPicker);

    public int Levels => 0;

    public int Boxes => 0;
}