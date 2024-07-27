using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAmplitude : MonoBehaviour
{
    public float startScale;
    public float maxScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(startScale + (maxScale * AudioSpectrum.amplitudeBuffer), startScale + (maxScale * AudioSpectrum.amplitudeBuffer), startScale + (maxScale * AudioSpectrum.amplitudeBuffer));
    }
}
