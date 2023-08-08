using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Packup.ModelView;

public partial class Core : Base
{
    public Core() {
        Model.LoteParameters parameters = Model.LoteParameters.Default;
        LoteMetricsNumeration = new Model.LoteMetricsNumeration(parameters);
        BeginParameters = new Model.BeginParameters();
        OperationStateReport = new OperationStateReport();
    }

    public OperationStateReport OperationStateReport { get; }

    [ObservableProperty]
    private ObservableCollection<Model.Lote> operations;

    [ObservableProperty]
    private ObservableCollection<Model.Tags> tags;

    [ObservableProperty]
    private ObservableCollection<Model.State> states;

    [ObservableProperty]
    private Model.Lote selectedLote;

    [ObservableProperty]
    private Model.State selectedState;

    [ObservableProperty]
    private Model.LoteMetricsNumeration loteMetricsNumeration;

    [ObservableProperty]
    private Model.BeginParameters beginParameters;

    [ObservableProperty]
    private Model.BeginReport beginReport;


    [RelayCommand]
    private async Task SetFirstPallet(Model.IPalletBody data) {
        BeginParameters.FirstPallet = new Model.PalletBody(data);
        await Shell.Current.GoToAsync("lastPallet");
    }

    [RelayCommand]
    private async Task SetLastPallet(Model.IPalletBody data) {
        BeginParameters.LastPallet = new Model.PalletBody(data, true);
        this.BeginReport = new Model.BeginReport(BeginParameters, Converter);
        await Shell.Current.GoToAsync("beginReport");
    }

    [RelayCommand]
    private async Task SaveLote() {
        //Construimos el [Entity.Lote]
        Model.PalletBody firstPallet = BeginParameters.FirstPallet;
        Model.Entity.Lote loteEntity = new Model.Entity.Lote(Model.LoteParameters.Default,
                                                       Converter.GetLoteBoxes(firstPallet));

        //Almacenamos en la base de datos
        await Repository.SaveAsync(loteEntity);
        await SaveTagsAsync(loteEntity);

        //Almacenamos en la cache
        Model.Lote lote = new Model.Lote(loteEntity, Converter);
        Operations?.Add(lote);
        SelectedLote = lote;
        
        var p = Shell.Current.CurrentPage; //Capturamos la página actual
        await Shell.Current.GoToAsync("//operation/state"); //Navegamos a lo que sigue
        await p.Navigation.PopToRootAsync();//Reiniciamos la página de inicio
    }

    private int tagsIndex = 0;
    private int previousTagNumber = 0;
    private Model.Tags CurrentTags => Tags[tagsIndex];

    [RelayCommand]
    private async Task SaveState(string tagNumberString) {
        int tagNumber = Convert.ToInt32(tagNumberString);
        if (tagNumber > previousTagNumber) tagsIndex++;
        Model.Entity.State stateEntity = new Model.Entity.State() {
            LoteId = SelectedLote.Id,
            PalletNumber = CurrentTags.PalletNumber,
            PreviousTagNumber = previousTagNumber,
            TagNumber = tagNumber
        };

        await Repository.SaveAsync(stateEntity);
        Model.State state = new Model.State(stateEntity, Converter);
        States.Add(state);
        SelectedState = state;
        previousTagNumber = tagNumber;
    }

    private void UpdateOperationStateReport() {
        ProductionReport lotePR = this.OperationStateReport.LoteProduction;
        ProductionReport palletPR = this.OperationStateReport.PalletProduction;

        Model.Tags tagsPack = Tags.First();
        int currentTagNumber = tagsPack.Major + 1;
        int tagsPackLength = tagsPack.Length;
        int lotePRProduced = this.States.Sum(state => state.ProducedBoxes); ;

        if (SelectedState is not null) {
            tagsPack = GetTags(SelectedState.PalletNumber);
            currentTagNumber = SelectedState.TagNumber;
            tagsPackLength = tagsPack.Length;
            lotePRProduced = this.States.Take(this.States.IndexOf(SelectedState)+1).Sum(state => state.ProducedBoxes);
        }

        lotePR.Total = this.Tags.Sum(pack => pack.Length);
        lotePR.Produced = lotePRProduced;
        lotePR.Pending = lotePR.Total - lotePR.Produced;

        palletPR.Total = tagsPackLength;
        palletPR.Pending = Converter.GetPalletPendingBoxes(currentTagNumber);
        palletPR.Produced = palletPR.Total - palletPR.Pending;
    }

    partial void OnSelectedStateChanged(Model.State value) {
        UpdateOperationStateReport();
    }

    private Model.Tags GetTags(int palletNumber) {
        return (from pack in Tags
                where pack.PalletNumber == palletNumber
                select pack).First();
    }

    private async Task SaveTagsAsync(Model.Entity.Lote lote) {
        foreach (Model.Entity.Tags tags in GenerateTags(lote))
            await Repository.SaveAsync(tags);
    }

    private IEnumerable<Model.Entity.Tags> GenerateTags(Model.Entity.Lote lote) {
        Model.PalletBody firstPallet = BeginParameters.FirstPallet;
        Model.PalletBody lastPallet = BeginParameters.LastPallet.Relative();
        yield return Converter.CreateTagsEntity(lote, firstPallet);
        for (int i = firstPallet.Number + 1; i < lastPallet.Number; i++)
            yield return new Model.Entity.Tags(lote.Id, i);
    }

    public void LoadOperations()
    {
        if (Operations is not null) return;

        this.Operations = new ObservableCollection<Model.Lote>();
        Repository.GetOperations().ContinueWith(receivedOperations);
    }

    private void receivedOperations(Task<List<Model.Entity.Lote>> task)
    {
        foreach (var lote in task.Result) {
            Operations.Add(new Model.Lote(lote, Converter));
        }
        SelectedLote = Operations.Last();
    }

    private async Task<List<T>> LoadDatas<T>() where T : Model.Entity.LoteMember, new() =>
        await Repository.GetMembers<T>(SelectedLote);

    private long loadedloteId = -1;

    public async Task LoadMembersAsync() {
        if (SelectedLote is null) return;
        if (SelectedLote.Id == loadedloteId) return;

        this.SelectedState = null;
        await LoadTags();
        await LoadStates();

        loadedloteId = SelectedLote.Id;
    }

    public void LoadMembers() =>
        LoadMembersAsync().ContinueWith(OnLoadedMembers);

    public async Task LoadTags() { 
        List<Model.Entity.Tags> list = await LoadDatas<Model.Entity.Tags>();
        Tags = new ObservableCollection<Model.Tags>();

        foreach (var tagsPack in list)
            this.Tags.Add(new Model.Tags(tagsPack));
    }

    public async Task LoadStates()
    {
        List<Model.Entity.State> list = await LoadDatas<Model.Entity.State>();
        this.States = new ObservableCollection<Model.State>();

        foreach (var state in list)
            this.States.Add(new Model.State(state, Converter));
    }

    private void OnLoadedMembers(Task task)
    {
        tagsIndex = 0;
        previousTagNumber = CurrentTags.Major + 1;

        if (States.Count > 0) {
            this.SelectedState = States.Last();
            var tags = GetTags(SelectedState.PalletNumber);
            tagsIndex = Tags.IndexOf(tags);
            previousTagNumber = SelectedState.TagNumber;
        }

        UpdateOperationStateReport();
    }
}
