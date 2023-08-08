using CommunityToolkit.Mvvm.ComponentModel;

namespace Packup.ModelView;

public partial class OperationStateReport : ObservableObject
{
    public ProductionReport LoteProduction { get; } = new ProductionReport();
    public ProductionReport PalletProduction { get; } = new ProductionReport();
}
