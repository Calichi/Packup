namespace Packup.Model;

public interface ILoteParameters
{
    public int PalletsByLote { get; }
    public int LevelsByPallet { get; }
    public int BoxesByLevel {  get; }
}
