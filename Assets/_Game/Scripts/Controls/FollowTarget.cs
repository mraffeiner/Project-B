using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private bool fixedRotation = true;

    private void Update()
    {
        transform.position = target.position;
        if (!fixedRotation)
            transform.rotation = target.rotation;
    }
}
