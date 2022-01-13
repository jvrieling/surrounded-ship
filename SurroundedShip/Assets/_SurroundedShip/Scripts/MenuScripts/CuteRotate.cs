using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteRotate : MonoBehaviour
{
    public float rotateAmount = 3;
    public float delayBetweenRotates = 0.4f;

    void OnEnable()
    {
        StartCoroutine(RoationRoutine());
    }

    private IEnumerator RoationRoutine()
    {
        while (gameObject.activeSelf)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotateAmount);
            yield return new WaitForSeconds(delayBetweenRotates);

            //transform.rotation = initialRotation * Quaternion.AngleAxis(-rotateAmount, transform.forward);
            transform.rotation = Quaternion.Euler(0, 0, -rotateAmount);
            yield return new WaitForSeconds(delayBetweenRotates);
        }
    }
}