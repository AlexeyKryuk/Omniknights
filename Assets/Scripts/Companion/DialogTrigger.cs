using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private Camera _switchTo;
    [SerializeField] private Camera _switchFrom;

    private void OnEnable()
    {
        _switchTo.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            SwitchCamera();

            if (player.TryGetComponent(out Movement movement))
            {
                movement.enabled = false;
            }
        }
    }

    private void SwitchCamera()
    {
        if (_switchTo.enabled == false)
        {
            _switchTo.enabled = true;
            _switchFrom.enabled = false;
        }
        else
        {
            _switchFrom.enabled = true;
            _switchTo.enabled = false;
        }
    }
}
