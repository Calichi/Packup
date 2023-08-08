namespace Packup.Model;

public struct LoteMetricsNumeration
{
    private static IEnumerable<int> GetEnumerable(int max, bool includeZero = true)
    {
        for (int n = includeZero ? 0 : 1; n <= max; n++)
            yield return n;
    }

    public static int[] GetArray(int max, bool includeZero = true) =>
        GetEnumerable(max, includeZero).ToArray();


    public LoteMetricsNumeration(LoteParameters parameters) {
        this.PalletsByLote = GetArray(parameters.PalletsByLote, false);
        this.LevelsByPallet = GetArray(parameters.LevelsByPallet);
        this.BoxesByLevel = GetArray(parameters.BoxesByLevel);
    }


    public int[] PalletsByLote { get; }
    public int[] LevelsByPallet { get; }
    public int[] BoxesByLevel {  get; }
}
