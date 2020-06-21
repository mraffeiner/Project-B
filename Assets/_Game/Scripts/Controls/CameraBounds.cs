using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer = 0;

    private Camera mainCamera;

    private void Awake() => mainCamera = Camera.main;
}
