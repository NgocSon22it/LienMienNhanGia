using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] GameObject NotClaimBtn;
    [SerializeField] GameObject ClaimBtn;
    [SerializeField] GameObject Done;

    [SerializeField] Image CurrentProgress;

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

        CurrentProgress.fillAmount = (float)Current / (float)_missionEntity.Target;

        if (Current >= _missionEntity.Target && State == 0)
        {
            NotClaimBtn.SetActive(false);
            ClaimBtn.SetActive(true);
            Done.SetActive(false);
        }
        else if(Current >= _missionEntity.Target && State == 1)
        {
            NotClaimBtn.SetActive(false);
            ClaimBtn.SetActive(false);
            Done.SetActive(true);
        }
        else
        {
            NotClaimBtn.SetActive(true);
            ClaimBtn.SetActive(false);
            Done.SetActive(false);
        }


    }

    public void ClaimReward()
    {
        MissionManager.Instance.ClaimRewardSelectedMission(missionEntity);
    }

}
