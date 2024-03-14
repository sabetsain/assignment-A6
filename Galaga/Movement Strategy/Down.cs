using DIKUArcade.Math;
using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    public class Down : IMovementStrategy {
        public Down(EntityContainer<Enemy> enemies) {
        }

        public void MoveEnemy (Enemy enemy) {
            enemy.Shape.AsDynamicShape().Direction = new Vec2F(0.0f, -0.002f);
            enemy.Shape.Move(enemy.Shape.AsDynamicShape().Direction);
        }

        public void MoveEnemies (EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}
