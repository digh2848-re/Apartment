using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform shakeCamera;
    public bool shakeRotate = false;

    private Vector3 originPos;
    private Quaternion originRot;


    // Start is called before the first frame update
    void Start()
    {
        originPos = shakeCamera.localPosition;
        originRot= shakeCamera.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShakeCamera(float duration=30f,
                                   float magnitudePos=0.3f,
                                   float magnitudeRot = 1f)
    {
        float passTime = 0.0f;

        while(passTime < duration)
        {
            Vector3 shakePos = Random.insideUnitSphere;
            shakeCamera.localPosition = shakePos * magnitudePos;

            if (shakeRotate)
            {
                Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(Time.time * magnitudeRot, 0.0f));

                shakeCamera.localRotation = Quaternion.Euler(shakeRot);

            }

            passTime += Time.deltaTime;

            yield return null;
        }

        shakeCamera.localPosition = originPos;
        shakeCamera.localRotation = originRot;
    }
}
