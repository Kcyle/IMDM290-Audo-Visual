using UnityEngine;

public class CameraDirector : MonoBehaviour
{
    public FireTextFormation fireText;
    int phase;
    float t;

    void Start() => transform.position = new Vector3(0, 0, -71.6f);

    void Update()
    {
        t += Time.deltaTime * 0.7f;
        if (phase == 0)
        {
            transform.position = Vector3.Lerp(new Vector3(0, 0, -71.6f), new Vector3(0, 0, -25), Mathf.SmoothStep(0, 1, t));
            if (t >= 1) { phase = 1; t = 0; }
        }
        else if (phase == 1 && (fireText == null || fireText.IsFinished))
        {
            phase = 2; t = 0;
        }
        else if (phase == 2)
        {
            float s = Mathf.SmoothStep(0, 1, t);
            transform.position = Vector3.Lerp(new Vector3(0, 0, -25), new Vector3(0, 80, -70), s);
            transform.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(40, 0, 0), s);
            if (t >= 1) phase = 3;
        }
    }
}