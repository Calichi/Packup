using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Packup.ModelView;

public partial class OperationsModelView : BaseModelView
{
    [ObservableProperty]
    private List<Model.Database.LoteEntity> operations;

    [ObservableProperty]
    private ObservableCollection<Model.Output.Tags> tags;

    [ObservableProperty]
    private Model.Database.LoteEntity selectedLote;

    [ObservableProperty]
    private bool tagsLoadEnabled;

    public OperationsModelView() {
        Repository.GetOperations().ContinueWith(receivedOperations);
    }

    private void receivedOperations(Task<List<Model.Database.LoteEntity>> task) {
        this.Operations = task.Result;
    }

    partial void OnTagsLoadEnabledChanging(bool value) {
        Repository.GetTags(SelectedLote).ContinueWith(receivedTags);
    }

    private void receivedTags(Task<List<Model.Database.TagsEntity>> task) {
        var result = new ObservableCollection<Model.Output.Tags>();
        foreach(var tag in task.Result) {
            result.Add(new Model.Output.Tags(tag));
        }
    }
}
