namespace Packup.ModelView;

public class CoreShared
{
    private static readonly Core sharedModelView;

    static CoreShared() {
        sharedModelView = new Core();
    }

    public Core SharedModelView => sharedModelView;
}
