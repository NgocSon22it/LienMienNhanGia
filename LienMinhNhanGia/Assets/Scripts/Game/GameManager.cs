using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] GameObject Player;

    [Header("Spawn Player Manager")]
    [SerializeField] Transform SpawnPoint;

    [Header("CheckPoint")]
    CheckPoint checkPoint;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.U))
        {
            int a = Random.Range(0, 4);
            checkPoint = GetSaveCheckPointByID(a);
            Player.transform.position = checkPoint.transform.position;
             Debug.Log("checkpoint: " + a + "   " + checkPoint.transform.position);

        }
    }

    public CheckPoint GetSaveCheckPointByID(int CheckPointID)
    {
        CheckPoint checkPoint = null;
        CheckPoint[] allCheckPoint = GameObject.FindObjectsOfType<CheckPoint>();

        foreach (CheckPoint currentCheckPoint in allCheckPoint)
        {
            if (currentCheckPoint.GetCheckPointID() == CheckPointID)
            {
                checkPoint = currentCheckPoint;
            }
        }
        return checkPoint;
    }



    public void SpawnPlayerNPet()
    {
        GameObject Player = (GameObject)Instantiate(Resources.Load("PainChibi", typeof(GameObject)), SpawnPoint.position, SpawnPoint.rotation);
        if (PetBagManager.EquipPet != null)
        {
            GameObject Pet = (GameObject)Instantiate(Resources.Load(PetBagManager.EquipPet.Name, typeof(GameObject)), Player.transform.Find("Pet").position, Quaternion.identity);
            Pet.transform.parent = Player.transform;
            Pet.GetComponent<Pet>().SetUp(PetBagManager.EquipPet);
        }
    }
}