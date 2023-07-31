namespace Packup.View;

public partial class TagsView : ViewCell
{
    public static readonly BindableProperty TextStyleProperty;

	static TagsView()
	{
        TextStyleProperty = BindableProperty.Create("TextStyle",
                            typeof(Style),
                            typeof(FourColumnsView),
                            null);
    }

    public TagsView()
	{
		InitializeComponent();
	}

    public Style TextStyle
    {
        get => (Style)GetValue(TextStyleProperty);
        set => SetValue(TextStyleProperty, value);
    }
}