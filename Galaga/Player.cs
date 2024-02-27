using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga;
public class Player {    
    private Entity entity;
    private DynamicShape shape;

// Ask about if these should be fields and then intialized in constructor, or if this is ok
    private float moveRight;
    private float moveLeft;
    private float MOVEMENT_SPEED;

    public Player(DynamicShape shape, IBaseImage image) {
    entity = new Entity(shape, image);
    this.shape = shape;
    this.moveRight = 0.0f;
    this.moveLeft = 0.0f;
    this.MOVEMENT_SPEED = 0.01f;
    }
    
    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;
    }

    public void Render() {
        entity.RenderEntity();
    }

    public void Move() {
        if (0.0f < shape.Position.X && shape.Position.X < 500.0f) {
            shape.Move();
        }
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
