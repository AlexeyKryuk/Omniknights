using UnityEngine;

public class Movement : MonoBehaviour 
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private Joystick _joystick;

    private void OnDisable()
    {
        _animator.SetFloat("Speed", 0);
    }

    void Update () 
	{
        Vector3 moveVector = (Vector3.left * _joystick.Horizontal + Vector3.back * _joystick.Vertical);
        _animator.SetFloat("Speed", moveVector.magnitude);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * _moveSpeed * Time.deltaTime, Space.World);
        }   
	}
}