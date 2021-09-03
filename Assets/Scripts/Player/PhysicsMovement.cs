using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
public class PhysicsMovement : MonoBehaviour 
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed = 0.3f;

    private CharacterController _characterController;
    private Player _player;
    private Vector3 _movementVector;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _animator.SetFloat("Speed", 0);
        _player.Died -= OnPlayerDied;
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

    private void OnPlayerDied()
    {
        enabled = false;
    }
}