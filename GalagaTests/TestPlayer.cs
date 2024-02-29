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

    [SetUp]
    public void Setup() {
        shape = new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f));
        image = new Image("Assets/Images/Player.png");
        player = new Player(shape, image);
    }

    [Test]
    public void TestMove() {
        player.SetMoveLeft(true);
        player.Move();
        Assert.AreEqual(0.0f, player.shape.Position.X);
        player.SetMoveRight(true);
        player.Move();
        Assert.AreEqual(0.01f, player.shape.Position.X);
        player.SetMoveDown(true);
        player.Move();
        Assert.AreEqual(0.01f, player.shape.Position.X);
        player.SetMoveUp(true);
        player.Move();
        Assert.AreEqual(0.01f, player.shape.Position.X);

        player.SetMoveLeft(false);
        player.SetMoveRight(false);
        player.SetMoveDown(false);
        player.SetMoveUp(false);
        player.Move();
        Assert.AreEqual(0.01f, player.shape.Position.X); 
    }

    [Test]
    public void TestSetMoveLeft() {
        // Test that the player moves left when SetMoveLeft is called with true
        player.SetMoveLeft(true);
        // The player should move left
        Assert.AreEqual(-0.01f, player.shape.Direction.X);
        // Test that the player stops moving left when SetMoveLeft is called with false
        player.SetMoveLeft(false);
        // The player should stop moving left
        Assert.AreEqual(0.0f, player.shape.Direction.X);
    }
    
    [Test]

    public void TestSetMoveDown() {
        // Act
        player.SetMoveDown(true);
        
        // Assert
        Assert.AreEqual(-0.01f, player.moveDown);
        
        // Act
        player.SetMoveDown(false);
        
        // Assert
        Assert.AreEqual(0.0f, player.moveDown);
    }

    [Test]
    public void TestGetPosition() {
        var expectedPosition = new Vec2F(0.0f, 0.0f);
        
        // Act
        var position = player.GetPosition();
        
        // Assert
        Assert.AreEqual(expectedPosition, position);
    }
}
