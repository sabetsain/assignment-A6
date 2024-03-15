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
public class TestsMovementStrategy {
    private Enemy mainEnemy;
    private EntityContainer<Enemy> enemies;

    [OneTimeSetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }

    [SetUp]
    public void Setup() {
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