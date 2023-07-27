using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Packup.Model;

namespace Packup.ModelView;

public partial class BeginParametersModelView : BaseModelView
{
    private static IEnumerable<int> getNumeration(int max, bool includeZero = true) {
        for(int n = includeZero ? 0 : 1; n <= max; n++)
            yield return n;
    }

    private static int[] GetNumeration(int max, bool includeZero = true) =>
        getNumeration(max, includeZero).ToArray();

    //Constructor
    public BeginParametersModelView() {
        PalletValues = GetNumeration(LoteParameters.PalletsByLote, false);
        LevelsValues = GetNumeration(LoteParameters.LevelsByPallet);
        BoxesValues = GetNumeration(LoteParameters.BoxesByLevel);
    }

    public int[] PalletValues { get; }
    public int[] LevelsValues { get; }
    public int[] BoxesValues { get; }

    //Párametros de inicio

    [ObservableProperty]
    private LoteParameters loteParameters = LoteParameters.Default;

    [ObservableProperty]
    private PalletBody firstPallet;

    [ObservableProperty]
    private PalletBody lastPallet;

    [RelayCommand]
    private void SetFirstPallet(IPalletBody data) =>
        FirstPallet = new PalletBody(data);
}
