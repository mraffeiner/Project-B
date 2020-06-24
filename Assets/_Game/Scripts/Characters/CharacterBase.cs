using System;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IHasHealth
{
    public event Action<IHasHealth> HealthReachedZero;

    [SerializeField] protected IntVariable maxHealth = null;
    [SerializeField] protected IntVariable health = null;

    public virtual void TakeDamage(int value)
    {
        if (health == null)
            return;

        health.value = Mathf.Clamp(health.value - value, 0, maxHealth.value);
        if (health.value == 0)
        {
            HealthReachedZero?.Invoke(this);
            Die();
        }
    }

    public void Heal(int value)
    {
        health.value = Mathf.Clamp(health.value + value, 0, maxHealth.value);
        Debug.Log($"{name} healed for {value}");
    }

    public void Die() => Destroy(gameObject);
}
