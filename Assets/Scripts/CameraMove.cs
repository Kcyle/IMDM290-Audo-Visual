using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;



[RequireComponent (typeof(AudioSource))]
public class CameraMove : MonoBehaviour
{
    
    //float cameraStart, cameraBet, cameraEnd;
   // public Transform cameraBet = null;
     float lerpFraction = 0f; 
     float time = 0f;
    public SplineAnimate splineAnimate;
    public float baseSpeed = 0.05f;
    public float audioAffect = 0.1f;

    //tracks  0-1 position along the spline
    private float normalizedPosition = 0f;

    void Start()
    {
    
            splineAnimate.NormalizedTime = 0f;
     
    }

    // Update is called once per frame
    void Update()
    {
      time += Time.deltaTime * AudioSpectrum.audioAmp;
        //Debug.Log(AudioSpectrum.audioAmp);
        // Vector3.Lerp(startPosition, endPosition, lerpFraction)  
     // lerpFraction = Mathf.Sin(time) * 0.5f + 0.5f;
           
       if (splineAnimate == null) return;
       lerpFraction  = Mathf.Lerp(lerpFraction, AudioSpectrum.audioAmp, 0.1f);

        // Speed = base speed + audio amplitude scaled by influence factor
        float currentSpeed = baseSpeed + (lerpFraction * AudioSpectrum.audioAmp);
        //float currentSpeed = baseSpeed + (AudioSpectrum.audioAmp * audioAffect);


        // advance position, loop with Repeat
        normalizedPosition += currentSpeed * Time.deltaTime;
        normalizedPosition = Mathf.Repeat((normalizedPosition), 1f);

        // apply to spline
        splineAnimate.NormalizedTime = normalizedPosition;
    }
}
