using System;
using Unity.Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCameraBase vCam;
    private float shakeTimer;

    private void Start()
    {
        vCam=GetComponent<CinemachineVirtualCameraBase>();
    }

    public void CameraShake(float intensity, float time)
    {
        var perlin=vCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin == null) return;
        
        perlin.AmplitudeGain = intensity;
        shakeTimer = time;
    }
    
    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer-=Time.deltaTime;
            if (shakeTimer <= 0)
            {
                var perlin=vCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
                if (perlin == null) return;
        
                perlin.AmplitudeGain = 0f;
            }
        }

        
    }
}
