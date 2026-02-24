using UnityEngine;

public class AudioAnalyzer : MonoBehaviour
{
    public AudioSource audioSource;
    public float bass, mid, high;
    public float[] bands = new float[8];
    float[] spectrum = new float[256];

    void Start()
    {
        if (!audioSource) audioSource = GetComponent<AudioSource>();
        if (audioSource && !audioSource.isPlaying) audioSource.Play();
    }

    void Update()
    {
        Analyze();
    }

    public void Analyze()
    {
        if (!audioSource || !audioSource.isPlaying)
        {
            bass = Mathf.Lerp(bass, 0, 5f * Time.deltaTime);
            mid = Mathf.Lerp(mid, 0, 5f * Time.deltaTime);
            high = Mathf.Lerp(high, 0, 5f * Time.deltaTime);
            return;
        }
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        int[] b = { 2, 4, 8, 16, 32, 64, 128, 256 };
        int idx = 0;
        for (int i = 0; i < 8; i++)
        {
            float avg = 0; int cnt = b[i];
            for (int s = 0; s < cnt; s++) { if (idx < 256) { avg += spectrum[idx]; idx++; } }
            bands[i] = avg / cnt;
        }
        float sm = 10f * Time.deltaTime;
        bass = Mathf.Lerp(bass, (bands[0] + bands[1]) * 12f, sm);
        mid = Mathf.Lerp(mid, (bands[3] + bands[4]) * 18f, sm);
        high = Mathf.Lerp(high, (bands[6] + bands[7]) * 22f, sm);
    }

    public float GetTime()
    {
        if (audioSource && audioSource.isPlaying)
            return audioSource.time;
        return Time.time;
    }
}