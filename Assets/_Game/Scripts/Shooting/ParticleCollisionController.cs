using UnityEngine;

public class ParticleCollisionController : MonoBehaviour
{
    private void OnParticleCollision(GameObject other) => other.GetComponent<IHasHealth>()?.TakeDamage(5);
}
