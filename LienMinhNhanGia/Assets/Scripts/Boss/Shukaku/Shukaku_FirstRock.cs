using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shukaku_FirstRock : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(TurnOff), 1.5f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
