using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PetBagManager.EquipPet.Name);
        Instantiate(Resources.Load(PetBagManager.EquipPet.Name, typeof(GameObject)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
