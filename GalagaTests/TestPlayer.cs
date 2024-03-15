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
}