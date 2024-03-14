using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity{

    readonly Vec2F startPos;

    public Enemy(DynamicShape shape, IBaseImage image) : base(shape, image) {
        startPos = shape.Position;
    }

    public Vec2F getStartPos() {
        return startPos;
    }

}
