using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMDCore.Graphics;

public readonly struct Tile(int graphicId = -1, int topperId = -1, bool isSolid = false)
{
    public int GraphicId { get; init; } = graphicId;
    public int TopperId { get; init; } = topperId;
    public bool IsSolid { get; init; } = isSolid;

    public bool HasTopper => TopperId >= 0;

    public Tile(int graphicId, bool isSolid) : this(graphicId, -1, isSolid) { }
}