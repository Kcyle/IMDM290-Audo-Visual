using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(AudioSource))]
public class CameraMove : MonoBehaviour
{
    
    //float cameraStart, cameraBet, cameraEnd;
    public Transform cameraBet = null;
     float lerpFraction; 
     float time = 0f;
    AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       time += Time.deltaTime * AudioSpectrum.audioAmp;
        //Debug.Log(AudioSpectrum.audioAmp);
        // what to update over time?
            // Lerp : Linearly interpolates between two points.
            // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Vector3.Lerp.html
            // Vector3.Lerp(startPosition, endPosition, lerpFraction)  
            lerpFraction = Mathf.Sin(time) * 0.5f + 0.5f;
            if (transform.position.z != 0)
            // lerpFraction variable defines the point between startPosition and endPosition (0~1)
          
             transform.position = Vector3.Lerp(transform.position,cameraBet.position,lerpFraction);  
            // Lerp logic. Update position       
           // t = i* 2 * Mathf.PI / numSphere;
    //        transform.position = Vector3.Lerp(startPosition[i], endPosition[i], lerpFraction);
    //         float scale = 1f + AudioSpectrum.audioAmp;
    //         spheres[i].transform.localScale = new Vector3(scale, 1f, 1f);
    //         spheres[i].transform.Rotate(AudioSpectrum.audioAmp, 1f, 1f);
    }
}
