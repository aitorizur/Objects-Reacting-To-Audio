using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAmplitudeHeight : MonoBehaviour
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
        transform.localScale = new Vector3( transform.localScale.x, startScale + (maxScale * AudioSpectrum.amplitudeBuffer), transform.localScale.z);
    }
}
