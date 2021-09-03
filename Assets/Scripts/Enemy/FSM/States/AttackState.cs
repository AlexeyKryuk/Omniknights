using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;

    private bool _isAttacking;

    private void OnEnable()
    {
        Target.Died += OnTargetDie;

        Animator.SetBool(AnimatorEnemyController.Params.Run, false);
        Animator.SetBool(AnimatorEnemyController.Params.Walk, false);
    }

    private void OnDisable()
    {
        if (Target != null)
            Target.Died -= OnTargetDie;
    }

    private void Update()
    {
        if (Target != null)
            Attack();
    }

    private void Attack()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            Animator.SetTrigger(AnimatorEnemyController.Params.Attack);
        }
    }

    private void OnAnimationStop()
    {
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        _isAttacking = false;
    }

    private void HitPlayer()
    {
        Target.ApplyDamage(_damage);
    }
}
