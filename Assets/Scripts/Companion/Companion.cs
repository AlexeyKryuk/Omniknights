using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    [SerializeField] private Following _following;

    private void OnEnable()
    {
        _following.enabled = true;
    }
}
