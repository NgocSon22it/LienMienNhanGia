using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Realtime;

public class CameraManager : MonoBehaviour
{

    CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin channelPerlin;

    public static CameraManager Instance;
    bool Isshaking;
    float ElapsedTime = 0f;
    float DurationTime;

    private void Awake()
    {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        channelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void StartShakeScreen(int AmplitudeGain, int FrequencyGain, float Duration)
    {
        channelPerlin.m_AmplitudeGain = AmplitudeGain;
        channelPerlin.m_FrequencyGain = FrequencyGain;
        Isshaking = true;
        ElapsedTime = 0f;
        DurationTime = Duration;
    }

    public void StopShakeScreen()
    {
        Isshaking = false;
        channelPerlin.m_AmplitudeGain = 0;
        channelPerlin.m_FrequencyGain = 0;
        ElapsedTime= 0f;
    }

    private void Update()
    {
        if(Isshaking)
        {
            ElapsedTime += Time.deltaTime;
            if(ElapsedTime > DurationTime)
            {
                StopShakeScreen();
            }
        }
    }
    
}
