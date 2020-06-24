using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class ControllerBase : MonoBehaviour
{
    [SerializeField] protected float attackCooldown = 1f;
    [SerializeField] private float moveSpeed = 10f;

    protected CharacterController controller;
    protected Animator animator;
    protected float remainingAttackCooldown = 0f;

    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Turn();
        Move();

        if (remainingAttackCooldown > 0f)
            remainingAttackCooldown -= Time.deltaTime;
    }

    protected virtual Quaternion GetTurnRotation() => Quaternion.LookRotation(transform.forward);

    protected virtual Vector3 GetMoveDirection() => transform.forward;

    protected virtual void HandleTurnAnimation(Quaternion rotation) { }

    protected virtual void HandleMoveAnimation(Vector3 movementVector) { }

    private void Turn()
    {
        var rotation = GetTurnRotation();
        HandleTurnAnimation(rotation);
        transform.rotation = rotation;
    }

    private void Move()
    {
        var moveDirection = GetMoveDirection();
        HandleMoveAnimation(moveDirection);
        controller.SimpleMove(moveDirection * moveSpeed);
    }
}
