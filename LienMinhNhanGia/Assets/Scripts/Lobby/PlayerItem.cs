using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    [Header("UI")]
    [SerializeField] TMP_Text PlayerNameTxt;
    [SerializeField] TMP_Text PlayerLevelTxt;
    [SerializeField] Image SkillU_Image;
    [SerializeField] TMP_Text SkillU_LevelTxt;
    [SerializeField] Image SkillI_Image;
    [SerializeField] TMP_Text SkillI_LevelTxt;
    [SerializeField] Image SkillO_Image;
    [SerializeField] TMP_Text SkillO_LevelTxt;

    Player player;

    public void SetUp(Player _player)
    {
        player = _player;
        SetPLayerData(player);

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if(targetPlayer != null && targetPlayer == player)
        {
            SetPLayerData(targetPlayer);
        }
    }


    public void SetPLayerData(Player player)
    {
        PlayerNameTxt.text = player.NickName;
        // Retrieve the JSON string from the CustomProperties dictionary

        string Accountjson = (string)player.CustomProperties["Account"];
        string Account_SkillU_json = (string)player.CustomProperties["Account_SkillU"];
        string Account_SkillI_json = (string)player.CustomProperties["Account_SkillI"];
        string Account_SkillO_json = (string)player.CustomProperties["Account_SkillO"];


        // Deserialize the JSON string back to the original object type
        AccountEntity Account = JsonUtility.FromJson<AccountEntity>(Accountjson);
        AccountSkillEntity Account_SkillU = JsonUtility.FromJson<AccountSkillEntity>(Account_SkillU_json);
        AccountSkillEntity Account_SkillI = JsonUtility.FromJson<AccountSkillEntity>(Account_SkillI_json);
        AccountSkillEntity Account_SkillO = JsonUtility.FromJson<AccountSkillEntity>(Account_SkillO_json);


        PlayerLevelTxt.text = "Level " + Account.Level.ToString();
        if (Account_SkillU != null)
        {
            SkillU_Image.sprite = Resources.Load<Sprite>("Skill/" + Account_SkillU.SkillID);
            SkillU_LevelTxt.text = "Level " + Account_SkillU.CurrentLevel;
        }
        else
        {
            SkillU_Image.sprite = null;
            SkillU_LevelTxt.text = null;
        }
        if (Account_SkillI != null)
        {
            SkillI_Image.sprite = Resources.Load<Sprite>("Skill/" + Account_SkillI.SkillID);
            SkillI_LevelTxt.text = "Level " + Account_SkillI.CurrentLevel;
        }
        else
        {
            SkillI_Image.sprite = null;
            SkillI_LevelTxt.text = null;
        }
        if (Account_SkillO != null)
        {
            SkillO_Image.sprite = Resources.Load<Sprite>("Skill/" + Account_SkillO.SkillID);
            SkillO_LevelTxt.text = "Level " + Account_SkillO.CurrentLevel;
        }
        else
        {
            SkillO_Image.sprite = null;
            SkillO_LevelTxt.text = null;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }

}
