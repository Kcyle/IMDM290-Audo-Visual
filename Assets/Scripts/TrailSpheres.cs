using UnityEngine;

public class TrailSpheres : MonoBehaviour
{
    public GameObject objectToDestroy;

    GameObject[] trailSpheres;
    Material[] tMats;
    TrailRenderer[] tTrails;
    bool created, destroyed;
    AudioAnalyzer audio;
    FireTextFormation fireText;
    int numTrailSpheres = 20;
    float arenaRadius = 14f;

    void Start()
    {
        audio = FindObjectOfType<AudioAnalyzer>();
        fireText = FindObjectOfType<FireTextFormation>();
    }

    void Update()
    {
        if (!created)
        {
            if (fireText == null || fireText.IsFinished)
            {
                CreateTrailSpheres();
                created = true;
            }
            return;
        }

        float songTime = audio.GetTime();

   
        if (!destroyed && songTime >= 56f)
        {
            if (objectToDestroy != null)
                Destroy(objectToDestroy);
            destroyed = true;
        }
        float boost = 1f;
        if (songTime >= 56f && songTime <= 70f)
        {
            float t01 = Mathf.InverseLerp(56f, 63f, songTime);
            float t10 = Mathf.InverseLerp(70f, 63f, songTime);
            boost = 1f + 3f * Mathf.Min(t01, t10); 
        }

        float t = songTime;
        float bass = audio.bass * boost;
        float mid = audio.mid * boost;
        float high = audio.high * boost;

        for (int i = 0; i < numTrailSpheres; i++)
        {
            float a = (float)i / numTrailSpheres * Mathf.PI * 2f;
            float spd = 2f + i * 0.3f;
            float r = arenaRadius * (0.5f + 0.5f * Mathf.Sin(t * 0.5f + i)) * (1f + bass);
            float h = Mathf.PingPong(t * spd * 0.5f + i * 2f, 15f) - 2f;

            trailSpheres[i].transform.position = new Vector3(
                Mathf.Cos(t * spd * 2f + a) * r, h, Mathf.Sin(t * spd * 2f + a) * r);

            Color tc = Color.Lerp(new Color(1, .5f, 0), new Color(1, 1, .7f), bass);
            tMats[i].SetColor("_EmissionColor", tc * (2f + bass * 3f));
            tTrails[i].startColor = new Color(tc.r, tc.g, tc.b, 0.8f);
            tTrails[i].endColor = new Color(tc.r, tc.g * 0.3f, 0, 0);

            var tl = trailSpheres[i].GetComponent<Light>();
            if (tl) { tl.intensity = 3f + bass * 2f; tl.color = tc; }
        }
    }

    void CreateTrailSpheres()
    {
        trailSpheres = new GameObject[numTrailSpheres];
        tMats = new Material[numTrailSpheres];
        tTrails = new TrailRenderer[numTrailSpheres];

        for (int i = 0; i < numTrailSpheres; i++)
        {
            trailSpheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            trailSpheres[i].transform.localScale = Vector3.one * 0.15f;
            Destroy(trailSpheres[i].GetComponent<Collider>());

            tMats[i] = new Material(Shader.Find("Standard"));
            tMats[i].EnableKeyword("_EMISSION");
            trailSpheres[i].GetComponent<Renderer>().material = tMats[i];

            tTrails[i] = trailSpheres[i].AddComponent<TrailRenderer>();
            tTrails[i].time = 1.2f; tTrails[i].startWidth = 0.3f; tTrails[i].endWidth = 0f;
            tTrails[i].material = new Material(Shader.Find("Sprites/Default"));
            tTrails[i].minVertexDistance = 0.05f; tTrails[i].emitting = true;

            var tl = trailSpheres[i].AddComponent<Light>();
            tl.type = LightType.Point; tl.color = new Color(1, .4f, .05f); tl.range = 5f;
        }
    }
}