using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompanionsViewer : MonoBehaviour
{
    [SerializeField] Aggregator _aggregator;
    [SerializeField] TMP_Text _text;

    private void OnEnable()
    {
        _aggregator.Added += UpdateValue;
    }

    private void OnDisable()
    {
        _aggregator.Added -= UpdateValue;
    }

    private void UpdateValue()
    {
        _text.text = _aggregator.Companions.Count.ToString();
    }
}
