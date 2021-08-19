using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _lerpTime;
    [SerializeField] private float _followStrength;

    private Vector3 _targetPos;
    private float _velocity;
    
    private void Update()
    {
        if (_targetPoint != null)
        {
            Vector3 relativePos = transform.position;
            Vector3 targetDirection = (_targetPoint.position - relativePos);

            _velocity = targetDirection.magnitude * _followStrength;

            if (Vector3.Distance(transform.position, _targetPoint.position) < 3f)
                _velocity = 0;

            _targetPos = transform.position + (targetDirection.normalized * _velocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, _targetPos, _lerpTime);
            transform.LookAt(_targetPos, Vector3.up);

            _animator.SetFloat("Speed", Mathf.Clamp01(_velocity));
        }
    }
}
