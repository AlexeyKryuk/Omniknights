using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTextRenderer : MonoBehaviour
{
    private void Update()
    {
        if (Camera.main != null)
            transform.rotation = Camera.main.transform.rotation;
    }
}
