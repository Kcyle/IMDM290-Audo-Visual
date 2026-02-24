using UnityEngine;
using System.Collections.Generic;

public class FireTextFormation : MonoBehaviour
{
    public GameObject fireParticlePrefab;
    float formDuration = 0.5f, holdDuration = 1f, textScale = 1f;
    public string displayText = "FIRE FORCE";

    float ignitionTime;

    GameObject[] orbs;
    Vector3[] targets, starts;
    int count;
    bool done;
    float startTime;

    public bool IsFinished => done;

    static readonly Dictionary<char, byte[,]> Font = new()
    {
        {'F', new byte[,]{{1,1,1},{1,0,0},{1,1,0},{1,0,0},{1,0,0}}},
        {'I', new byte[,]{{1,1,1},{0,1,0},{0,1,0},{0,1,0},{1,1,1}}},
        {'R', new byte[,]{{1,1,0},{1,0,1},{1,1,0},{1,0,1},{1,0,1}}},
        {'E', new byte[,]{{1,1,1},{1,0,0},{1,1,0},{1,0,0},{1,1,1}}},
        {'O', new byte[,]{{0,1,0},{1,0,1},{1,0,1},{1,0,1},{0,1,0}}},
        {'C', new byte[,]{{0,1,1},{1,0,0},{1,0,0},{1,0,0},{0,1,1}}},
        {' ', new byte[,]{{0,0},{0,0},{0,0},{0,0},{0,0}}},
    };

    List<Vector3> GenPositions()
    {
        var pos = new List<Vector3>();
        int totalCols = 0;
        foreach (char c in displayText.ToUpper())
            if (Font.ContainsKey(c)) totalCols += Font[c].GetLength(1) + 1;
        totalCols--;
        float ox = -totalCols * textScale * 0.5f, oy = -5 * textScale * 0.5f;
        int col = 0;
        foreach (char c in displayText.ToUpper())
        {
            if (!Font.ContainsKey(c)) continue;
            var g = Font[c]; int rows = g.GetLength(0), cols = g.GetLength(1);
            for (int r = 0; r < rows; r++)
                for (int cc = 0; cc < cols; cc++)
                    if (g[r, cc] == 1)
                        pos.Add(new Vector3((col + cc) * textScale + ox, (rows - 1 - r) * textScale + oy + 3f, 0f));
            col += cols + 1;
        }
        return pos;
    }

    void Start()
    {
        ignitionTime = formDuration + holdDuration;
        var positions = GenPositions();
        count = positions.Count;
        orbs = new GameObject[count];
        targets = positions.ToArray();
        starts = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            starts[i] = new Vector3(Random.Range(-25f, 25f), Random.Range(-8f, 15f), Random.Range(-15f, 15f));
            orbs[i] = Instantiate(fireParticlePrefab);
            orbs[i].transform.position = starts[i];
            orbs[i].transform.localScale = Vector3.one * textScale * 0.4f;
        }

        startTime = Time.time;
    }

    void Update()
    {
        UpdateText(Time.time - startTime);
    }

    public void UpdateText(float time)
    {
        if (done) return;
        float t = Mathf.SmoothStep(0, 1, Mathf.Clamp01(time / formDuration));

        if (time < formDuration)
        {
            for (int i = 0; i < count; i++)
                orbs[i].transform.position = Vector3.Lerp(starts[i], targets[i], t);
        }
        else if (time >= ignitionTime && !done)
        {
            done = true;
            for (int i = 0; i < count; i++)
                Destroy(orbs[i]);
        }
    }
}