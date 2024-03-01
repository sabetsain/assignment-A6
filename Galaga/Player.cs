using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;


namespace Galaga;
public class Player {    
    private Entity entity;
    private DynamicShape shape;

    private float moveRight = 0.0f;
    private float moveLeft = 0.0f;
    private float moveUp = 0.0f;
    private float moveDown = 0.0f;
    private float MOVEMENT_SPEED = 0.01f;

    public Player(DynamicShape shape, IBaseImage image) {
    entity = new Entity(shape, image);
    this.shape = shape;
    }
    
    public DynamicShape Shape {
        get { return shape; }
        set { shape = value; }
    }

    public float MoveRight {
        get { return moveRight; }
    }

    public float MoveLeft {
        get { return moveLeft; }
    }

    public float MoveUp {
        get { return moveUp; }
    }

    public float MoveDown {
        get { return moveDown; }
    }
    
    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;
        shape.Direction.Y = moveDown + moveUp;
    }

    public void Render() {
        entity.RenderEntity();
    }

    public void Move() {
        if (0.0f > shape.Position.X) { 
            shape.Position.X = 0.0f;
        } else if (shape.Position.X > 1.0f - shape.Extent.X) {
            shape.Position.X = 1.0f - shape.Extent.X;
        } else if (shape.Position.Y < 0.0f) {
            shape.Position.Y = 0.0f;
        } else if (shape.Position.Y > 1.0f - shape.Extent.Y) {
            shape.Position.Y = 1.0f - shape.Extent.Y;
        } 
        shape.Move();
    }

    public void SetMoveLeft(bool val) {
        if (val) {
            moveLeft -= MOVEMENT_SPEED;
        } else {
            moveLeft = 0.0f;
        } UpdateDirection();
    }

    public void SetMoveRight(bool val) {
        if (val) {
            moveRight += MOVEMENT_SPEED;
        } else {
            moveRight = 0.0f;
        } UpdateDirection();
    }

    public void SetMoveUp (bool val) {
        if (val) {
            moveUp += MOVEMENT_SPEED;
        } else {
            moveUp = 0.0f;
        } UpdateDirection();
    }

    public void SetMoveDown (bool val) {
        if (val) {
            moveDown -= MOVEMENT_SPEED;
        } else {
            moveDown = 0.0f;
        } UpdateDirection();
    }

    public Vec2F GetPosition() {
        return shape.Position;
    }
}
