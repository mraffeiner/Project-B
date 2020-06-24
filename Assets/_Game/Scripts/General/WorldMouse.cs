using UnityEngine;
using UnityEngine.InputSystem;

public class WorldMouse : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer = 0;

    private Camera mainCamera;

    private void Awake() => mainCamera = Camera.main;

    private void Update()
    {
        var mouseInScreen = Mouse.current.position.ReadValue();

        if (Physics.Raycast(mainCamera.ScreenPointToRay(mouseInScreen), out var hit, 100f, groundLayer))
        {
            var mouseInWorld = hit.point;

            // Set to ground level
            mouseInWorld.y = 0f;

            transform.position = mouseInWorld;
        }
    }
}