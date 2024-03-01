using NUnit.Framework;
using Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace GalagaTests; 
public class TestsPlayer {
    private Player player; 
    private DynamicShape shape; 
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
    }

   [Test]
    public void TestMove() {
        player.SetMoveLeft(true);
        player.Move();
        Assert.AreEqual(0.44f, player.Shape.Position.X);
        player.SetMoveLeft(false);

        player.SetMoveRight(true);
        player.Move();
        Assert.AreEqual(0.45f, player.Shape.Position.X);
        player.SetMoveRight(false);

        player.SetMoveUp(true);
        player.Move();
        Assert.AreEqual(0.109999999f, player.Shape.Position.Y);
        player.SetMoveUp(false);

        player.SetMoveDown(true);
        player.Move();
        Assert.AreEqual(0.100000001f, player.Shape.Position.Y);
        player.SetMoveDown(false);
    }

    [Test]
    public void TestSetMoveLeft() {
        player.SetMoveLeft(true);
        player.Move();
        Assert.AreEqual(0.44f, player.Shape.Position.X);
        player.SetMoveLeft(false);
        Assert.AreEqual(0.44f, player.Shape.Position.X);
    }

    [Test]
    public void TestSetMoveRight() {
        var expectedPosition = 0.46f;
        var tolerance = 0.001f;
        player.SetMoveRight(true);
        player.Move();
        var positionAferMove = player.Shape.Position.X;
        Assert.AreEqual(expectedPosition, positionAferMove, tolerance);
        player.SetMoveRight(false);
        Assert.AreEqual(expectedPosition, player.Shape.Position.X, tolerance);
    }

    [Test]
    public void TestSetMoveUp() {
        var tolerance = 0.001f;
        var expectedPosition = 0.11f;
        player.SetMoveUp(true);
        player.Move();
        var positionAferMove = player.Shape.Position.Y;
        Assert.AreEqual(expectedPosition, positionAferMove, tolerance);
        player.SetMoveUp(false);
        Assert.AreEqual(expectedPosition, positionAferMove, tolerance);
    }
    
    [Test]
    public void TestSetMoveDown() {
        player.SetMoveDown(true);
        player.Move();
        Assert.AreEqual(0.0900000036f, player.Shape.Position.Y);
        player.SetMoveDown(false);
        Assert.AreEqual(0.0900000036f, player.Shape.Position.Y);
    }

    [Test]
    public void TestGetPosition() {
        var expectedPosition = new Vec2F(0.45f, 0.1f); 
        
        var position = player.GetPosition();
        Assert.AreEqual(expectedPosition.X, position.X);
        Assert.AreEqual(expectedPosition.Y, position.Y);
    }
}