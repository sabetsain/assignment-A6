using DIKUArcade.Math;
using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    public class NoMove : IMovementStrategy {
        public NoMove(EntityContainer<Enemy> enemies) {
        }

        public void MoveEnemy (Enemy enemy) {
            enemy.Shape.AsDynamicShape().Direction = new Vec2F(0.0f, 0.0f);
            enemy.Shape.Move();
        }

        public void MoveEnemies (EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}
