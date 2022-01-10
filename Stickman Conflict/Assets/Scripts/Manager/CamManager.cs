using UnityEngine;
using Cinemachine;

public class CamManager : MonoBehaviour
{
    public static CamManager Instance { get; private set; }
    [SerializeField] CinemachineStateDrivenCamera stateDrivenCamera;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float startingIntensity;
    private float shakeStartTimer, shakeTimer;

    private void Awake()
    {
        Instance = this;
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                //cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0, (1 - (shakeTimer / shakeStartTimer)));
            }
        }
    }

    public void Shake(float intensity, float time)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = startingIntensity = intensity;
        shakeStartTimer = shakeTimer = time;
    }
}