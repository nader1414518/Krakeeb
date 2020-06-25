using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingIconController : MonoBehaviour
{
    RectTransform rect;
    public float rotationSpeed = 2.5f;

    void OnEnable()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (rect)
        {
            rect.localEulerAngles = new Vector3(rect.localEulerAngles.x, rect.localEulerAngles.y, rect.localEulerAngles.z - rotationSpeed);
        }
    }
}
