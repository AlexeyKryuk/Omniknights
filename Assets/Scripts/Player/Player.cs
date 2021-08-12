using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AttackBehaviour))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float HealthChangeSpeed = 0.05f;

    private AttackBehaviour _attackBehaviour;
    private Animator _animator;

    public float Health { get; private set; }
    public float MaxHealth { get; private set; }
    public float MaxDanger { get; private set; }

    public UnityAction HealthFilled;

    private void Awake()
    {
        _attackBehaviour = GetComponent<AttackBehaviour>();
        _animator = GetComponent<Animator>();

        MaxHealth = 8;
        MaxDanger = 8;
        Health = MaxHealth;
    }

    private void OnEnable()
    {
        _attackBehaviour.EnemyKilled += HealthRecovery;
    }

    private void OnDisable()
    {
        _attackBehaviour.EnemyKilled -= HealthRecovery;
    }

    private void Update()
    {
        Health = Mathf.MoveTowards(Health, 0, HealthChangeSpeed * Time.deltaTime);
    }

    private void HealthRecovery()
    {
        Health += 0.5f;

        if (Health >= 10)
        {
            Health = 10;
            HealthFilled?.Invoke();
        }
    }

    public void ApplyDamage(float amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    private void Die()
    {
        _animator.SetTrigger("Death");
    }
}
