using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : MonoBehaviour
{
    [Header("MISSION PANEL")]
    [SerializeField] TMP_Text MissionName;
    [SerializeField] TMP_Text MissionDescription;
    [SerializeField] TMP_Text MissionCurrent;
    [SerializeField] TMP_Text MissionTarget;
    [SerializeField] TMP_Text MissionCoinBonus;
    [SerializeField] TMP_Text MissionExperienceBonus;

    [SerializeField] GameObject DonePanel;
    [SerializeField] GameObject ClaimBtn;
    [SerializeField] GameObject InprogressPanel;

    MissionEntity missionEntity;

    public void SetUp(MissionEntity _missionEntity, int Current, int State)
    {
        missionEntity = _missionEntity;

        MissionName.text = _missionEntity.Name.ToString();
        MissionDescription.text = _missionEntity.Request.ToString();
        MissionCurrent.text = Current.ToString();
        MissionTarget.text = _missionEntity.Target.ToString();
        MissionCoinBonus.text = _missionEntity.CoinBonus.ToString();
        MissionExperienceBonus.text = _missionEntity.ExperienceBonus.ToString();

        if (Current >= _missionEntity.Target && State == 1)
        {
            DonePanel.SetActive(true);
            ClaimBtn.SetActive(false);
            InprogressPanel.SetActive(false);
        }
        else if(Current >= _missionEntity.Target && State == 0)
        {
            DonePanel.SetActive(false);
            ClaimBtn.SetActive(true);
            InprogressPanel.SetActive(false);
        }
        else
        {
            DonePanel.SetActive(false);
            ClaimBtn.SetActive(false);
            InprogressPanel.SetActive(true);
        }

    }

    public void ClaimReward()
    {
        MissionManager.Instance.ClaimRewardSelectedMission(missionEntity);
    }

}
