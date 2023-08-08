namespace Packup.Model;

public class Lote : Entity.Lote
{
    public PalletBody BeginPoint { get; }

    public Lote(Entity.Lote data, Service.Converter converter)
    {
        Id = data.Id;
        Timestamp = data.Timestamp;
        PalletsByLote = data.PalletsByLote;
        LevelsByPallet = data.LevelsByPallet;
        BoxesByLevel = data.BoxesByLevel;
        ReceivedBoxes = data.ReceivedBoxes;
        BeginPoint = converter.CreatePalletBody(ReceivedBoxes).Relative();
    }
}
