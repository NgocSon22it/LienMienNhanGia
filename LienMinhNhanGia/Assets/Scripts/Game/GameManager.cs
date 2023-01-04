using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {

        GameObject Player = (GameObject)Instantiate(Resources.Load("PainChibi", typeof(GameObject)), SpawnPoint.position, SpawnPoint.rotation);
        if (PetBagManager.EquipPet != null)
        {
            GameObject Pet = (GameObject)Instantiate(Resources.Load(PetBagManager.EquipPet.Name, typeof(GameObject)), Player.transform.Find("Pet").position, Quaternion.identity);
            Pet.transform.parent = Player.transform;
            Pet.GetComponent<Pet>().SetUp(PetBagManager.EquipPet);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
