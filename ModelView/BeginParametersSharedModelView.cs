namespace Packup.ModelView;

public class BeginParametersSharedModelView
{
    private static readonly BeginParametersModelView sharedModelView;

    static BeginParametersSharedModelView() {
        sharedModelView = new BeginParametersModelView();
    }

    public BeginParametersModelView SharedModelView => sharedModelView;
}
