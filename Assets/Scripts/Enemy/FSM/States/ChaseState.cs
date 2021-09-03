using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class ChaseState : State
{
    [SerializeField] private float _maxRadiansDelta;
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToWalkspeed;

    public float WalkSpeed => _speed / 2;
    public float RunSpeed => _speed;

    private float _transitionRange;

    private void OnEnable()
    {
        Target.Died += OnTargetDie;

        Animator.SetBool(AnimatorEnemyController.Params.Walk, true);

        var distanceTransition = (DistanceTransition)Transitions[0];

        if (distanceTransition != null)
            _transitionRange = distanceTransition.Range;
    }

    private void OnDisable()
    {
        if (Target != null)
            Target.Died -= OnTargetDie;
    }

    private void Update()
    {
        if (Target != null)
        {
            float distance = Vector3.Distance(transform.position, Target.transform.position);
            bool isNear = distance < _transitionRange + _distanceToWalkspeed;
            float speed = isNear ? WalkSpeed : RunSpeed;

            Animator.SetBool(AnimatorEnemyController.Params.Run, !isNear);

            MoveToTarget(Target.transform.position, speed);
            RotateToTarget(Target.transform.position);
        }
    }

    private void MoveToTarget(Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void RotateToTarget(Vector3 target)
    {
        Vector3 targetDirection = target - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _maxRadiansDelta, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
