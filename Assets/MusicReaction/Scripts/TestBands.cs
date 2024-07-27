using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBands : MonoBehaviour
{
    public int band;
    public float startScale;
    public float maxScale;

    public bool useBuffers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (useBuffers)
        {
            transform.localScale = new Vector3(transform.localScale.x, startScale + (AudioSpectrum.audioBandBuffer[band] * maxScale), transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, startScale + (AudioSpectrum.audioBand[band] * maxScale), transform.localScale.z);
        }
    }
}
