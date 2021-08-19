using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTextRenderer : MonoBehaviour
{
    private void Update()
    {
        if (Camera.main != null)
            transform.rotation = Camera.main.transform.rotation;
    }
}
