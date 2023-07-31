using CommunityToolkit.Mvvm.ComponentModel;

namespace Packup.View;


public partial class FourColumnsView : Grid
{
	public static readonly BindableProperty TextStyleProperty;
	public static readonly BindableProperty FirstTextProperty;
	public static readonly BindableProperty SecondTextProperty;
	public static readonly BindableProperty ThirdTextProperty;
	public static readonly BindableProperty FourthTextProperty;

	static FourColumnsView()
	{
		TextStyleProperty = BindableProperty.Create("TextStyle",
			typeof(Style),
			typeof(FourColumnsView),
			null);
		FirstTextProperty = BindableProperty.Create("FirstText",
			typeof(string),
			typeof(FourColumnsView),
			"");
        SecondTextProperty = BindableProperty.Create("SecondText",
            typeof(string),
            typeof(FourColumnsView),
            "");
        ThirdTextProperty = BindableProperty.Create("ThirdText",
            typeof(string),
            typeof(FourColumnsView),
            "");
        FourthTextProperty = BindableProperty.Create("FourthText",
            typeof(string),
            typeof(FourColumnsView),
            "");
    }

	public FourColumnsView()
	{
		InitializeComponent();
	}

	public Style TextStyle
	{
		get => (Style)GetValue(TextStyleProperty);
		set => SetValue(TextStyleProperty, value);
	}

	public string FirstText
	{
		get => (string)GetValue(FirstTextProperty);
		set => SetValue(FirstTextProperty, value);
	}

	public string SecondText
	{
		get => (string)GetValue(SecondTextProperty);
		set => SetValue(SecondTextProperty, value);
	}

	public string ThirdText
	{
		get => (string)GetValue(ThirdTextProperty);
		set => SetValue(ThirdTextProperty, value);
	}

	public string FourthText
	{
		get => (string)GetValue(FourthTextProperty);
		set => SetValue(FourthTextProperty, value);
	}
}