using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private List<Transform> _points = new List<Transform>();

    private int _currentIndex;

    private void OnEnable()
    {
        _currentIndex = -1;
    }

    public Transform GetNext()
    {
        if (_currentIndex >= _points.Count - 1)
            _currentIndex = -1;

        return _points[++_currentIndex];
    }

    public Transform GetPrevious()
    {
        if (_currentIndex <= 0)
            _currentIndex = _points.Count;

        return _points[--_currentIndex];
    }
}
