public interface IHasHealth
{
    void TakeDamage(int value);
    void Heal(int value);
    void Die();
}