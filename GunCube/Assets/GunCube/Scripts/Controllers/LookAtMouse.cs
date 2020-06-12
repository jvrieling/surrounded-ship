using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) * -1;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
