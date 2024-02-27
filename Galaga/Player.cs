using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga;
public class Player {    
    private Entity entity;
    private DynamicShape shape;

    private float moveRight = 0.0f;
    private float moveLeft = 0.0f;
    private float MOVEMENT_SPEED = 0.01f;

    public Player(DynamicShape shape, IBaseImage image) {
    entity = new Entity(shape, image);
    this.shape = shape;
    }
    
    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;
    }

    public void Render() {
        entity.RenderEntity();
    }

    public void Move() {
        if (0.0f > shape.Position.X) {
            shape.Position.X = 0.0f;
        } else if (shape.Position.X > 1.0f - shape.Extent.X) {
            shape.Position.X = 1.0f - shape.Extent.X;
        }
        shape.Move();
        // TODO: move the shape and guard against the window borders
    }

    public void SetMoveLeft(bool val) {
        if (val) {
            moveLeft -= MOVEMENT_SPEED;
        } else {
            moveLeft = 0.0f;
        } UpdateDirection();
        // TODO:set moveLeft appropriately and call UpdateDirection()
    }

    public void SetMoveRight(bool val) {
        if (val) {
            moveRight += MOVEMENT_SPEED;
        } else {
            moveRight = 0.0f;
        } UpdateDirection();
        // TODO:set moveRight appropriately and call UpdateDirection()
    }

}
