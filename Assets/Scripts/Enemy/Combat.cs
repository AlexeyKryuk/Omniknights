using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Combat : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Coroutine _coroutine;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.Damaged += OnDamage;
        _enemy.Killed += OnKill;
    }

    private void OnDisable()
    {
        _enemy.Damaged -= OnDamage;
        _enemy.Killed -= OnKill;
    }

    private void OnDamage() => _animator.SetTrigger("Damage");
    private void OnKill()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Kill());
    }

    private IEnumerator Kill()
    {
        _animator.SetTrigger("Death");

        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }
}
