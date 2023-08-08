using Packup.Service;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Packup.ModelView;

public class Base : ObservableObject
{
    public RepositoryService Repository => RepositoryService.Instance;
    public Service.Converter Converter => Service.Converter.Instance;
}
