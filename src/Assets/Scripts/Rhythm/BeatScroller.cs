using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    private float tempo;
    // Start is called before the first frame update
    void Start()
    {
        tempo = RhythmManager.rManager.GetTempo();
    }

    // Update is called once per frame
    void Update()
    {
        if (RhythmManager.rManager.hasStarted)
        {
            transform.position += new Vector3((tempo * Time.deltaTime), 0f, 0f);
        }
    }
}
