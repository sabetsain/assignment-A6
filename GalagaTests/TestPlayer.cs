using NUnit.Framework;
using Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System.Collections.Generic;
using Galaga.MovementStrategy;
using System.IO;
using System;

namespace GalagaTests; 
public class TestsPlayer {
    private Player player; 
    private DynamicShape shape; 
    private GameEventBus eventBus;
    private IBaseImage image; 
    private Enemy mainEnemy;
    private EntityContainer<Enemy> enemies;

    [OneTimeSetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }

    [SetUp]
    public void Setup() {
        shape = new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f));
        image = new Image("../Galaga/Assets/Images/Player.png");
        player = new Player(shape, image);

        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.PlayerEvent});
        eventBus.Subscribe(GameEventType.PlayerEvent, player);

        List<Image> images = ImageStride.CreateStrides
            (4, "../Galaga/Assets/Images/BlueMonster.png");
        const int numEnemies = 1;
        enemies = new EntityContainer<Enemy>(numEnemies);
        mainEnemy = new Enemy(
                new DynamicShape(new Vec2F(0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, images));
        enemies.AddEntity(mainEnemy);
    }

   [Test]
    public void TestMoveRightSide() {
        // Arrange 1
        player.Shape.Position = new Vec2F (0.9f, 0.9f);
        var expectedPosition = 0.9f;
        var tolerance = 0.001f;
        GameEvent gameEventPress = new GameEvent {
            EventType = GameEventType.PlayerEvent, 
            From = this, 
            To = player, 
            Message = "MOVE_PLAYER_RIGHT", 
            StringArg1 = "KEY_PRESS", 
        };
        
        // Act 1
        eventBus.RegisterEvent (gameEventPress);
        player.ProcessEvent(gameEventPress);
        player.Move(); 
        var positionAferMove = player.Shape.Position.X;

        // Assert 1
        Assert.AreEqual(expectedPosition, positionAferMove, tolerance);
    }

    [Test]
    public void TestMoveUpSide() {
        // Arrange 1
        player.Shape.Position = new Vec2F (0.9f, 0.9f);
        var expectedPosition = 0.9f;
        var tolerance = 0.001f;
        GameEvent gameEventPress = new GameEvent {
            EventType = GameEventType.PlayerEvent, 
            From = this, 
            To = player, 
            Message = "MOVE_PLAYER_UP", 
            StringArg1 = "KEY_PRESS", 
        };
        
        // Act 1
        eventBus.RegisterEvent (gameEventPress);
        player.ProcessEvent(gameEventPress);
        player.Move(); 
        var positionAferMove = player.Shape.Position.X;

        // Assert 1
        Assert.AreEqual(expectedPosition, positionAferMove, tolerance);
    }

    [Test]
    public void TestMoveDownSide() {
        // Arrange 1
        player.Shape.Position = new Vec2F (0.0f, 0.0f);
        var expectedPosition = 0.0f;
        var tolerance = 0.001f;
        GameEvent gameEventPress = new GameEvent {
            EventType = GameEventType.PlayerEvent, 
            From = this, 
            To = player, 
            Message = "MOVE_PLAYER_DOWN", 
            StringArg1 = "KEY_PRESS", 
        };
        
        // Act 1
        eventBus.RegisterEvent (gameEventPress);
        player.ProcessEvent(gameEventPress);
        player.Move(); 
        var positionAferMove = player.Shape.Position.X;

        // Assert 1
        Assert.AreEqual(expectedPosition, positionAferMove, tolerance);
    }    

    [Test]
    public void TestMoveLeftSide() {
        // Arrange 1
        player.Shape.Position = new Vec2F (0.0f, 0.0f);
        var expectedPosition = 0.0f;
        var tolerance = 0.001f;
        GameEvent gameEventPress = new GameEvent {
            EventType = GameEventType.PlayerEvent, 
            From = this, 
            To = player, 
            Message = "MOVE_PLAYER_LEFT", 
            StringArg1 = "KEY_PRESS", 
        };
        
        // Act 1
        eventBus.RegisterEvent (gameEventPress);
        player.ProcessEvent(gameEventPress);
        player.Move(); 
        var positionAferMove = player.Shape.Position.X;

        // Assert 1
        Assert.AreEqual(expectedPosition, positionAferMove, tolerance);
    }    

    [Test]
    public void TestSetMoveLeft() {
        // Arrange 1
        var expectedPosition = 0.44f;
        var tolerance = 0.001f;
        GameEvent gameEventPress = new GameEvent {
            EventType = GameEventType.PlayerEvent, 
            From = this, 
            To = player, 
            Message = "MOVE_PLAYER_LEFT", 
            StringArg1 = "KEY_PRESS", 
        };
        
        // Act 1
        eventBus.RegisterEvent (gameEventPress);
        player.ProcessEvent(gameEventPress);
        player.Move(); 
        var positionAferMove = player.Shape.Position.X;

        // Assert 1
        Assert.AreEqual(expectedPosition, positionAferMove, tolerance);

        // Arrange 2
        GameEvent gameEventRelease = new GameEvent {
            EventType = GameEventType.PlayerEvent, 
            From = this, 
            To = player, 
            Message = "MOVE_PLAYER_LEFT", 
            StringArg1 = "KEY_RELEASE", 
        };
        eventBus.RegisterEvent(gameEventRelease);
        player.ProcessEvent(gameEventRelease);
        player.Move(); 

        // Assert 2
        Assert.AreEqual(expectedPosition, player.Shape.Position.X, tolerance);
    }

    [Test]
    public void TestSetMoveRight() {
        // Arrange 1
        var expectedPosition = 0.46f;
        var tolerance = 0.001f;
        GameEvent gameEventPress = new GameEvent {
            EventType = GameEventType.PlayerEvent, 
            From = this, 
            To = player, 
            Message = "MOVE_PLAYER_RIGHT", 
            StringArg1 = "KEY_PRESS", 
        };
        
        // Act 1
        eventBus.RegisterEvent (gameEventPress);
        player.ProcessEvent(gameEventPress);
        player.Move(); 
        var positionAferMove = player.Shape.Position.X;

        // Assert 1
        Assert.AreEqual(expectedPosition, positionAferMove, tolerance);

        // Arrange 2
        GameEvent gameEventRelease = new GameEvent {
            EventType = GameEventType.PlayerEvent, 
            From = this, 
            To = player, 
            Message = "MOVE_PLAYER_RIGHT", 
            StringArg1 = "KEY_RELEASE", 
        };
        eventBus.RegisterEvent(gameEventRelease);
        player.ProcessEvent(gameEventRelease);
        player.Move(); 

        // Assert 2
        Assert.AreEqual(expectedPosition, player.Shape.Position.X, tolerance);
    }

    [Test]
    public void TestSetMoveUp() {
        // Arrange 1
        var expectedPosition = 0.11f;
        var tolerance = 0.001f;
        GameEvent gameEventPress = new GameEvent {
            EventType = GameEventType.PlayerEvent, 
            From = this, 
            To = player, 
            Message = "MOVE_PLAYER_UP", 
            StringArg1 = "KEY_PRESS", 
        };
        
        // Act 1
        eventBus.RegisterEvent (gameEventPress);
        player.ProcessEvent(gameEventPress);
        player.Move(); 
        var positionAferMove = player.Shape.Position.Y;

        // Assert 1
        Assert.AreEqual(expectedPosition, positionAferMove, tolerance);

        // Arrange 2
        GameEvent gameEventRelease = new GameEvent {
            EventType = GameEventType.PlayerEvent, 
            From = this, 
            To = player, 
            Message = "MOVE_PLAYER_UP", 
            StringArg1 = "KEY_RELEASE", 
        };
        eventBus.RegisterEvent(gameEventRelease);
        player.ProcessEvent(gameEventRelease);
        player.Move(); 

        // Assert 2
        Assert.AreEqual(expectedPosition, player.Shape.Position.Y, tolerance);
    }

    [Test]
    public void TestSetMoveDown() {
        // Arrange 1
        var expectedPosition = 0.09f;
        var tolerance = 0.001f;
        GameEvent gameEventPress = new GameEvent {
            EventType = GameEventType.PlayerEvent, 
            From = this, 
            To = player, 
            Message = "MOVE_PLAYER_DOWN", 
            StringArg1 = "KEY_PRESS", 
        };
        
        // Act 1
        eventBus.RegisterEvent (gameEventPress);
        player.ProcessEvent(gameEventPress);
        player.Move(); 
        var positionAferMove = player.Shape.Position.Y;

        // Assert 1
        Assert.AreEqual(expectedPosition, positionAferMove, tolerance);

        // Arrange 2
        GameEvent gameEventRelease = new GameEvent {
            EventType = GameEventType.PlayerEvent, 
            From = this, 
            To = player, 
            Message = "MOVE_PLAYER_DOWN", 
            StringArg1 = "KEY_RELEASE", 
        };
        eventBus.RegisterEvent(gameEventRelease);
        player.ProcessEvent(gameEventRelease);
        player.Move(); 

        // Assert 2
        Assert.AreEqual(expectedPosition, player.Shape.Position.Y, tolerance);
    }

    [Test]
    public void TestGetPosition() {
        var expectedPosition = new Vec2F(0.45f, 0.1f); 
        
        var position = player.GetPosition();
        Assert.AreEqual(expectedPosition.X, position.X);
        Assert.AreEqual(expectedPosition.Y, position.Y);
    } 

    [Test]
    public void TestNoMove() {
        NoMove movementStrategy = new NoMove(enemies);
        
        var expectedPositionX = 0.1f;
        var expectedPositionY = 0.9f;

        movementStrategy.MoveEnemies(enemies);
        
        var actualPosition = mainEnemy.Shape.Position;
        Assert.AreEqual(expectedPositionX, actualPosition.X);
        Assert.AreEqual(expectedPositionY, actualPosition.Y);
    } 

    [Test]
    public void TestDown() {
        Down movementStrategy = new Down(enemies);
        
        var expectedPositionX = 0.1f;
        var expectedPositionY = 0.898f;
        var tolerance = 0.0001f;

        movementStrategy.MoveEnemies(enemies);
        
        var actualPosition = mainEnemy.Shape.Position;
        Assert.AreEqual(expectedPositionX, actualPosition.X, tolerance);
        Assert.AreEqual(expectedPositionY, actualPosition.Y, tolerance);
    } 

    [Test]
    public void TestZigZagDown() {
        ZigZagDown movementStrategy = new ZigZagDown(enemies);
        
        var expectedPositionX = mainEnemy.getStartPos().X + (
                    0.05f * (MathF.Sin((2.0f * MathF.PI * (
                        mainEnemy.getStartPos().Y - mainEnemy.Shape.AsDynamicShape().Position.Y
                    )) / 0.045f))
                );
        var expectedPositionY = 0.8997f;

        movementStrategy.MoveEnemies(enemies);
        
        var actualPosition = mainEnemy.Shape.Position;
        Assert.AreEqual(expectedPositionX, actualPosition.X);
        Assert.AreEqual(expectedPositionY, actualPosition.Y);
    } 
}