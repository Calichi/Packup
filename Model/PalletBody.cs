namespace Packup.Model;

public struct PalletBody : IPalletBody
{
    private bool isAbsolute;

    public PalletBody(int number, int levels, int boxes, bool isAbsolute = false) {
        Number = number;
        Levels = levels;
        Boxes = boxes;
        this.isAbsolute = isAbsolute;
    }

    public PalletBody(IPalletBody data, bool isAbsolute = false) :
                 this(data.Number, data.Levels, data.Boxes, isAbsolute) { }

    public int Number { get; }

    public int Levels { get; }

    public int Boxes { get; }

    public PalletBody Clone(int number, bool isAbsolute = false) =>
        new PalletBody(number, Levels, Boxes, isAbsolute);

    public PalletBody Absolute() =>
        isAbsolute ? this : Clone(Number - 1, true);

    public PalletBody Relative() =>
        isAbsolute ? Clone(Number + 1) : this;

    public override string ToString() =>
        $"[T: {Number}, L: {Levels}, B: {Boxes}]";
}
