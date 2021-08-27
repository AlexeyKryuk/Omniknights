using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _character;
    [Space]
    [SerializeField] private Button _button;
    [SerializeField] private Joystick _joystick;
    [Space]
    [SerializeField] private float _range;
    [SerializeField] private int _damage;

    private Enemy _target;

    public bool IsAttacking { get; private set; }

    private void OnEnable()
    {
        _button.onClick.AddListener(Attack);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Attack);
    }

    private void Update()
    {
        FindEnemy();
    }

    private void Attack()
    {
        if (!IsAttacking)
        {
            Animate();
        }
    }

    private void FindEnemy()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + _character.center;

        if (Physics.SphereCast(origin, _character.height / 2, transform.forward, out hit, _range))
        {
            if (hit.transform.TryGetComponent(out Enemy enemy))
                _target = enemy;
            else
                _target = null;
        }
        else
        {
            _target = null;
        }
    }

    private void Animate()
    {
        _animator.SetTrigger("Attack");
        IsAttacking = true;
        _joystick.IsActive = false;
    }

    private void OnAnimationStop()
    {
        IsAttacking = false;
        _joystick.IsActive = true;
    }

    private void HitEnemy()
    {
        if (_target != null)
            _target.Damage(_damage);
    }
}
