namespace _Project.Gameplay.Shared.Scripts.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
        bool HasForceOnHit();
        void Heal(int amount);

    }
}