using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer = 0;
    [SerializeField] private Transform player = null;
    [SerializeField] private float maxDistance = 20f;

    private Camera mainCamera;

    private void Awake() => mainCamera = Camera.main;

    private void Update()
    {
        var mouseInScreen = Mouse.current.position.ReadValue();

        if (Physics.Raycast(mainCamera.ScreenPointToRay(mouseInScreen), out var hit, 100f, groundLayer))
        {
            var mouseInWorld = hit.point;

            // Set position to be within max distance of player
            var playerToMouse = mouseInWorld - player.position;
            if (playerToMouse.magnitude > maxDistance)
                mouseInWorld = player.position + playerToMouse.normalized * maxDistance;

            // Set to ground level
            mouseInWorld.y = 0f;

            transform.position = mouseInWorld;
        }
    }
}