using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private Transform mouse = null;

    [Range(0f, 10f)]
    [SerializeField] private float maxDistance = 3f;
    [Range(0f, 1f)]
    [SerializeField] private float dampen = .1f;

    private Camera mainCamera;

    private void Awake() => mainCamera = Camera.main;

    private void Update()
    {
        // Dampen
        var playerToMouse = (mouse.position - player.position) * (1 - dampen);
        // Clamp to max distance from player
        var newPosition = player.position + (playerToMouse.magnitude > maxDistance ? playerToMouse.normalized * maxDistance : playerToMouse);

        transform.position = newPosition;
    }
}
