using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineWaterSlashExplosion : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(TurnOff), 1f);
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
