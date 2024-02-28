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

namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private GameEventBus eventBus;
    private Player player;
    private EntityContainer<Enemy> enemies;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;

    private const int EXPLOSION_LENGTH_MS = 500;
    
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
        window.SetKeyEventHandler(KeyHandler);
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
    }

    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(true);
                break;
            case KeyboardKey.Left:
                player.SetMoveLeft(true);
                break;
            case KeyboardKey.Up:
                player.SetMoveUp(true);
                break;
            case KeyboardKey.Down:
                player.SetMoveDown(true);
                break;
        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Right:
                player.SetMoveRight(false);
                break;
            case KeyboardKey.Left:
                player.SetMoveLeft(false);
                break;
            case KeyboardKey.Up:
                player.SetMoveUp(false);
                break;
            case KeyboardKey.Down:
                player.SetMoveDown(false);
                break;
            case KeyboardKey.Space:
                Vec2F shotPosition = new Vec2F(
                    player.GetPosition().X + 0.047f, player.GetPosition().Y
                ); 
                PlayerShot newShot = new PlayerShot(shotPosition, playerShotImage);
                playerShots.AddEntity(newShot);
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
        // TODO: add explosion to the AnimationContainer
    }
}
