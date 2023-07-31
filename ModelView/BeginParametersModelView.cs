using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Packup.Model;
using Packup.Model.Database;

namespace Packup.ModelView;

public partial class BeginParametersModelView : BaseModelView
{
    private static IEnumerable<int> getNumeration(int max, bool includeZero = true) {
        for (int n = includeZero ? 0 : 1; n <= max; n++)
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

    [ObservableProperty]
    private int lotePendingBoxes;

    [ObservableProperty]
    public int palletProducedBoxes;

    [ObservableProperty]
    public int palletPendingBoxes;

    [ObservableProperty]
    public int nextTag;

    //Párametros de inicio

    [ObservableProperty]
    private LoteParameters loteParameters = LoteParameters.Default;

    [ObservableProperty]
    private PalletBody firstPallet;

    [ObservableProperty]
    private PalletBody lastPallet;

    [RelayCommand]
    private async Task SetFirstPallet(IPalletBody data) {
        FirstPallet = new PalletBody(data);
        await Shell.Current.GoToAsync("lastPallet");
    }

    [RelayCommand]
    private async Task SetLastPallet(IPalletBody data) {
        LastPallet = new PalletBody(data, true);
        SetBeginReport();
        await Shell.Current.GoToAsync("beginReport");
    }

    [RelayCommand]
    private async Task SaveLote() {
        LoteEntity lote = new LoteEntity(LoteParameters, Convert.GetLoteBoxes(FirstPallet));
        await Repository.SaveAsync(lote);
        await SaveTagsAsync(lote);
        await Shell.Current.GoToAsync("//operations");
    }

    private async Task SaveTagsAsync(LoteEntity lote) {
        foreach (TagsEntity tags in GenerateTags(lote))
            await Repository.SaveAsync(tags);
    }

    private IEnumerable<TagsEntity> GenerateTags(LoteEntity lote) {
        PalletBody lastPallet = LastPallet.Relative();
        yield return Convert.CreateTagsEntity(lote, FirstPallet);
        for (int i = FirstPallet.Number + 1; i < lastPallet.Number; i++)
            yield return new TagsEntity(lote.Id, i);
    }

    private void SetBeginReport() {
      LotePendingBoxes = Convert.GetLoteBoxes(LastPallet) - Convert.GetLoteBoxes(FirstPallet);
      PalletProducedBoxes = Convert.GetPalletBoxes(FirstPallet);
      PalletPendingBoxes = LoteParameters.BoxesByPallet - PalletProducedBoxes;
      NextTag = Convert.GetTagNumber(FirstPallet);
    }
}
