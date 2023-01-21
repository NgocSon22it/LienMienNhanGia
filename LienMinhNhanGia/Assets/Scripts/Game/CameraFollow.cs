using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform Player;
    [SerializeField] Vector3 Offset;
    [SerializeField] float MinHorizontal, MaxHorizontal;
    [SerializeField] float MinVertical, MaxVertical;

    [Range(0f, 1f)]
    [SerializeField] float SmoothTime;
    [SerializeField] float speed;

    Vector3 TargetPosition;
    Vector3 Velocity = Vector3.zero;

    bool NeedLimitation;

    public static CameraFollow Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        TargetPosition = Player.position + Offset;
        TargetPosition = new Vector3(Mathf.Clamp(TargetPosition.x, MinHorizontal, MaxHorizontal), Mathf.Clamp(TargetPosition.y, MinVertical, MaxVertical), -10);
        transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref Velocity, SmoothTime);
    }

    public void SetLimitationCamera(float Xmin, float Xmax, float Ymin, float Ymax)
    {
        MinHorizontal = Mathf.Lerp(MinHorizontal, Xmin, speed * Time.deltaTime);
        MaxHorizontal = Mathf.Lerp(MaxHorizontal, Xmax, speed * Time.deltaTime);
        MinVertical = Mathf.Lerp(MinVertical, Ymin, speed * Time.deltaTime);
        MaxVertical = Mathf.Lerp(MaxVertical, Ymax, speed * Time.deltaTime);
        
    }

    public void ToggleCameraStatus(bool Status)
    {
        NeedLimitation = Status;
    }

}
