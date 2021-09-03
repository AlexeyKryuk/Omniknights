using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float HealthChangeSpeed = 0.05f;

    private Animator _animator;

    public float Health { get; private set; }
    public float MaxHealth { get; private set; }
    public float MaxDanger { get; private set; }

    public UnityAction Died;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        MaxHealth = 8;
        MaxDanger = 8;
        Health = MaxHealth;
    }

    private void Update()
    {
        Health = Mathf.MoveTowards(Health, 0, HealthChangeSpeed * Time.deltaTime);
    }

    public void ApplyDamage(float amount)
    {
        Health -= amount;
        _animator.SetTrigger("Damage");

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    private void Die()
    {
        _animator.SetTrigger("Death");
        Died?.Invoke();
    }
}
