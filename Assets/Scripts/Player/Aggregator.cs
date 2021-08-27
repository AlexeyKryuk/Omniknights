using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Aggregator : MonoBehaviour
{
    public List<Companion> Companions { get; private set; } = new List<Companion>();

    public UnityAction Added;

    public void TakeOver(Companion companion)
    {
        companion.enabled = true;
        Companions.Add(companion);
        Added?.Invoke();
    }
}
