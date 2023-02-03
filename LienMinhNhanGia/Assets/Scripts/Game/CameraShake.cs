using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Instance")]
    public static CameraShake Instance;

    [SerializeField] float duration = 1.0f;

    private void Awake()
    {
        Instance = this;
    }

    public void ExecuteShakeScreen(AnimationCurve animationCurve)
    {
        StartCoroutine(Shake(animationCurve));
    }

    IEnumerator Shake(AnimationCurve animationCurve)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strenth = animationCurve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strenth;
            yield return null;
        }

        transform.position = startPosition;
    }

}
