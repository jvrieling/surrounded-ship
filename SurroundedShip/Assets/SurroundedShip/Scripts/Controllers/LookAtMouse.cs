///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A quick script to make the object point at where the mouse (or finger touch) is.
/// </summary>
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
