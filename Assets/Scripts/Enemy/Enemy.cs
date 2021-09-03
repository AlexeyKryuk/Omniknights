using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _maxHealth;

    public int Health { get; private set; }
    public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
    public Player Target => _target;

    public UnityAction Damaged;
    public UnityAction Killed;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        Health = MaxHealth;
        Target.Died += OnTargetDie;
    }

    private void OnDisable()
    {
        if (Target != null)
            Target.Died -= OnTargetDie;
    }

    public void Damage(int amount)
    {
        Health -= amount;
        Damaged?.Invoke();

        if (Health <= 0)
        {
            Health = 0;
            Kill();
        }

        _animator.SetTrigger(AnimatorEnemyController.Params.Damage);
    }

    private void Kill()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(KillCoroutine());
    }

    private IEnumerator KillCoroutine()
    {
        _animator.SetTrigger(AnimatorEnemyController.Params.Death);

        yield return new WaitForSeconds(3.5f);

        Killed?.Invoke();
        Destroy(gameObject);
    }

    private void OnTargetDie()
    {
        _target = null;
    }
}
