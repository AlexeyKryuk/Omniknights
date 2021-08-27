using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    public int Health { get; private set; }
    public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }

    public UnityAction Damaged;
    public UnityAction Killed;

    private void OnEnable()
    {
        Health = MaxHealth;
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
    }

    public void Kill()
    {
        Killed?.Invoke();
    }
}
