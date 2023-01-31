using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] bool start = false;
    [SerializeField] float duration = 1.0f;
    [SerializeField] AnimationCurve Curve;

    // Update is called once per frame
    void Update()
    {
        if(start) 
        {
            start = false;
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime+= Time.deltaTime;
            float strenth = Curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strenth;
            yield return null;
        }
        transform.position = startPosition;
    }

}
