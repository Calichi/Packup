namespace Packup.View;

public partial class FlagView : ContentView
{
	public static readonly BindableProperty TextProperty;

	static FlagView()
	{
		TextProperty = BindableProperty.Create("Text",
			typeof(string),
			typeof(FlagView),
			"");
	}

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

	public FlagView()
	{
		InitializeComponent();
	}

    private void ComputeSize(object sender, EventArgs e)
    {
		double lblH = this.label.Height;
		double lblW = this.label.Width;
		double min = lblH;

		if(lblW < lblH) {
			lblW = lblH;
		}

		this.shape.WidthRequest = lblW;
		this.shape.HeightRequest = lblH;
		this.shape.CornerRadius = new CornerRadius(min/2);
    }
}