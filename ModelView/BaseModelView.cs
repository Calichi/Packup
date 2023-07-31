using Packup.Service;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Packup.ModelView;

public class BaseModelView : ObservableObject
{
    public RepositoryService Repository => RepositoryService.Instance;
    public ConvertService Convert => ConvertService.Instance;
}
