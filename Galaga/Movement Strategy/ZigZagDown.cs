using DIKUArcade.Math;
using DIKUArcade.Entities;
using System;

namespace Galaga.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        public ZigZagDown(EntityContainer<Enemy> enemies) {
        }

        public void MoveEnemy (Enemy enemy) {
            float downSpeed = 0.0003f;
            float wavePeriod = 0.045f;
            float waveAmp = 0.05f;

            enemy.Shape.AsDynamicShape().Position.Y -= downSpeed;
            enemy.Shape.AsDynamicShape().Position.X = 
                enemy.getStartPos().X + (
                    waveAmp * (MathF.Sin((2.0f * MathF.PI * (
                        enemy.getStartPos().Y - enemy.Shape.AsDynamicShape().Position.Y
                    )) / wavePeriod))
                );
            enemy.Shape.Move(enemy.Shape.AsDynamicShape().Direction);
        }

        public void MoveEnemies (EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}
