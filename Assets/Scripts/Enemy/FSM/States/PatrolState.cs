using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxRadiansDelta;
    [Space]
    [SerializeField] private Waypoints _waypoints;

    public bool IsActive { get; private set; } = true;

    private Transform _target;

    private void OnEnable()
    {
        Target.Died += OnTargetDie;

        _target = _waypoints.GetNext();
    }

    private void OnDisable()
    {
        if (Target != null)
            Target.Died -= OnTargetDie;

        Animate(false);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _target.position) < 1f)
        {
            _target = _waypoints.GetNext();
        }

        if (IsActive)
        {
            Move(_target.position);
            Rotate(_target.position);
        }

        Animate(IsActive);
    }

    private void Move(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
    }

    private void Rotate(Vector3 target)
    {
        Vector3 targetDirection = target - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _maxRadiansDelta, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void Animate(bool state)
    {
        Animator.SetBool(AnimatorEnemyController.Params.Walk, state);
    }

    public void Enable() => IsActive = true;
    public void Disable() => IsActive = false;
}
