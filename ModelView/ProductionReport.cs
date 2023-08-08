using CommunityToolkit.Mvvm.ComponentModel;

namespace Packup.ModelView;

public partial class ProductionReport : ObservableObject
{
    [ObservableProperty]
    private int produced;

    [ObservableProperty]
    private int pending;

    [ObservableProperty]
    private int total;

}
