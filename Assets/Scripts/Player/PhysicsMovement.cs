using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PhysicsMovement : MonoBehaviour 
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed = 0.3f;

    private CharacterController _characterController;
    private Vector3 _movementVector;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnDisable()
    {
        _animator.SetFloat("Speed", 0);
    }

    private void Update()
    {
        Move();
        Rotate();
        Animate();
    }

    private void Move()
    {
        _movementVector = new Vector3(-_joystick.Horizontal, 0, -_joystick.Vertical);

        if (_movementVector != Vector3.zero)
        {
            _characterController.SimpleMove(_movementVector * _speed);
        }
    }

    private void Rotate()
    {
        Vector3 lookDirection = _movementVector + transform.position;
        transform.LookAt(new Vector3(lookDirection.x, transform.position.y, lookDirection.z));
    }

    private void Animate()
    {
        _animator.SetFloat("Speed", _movementVector.magnitude);
    }
}