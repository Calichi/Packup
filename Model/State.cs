namespace Packup.Model;

public class State : Entity.State, IEquatable<State>
{
    public int ProducedBoxes { get; }
    public PalletBody VisualStructure { get; }

    public State(Entity.State data, Service.Converter converter) {
        Id = data.Id;
        Timestamp = data.Timestamp;
        LoteId = data.LoteId;
        PalletNumber = data.PalletNumber;
        PreviousTagNumber = data.PreviousTagNumber;
        TagNumber = data.TagNumber;
        VisualStructure = converter.CreatePalletBody(PalletNumber, TagNumber);
        ProducedBoxes = converter.GetProducedBoxes(PreviousTagNumber, TagNumber);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as State);
    }

    public bool Equals(State other)
    {
        return other is not null &&
               Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public static bool operator ==(State left, State right)
    {
        return EqualityComparer<State>.Default.Equals(left, right);
    }

    public static bool operator !=(State left, State right)
    {
        return !(left == right);
    }
}
