namespace Packup.ModelView;

public class OperationsSharedModelView
{
    private static readonly OperationsModelView sharedModelView;

    static OperationsSharedModelView() {
        sharedModelView = new OperationsModelView();
    }

    public OperationsModelView SharedModelView => sharedModelView;
}
