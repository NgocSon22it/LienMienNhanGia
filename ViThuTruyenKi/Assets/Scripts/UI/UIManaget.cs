using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManaget : MonoBehaviour
{
    [SerializeField] GameObject Shop;
    [SerializeField] GameObject PetBag;

    public void OpenShop()
    {
        Shop.SetActive(true);
    }
}
