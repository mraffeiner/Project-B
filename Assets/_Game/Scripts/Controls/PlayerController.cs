using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform lookAtTarget = null;
    [SerializeField] private float moveSpeed = 10f;

    private Camera mainCam;
    private CharacterController controller;
    private Animator animator;
    private Vector2 moveInput;

    private void Awake()
    {
        mainCam = Camera.main;
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    private void Update()
    {
        Look();
        Move();
    }

    private void Move()
    {
        HandleAnimations();
        controller.SimpleMove(new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed);
    }
    private void Look()
    {
        var oldRotation = transform.rotation;
        transform.LookAt(lookAtTarget);

        // Reset x and z euler to only rotate on y axis
        var newRotation = transform.rotation;
        newRotation.x = oldRotation.x;
        newRotation.z = oldRotation.z;

        transform.rotation = newRotation;
    }

    private void HandleAnimations()
    {
        var localMoveInput = transform.InverseTransformDirection(new Vector3(moveInput.x, 0f, moveInput.y));
        if (animator != null)
        {
            if (Mathf.Abs(localMoveInput.z) > Mathf.Abs(localMoveInput.x))
            {
                animator.SetFloat("Horizontal", 0f);
                animator.SetFloat("Vertical", localMoveInput.z);
            }
            else
            {
                animator.SetFloat("Vertical", 0f);
                animator.SetFloat("Horizontal", localMoveInput.x);
            }
        }
    }
}
