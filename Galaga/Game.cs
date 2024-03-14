using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Physics;
using Galaga.MovementStrategy;

namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private GameEventBus eventBus;
    private Player player;
    private EntityContainer<Enemy> enemies;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private ZigZagDown movementStrategy;

    private const int EXPLOSION_LENGTH_MS = 500;
    
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { 
            GameEventType.PlayerEvent, 
            GameEventType.WindowEvent,
            GameEventType.InputEvent
        });

        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        List<Image> images = ImageStride.CreateStrides
            (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        const int numEnemies = 8;
        enemies = new EntityContainer<Enemy>(numEnemies);
        for (int i = 0; i < numEnemies; i++) {
            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, images)));
        }

        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

        enemyExplosions = new AnimationContainer(numEnemies);
        explosionStrides = ImageStride.CreateStrides(8,
            Path.Combine("Assets", "Images", "Explosion.png"));

        movementStrategy = new ZigZagDown(enemies);
    }
    
    public override void Render() {
        player.Render();
        playerShots.RenderEntities();
        enemies.RenderEntities();
        enemyExplosions.RenderAnimations();
    }
    
    public override void Update() {
        window.PollEvents();
        player.Move();
        eventBus.ProcessEventsSequentially();
        IterateShots();
        movementStrategy.MoveEnemies(enemies);
    }

    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.WindowEvent, 
                    From = this, 
                    To = this, 
                    Message = "CLOSE_WINDOW", 
                    StringArg1 = "", 
                    StringArg2 = "",
                    ObjectArg1= "", 
                    IntArg1 = 0, 
                    Id = 9
                });
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.PlayerEvent, 
                    From = this, 
                    To = player, 
                    Message = "MOVE_PLAYER_RIGHT", 
                    StringArg1 = "KEY_PRESS", 
                    StringArg2 = "",
                    ObjectArg1= "", 
                    IntArg1 = 0, 
                    Id = 0
                });
                break;
            case KeyboardKey.Left:
                eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.PlayerEvent, 
                    From = this, 
                    To = player, 
                    Message = "MOVE_PLAYER_LEFT", 
                    StringArg1 = "KEY_PRESS", 
                    StringArg2 = "",
                    ObjectArg1= "", 
                    IntArg1 = 0, 
                    Id = 2
                });
                break;
                case KeyboardKey.Up:
                eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.PlayerEvent, 
                    From = this, 
                    To = player, 
                    Message = "MOVE_PLAYER_UP", 
                    StringArg1 = "KEY_PRESS", 
                    StringArg2 = "",
                    ObjectArg1= "", 
                    IntArg1 = 0, 
                    Id = 4
                });
                break;
            case KeyboardKey.Down:
                eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.PlayerEvent, 
                    From = this, 
                    To = player, 
                    Message = "MOVE_PLAYER_DOWN", 
                    StringArg1 = "KEY_PRESS", 
                    StringArg2 = "",
                    ObjectArg1= "", 
                    IntArg1 = 0, 
                    Id = 6
                });
                break;
        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Right:
                eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.PlayerEvent, 
                    From = this, 
                    To = player, 
                    Message = "MOVE_PLAYER_RIGHT", 
                    StringArg1 = "KEY_RELEASE", 
                    StringArg2 = "",
                    ObjectArg1= "", 
                    IntArg1 = 0, 
                    Id = 1
                });
                break;
            case KeyboardKey.Left:
                eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.PlayerEvent, 
                    From = this, 
                    To = player, 
                    Message = "MOVE_PLAYER_LEFT", 
                    StringArg1 = "KEY_RELEASE", 
                    StringArg2 = "",
                    ObjectArg1= "", 
                    IntArg1 = 0, 
                    Id = 3
                });
                break;
            case KeyboardKey.Up:
                eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.PlayerEvent, 
                    From = this, 
                    To = player, 
                    Message = "MOVE_PLAYER_UP", 
                    StringArg1 = "KEY_RELEASE", 
                    StringArg2 = "",
                    ObjectArg1= "", 
                    IntArg1 = 0, 
                    Id = 5
                });
                break;
            case KeyboardKey.Down:
                eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.PlayerEvent, 
                    From = this, 
                    To = player, 
                    Message = "MOVE_PLAYER_DOWN", 
                    StringArg1 = "KEY_RELEASE", 
                    StringArg2 = "",
                    ObjectArg1= "", 
                    IntArg1 = 0, 
                    Id = 7
                });
                break;
            case KeyboardKey.Space:
                eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.InputEvent, 
                    From = this, 
                    To = this, 
                    Message = "SHOOT", 
                    StringArg1 = "", 
                    StringArg2 = "",
                    ObjectArg1= "", 
                    IntArg1 = 0, 
                    Id = 11
                });
                break;
        }
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    window.CloseWindow();
                    break;
            }
        }
        if (gameEvent.EventType == GameEventType.InputEvent) {
            switch (gameEvent.Message) {
                case "SHOOT":
                Vec2F shotPosition = new Vec2F(
                    player.GetPosition().X + 0.047f, player.GetPosition().Y
                ); 
                PlayerShot newShot = new PlayerShot(shotPosition, playerShotImage);
                playerShots.AddEntity(newShot);
                break;
            }
        }
        
    }


    private void IterateShots() {
        playerShots.Iterate(shot => {
            shot.Shape.Move();
            if (shot.Shape.Position.Y > 1.0f || shot.Shape.Position.Y < 0.0f) {
                shot.DeleteEntity();
            } else {
                enemies.Iterate(enemy => {
                    var collide = CollisionDetection.Aabb((DynamicShape) shot.Shape, enemy.Shape);
                    if (collide.Collision) {
                        AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                        shot.DeleteEntity();
                        enemy.DeleteEntity();
                    }
                });
            }
        });
    }

    public void AddExplosion(Vec2F position, Vec2F extent) {
        StationaryShape expStatShap = new StationaryShape(position, extent);
        ImageStride expImageStride = new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides);
        enemyExplosions.AddAnimation(expStatShap, EXPLOSION_LENGTH_MS, expImageStride);
    }
}
