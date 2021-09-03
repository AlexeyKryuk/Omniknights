using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComparisonType
{
    Less = -1,
    Equal,
    More
}

public class DistanceTransition : Transition
{
    [SerializeField] private float _range;
    [SerializeField] private ComparisonType _comparisonType;

    public float Range { get => _range; private set => _range = value; }

    private void Update()
    {
        switch (_comparisonType)
        {
            case ComparisonType.Less:
                if (Vector3.Distance(transform.position, Target.transform.position) < Range)
                    NeedTransit = true;
                break;

            case ComparisonType.More:
                if (Vector3.Distance(transform.position, Target.transform.position) > Range)
                    NeedTransit = true;
                break;

            case ComparisonType.Equal:
                if (Vector3.Distance(transform.position, Target.transform.position) == Range)
                    NeedTransit = true;
                break;
        }
    }
}
