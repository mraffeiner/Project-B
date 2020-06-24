using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : ControllerBase
{
    [SerializeField] private Transform lookAtTarget = null;
    [SerializeField] private ParticleSystem attackParticles = null;

    private Camera mainCam;
    private Vector2 moveInput;

    protected override void Awake()
    {
        base.Awake();
        mainCam = Camera.main;
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    public void OnAttack() => Shoot();

    protected override Vector3 GetMoveDirection() => new Vector3(moveInput.x, 0f, moveInput.y);

    protected override Quaternion GetTurnRotation()
    {
        var oldRotation = transform.rotation;
        transform.LookAt(lookAtTarget);

        // Reset x and z euler to only rotate on y axis
        var newRotation = transform.rotation;
        newRotation.x = oldRotation.x;
        newRotation.z = oldRotation.z;

        return newRotation;
    }

    protected override void HandleMoveAnimation(Vector3 moveDirection)
    {
        if (animator == null)
            return;

        // Get movement in local space to choose animation relative to mouse
        var localMoveDirection = transform.InverseTransformDirection(moveDirection);

        // Set bool to differentiate which input is more dominant
        var verticalDominant = Mathf.Abs(localMoveDirection.z) > Mathf.Abs(localMoveDirection.x);

        animator.SetFloat("Horizontal", localMoveDirection.x);
        animator.SetFloat("Vertical", localMoveDirection.z);
        animator.SetBool("VerticalDominant", verticalDominant);
    }

    private void Shoot()
    {
        if (remainingAttackCooldown <= 0f)
        {
            attackParticles.Play();
            remainingAttackCooldown = attackCooldown;
        }
    }
}
