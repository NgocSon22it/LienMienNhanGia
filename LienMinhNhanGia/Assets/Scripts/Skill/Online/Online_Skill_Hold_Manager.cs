using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Online_Skill_Hold_Manager : MonoBehaviour
{
    public static Online_Skill_Hold_Manager Instance;

    [SerializeField] PhotonView PV;

    AccountSkillEntity AccountSkill_U;
    AccountSkillEntity AccountSkill_I;
    AccountSkillEntity AccountSkill_O;

    SkillEntity Skill;

    [Header("DAO Manager")]
    [SerializeField] GameObject DAOManager;

    OnlinePlayer localPlayer;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        OnlineSetUpListSkill();
        localPlayer = OnlinePlayer.LocalPlayerInstance;
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            OnlineExecuteSkill();
        }
    }

    public void WaterBall()
    {
        GameObject waterball = Skill_Pool.Instance.GetWaterBallFromPool();

        if (waterball != null)
        {
            waterball.transform.position = localPlayer.Place_ExecuteSkill.position;
            waterball.transform.rotation = localPlayer.Place_ExecuteSkill.rotation;
            waterball.SetActive(true);
        }


    }

    public void WaterSword()
    {
        Debug.Log("WaterSword");
    }
    public void WaterDragon()
    {
        Debug.Log("WaterDragon");
    }

    public void WaterShark()
    {
        Debug.Log("WateShark");
    }

    public void OnlineCallMethodName(string MethodName)
    {
        PV.RPC(nameof(CallMethodFromHold), RpcTarget.AllBuffered, MethodName);
    }

    [PunRPC]
    public void CallMethodFromHold(string MethodName)
    {
        Invoke(MethodName, 0f);
    }


    public void OnlineSetUpListSkill()
    {
        AccountSkill_U = DAOManager.GetComponent<Account_SkillDAO>().GetAccountSkillbySlotIndex(AccountManager.AccountID, 1);
        AccountSkill_I = DAOManager.GetComponent<Account_SkillDAO>().GetAccountSkillbySlotIndex(AccountManager.AccountID, 2);
        AccountSkill_O = DAOManager.GetComponent<Account_SkillDAO>().GetAccountSkillbySlotIndex(AccountManager.AccountID, 3);
    }

    public void OnlineControlSkill(KeyCode key, AccountSkillEntity accountSkillEntity)
    {
        if (Input.GetKeyDown(key) && accountSkillEntity != null)
        {
            Skill = DAOManager.GetComponent<SkillDAO>().GetSkillbyID(accountSkillEntity.SkillID);
            OnlineCallMethodName(Skill.MethodName);
        }
    }

    public void OnlineExecuteSkill()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            OnlineControlSkill(KeyCode.U, AccountSkill_U);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            OnlineControlSkill(KeyCode.I, AccountSkill_I);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            OnlineControlSkill(KeyCode.O, AccountSkill_O);
        }
    }
}
