using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Online_GameManager : MonoBehaviour
{
    [SerializeField] Transform SpawnPoint;
    [SerializeField] GameObject Character;
    // Start is called before the first frame update
    void Start()
    {
        
        PhotonNetwork.Instantiate(Path.Combine("Character/Online/", Character.name), SpawnPoint.position , SpawnPoint.rotation);
    }
}
