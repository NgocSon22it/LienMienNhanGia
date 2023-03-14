using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shukaku_BeastBombExplosion : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(TurnOff), 2f);
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
