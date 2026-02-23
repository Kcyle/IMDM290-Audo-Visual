using UnityEngine;

public class CircleAround : MonoBehaviour
{
    public Transform target;
    //public float speed = 25f;
    public float radius = 3f;
    public float speed = 1f + AudioSpectrum.audioAmp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    //     speed += transform.localScale = new Vector3(scale, 1f, 1f);

    //   float lerpFraction  = Mathf.Lerp(lerpFraction, AudioSpectrum.audioAmp, 0.1f);

    //     // Speed = base speed + audio amplitude scaled by influence factor
    //     float currentSpeed = baseSpeed + (lerpFraction * AudioSpectrum.audioAmp);

        transform.RotateAround(target.position, Vector3.up,speed * Time.deltaTime);
        Vector3 offset = transform.position - target.position;
        Vector3 updatedPos = Vector3.ClampMagnitude(offset, radius);
        transform.position = target.position + updatedPos;
    }
}
