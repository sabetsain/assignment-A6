using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;


namespace Galaga;
public class Player : IGameEventProcessor {    
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

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.PlayerEvent) {
            switch (gameEvent.Message) {
                case "MOVE_PLAYER_RIGHT":
                    if (gameEvent.StringArg1 == "KEY_PRESS") {
                        SetMoveRight(true);
                    } else if (gameEvent.StringArg1 == "KEY_RELEASE") {
                        SetMoveRight(false);
                    } break;
                case "MOVE_PLAYER_LEFT":
                    if (gameEvent.StringArg1 == "KEY_PRESS") {
                        SetMoveLeft(true);
                    } else if (gameEvent.StringArg1 == "KEY_RELEASE") {
                        SetMoveLeft(false);
                    } break;
                case "MOVE_PLAYER_UP":
                    if (gameEvent.StringArg1 == "KEY_PRESS") {
                        SetMoveUp(true);
                    } else if (gameEvent.StringArg1 == "KEY_RELEASE") {
                        SetMoveUp(false);
                    } break;
                case "MOVE_PLAYER_DOWN":
                    if (gameEvent.StringArg1 == "KEY_PRESS") {
                        SetMoveDown(true);
                    } else if (gameEvent.StringArg1 == "KEY_RELEASE") {
                        SetMoveDown(false);
                    } break;
            }
        }
    }

    public void Move() {
        shape.Move();
        if (0.0f > shape.Position.X && shape.Position.Y < 0.0f) {
            shape.Position.Y = 0.0f;
            shape.Position.X = 0.0f;
        } else if (0.0f > shape.Position.X && shape.Position.Y > 1.0f - shape.Extent.Y) {
            shape.Position.X = 0.0f;
            shape.Position.Y = 1.0f - shape.Extent.Y;
        } else if (shape.Position.X > 1.0f - shape.Extent.X && shape.Position.Y < 0.0f) {
            shape.Position.X = 1.0f - shape.Extent.X;
            shape.Position.Y = 0.0f;
        } else if (shape.Position.X > 1.0f - shape.Extent.X && shape.Position.Y > 1.0f - shape.Extent.Y) {
            shape.Position.Y = 1.0f - shape.Extent.Y;
            shape.Position.X = 1.0f - shape.Extent.X;
        } else if (0.0f > shape.Position.X) { 
            shape.Position.X = 0.0f;
        } else if (shape.Position.X > 1.0f - shape.Extent.X) {
            shape.Position.X = 1.0f - shape.Extent.X;
        } else if (shape.Position.Y < 0.0f) {
            shape.Position.Y = 0.0f;
        } else if (shape.Position.Y > 1.0f - shape.Extent.Y) {
            shape.Position.Y = 1.0f - shape.Extent.Y;
        } 
    }

    private void SetMoveLeft(bool val) {
        if (val) {
            moveLeft -= MOVEMENT_SPEED;
        } else {
            moveLeft = 0.0f;
        } UpdateDirection();
    }

    private void SetMoveRight(bool val) {
        if (val) {
            moveRight += MOVEMENT_SPEED;
        } else {
            moveRight = 0.0f;
        } UpdateDirection();
    }

    private void SetMoveUp (bool val) {
        if (val) {
            moveUp += MOVEMENT_SPEED;
        } else {
            moveUp = 0.0f;
        } UpdateDirection();
    }

    private void SetMoveDown (bool val) {
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
