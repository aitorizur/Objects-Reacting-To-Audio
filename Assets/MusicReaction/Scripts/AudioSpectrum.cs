using System.Linq;
using UnityEngine;


[RequireComponent(typeof (AudioSource))]
public class AudioSpectrum : MonoBehaviour
{

    AudioSource audioSource;

    public static float[] samples = new float[512];
    public static float[] freqBands = new float[8];
    public static float[] freqBandBuffers = new float[8];
    float[] freqBandsbufferDecrease = new float[8];

    [SerializeField] float[] freqBandHighest = new float[8];
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];

    public static float amplitude;
    public static float amplitudeBuffer;
    
    [SerializeField] float amplitudeHighest;

    [SerializeField] private float bufferDecreasePercentageMultiplierPerSecond = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        SetFreqBandsBufferDecrease();
        GetSpectrumAudioSource();
        PrepareFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    private void GetAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;

        for (int i = 0; i < 8; i++)
        {
            currentAmplitude += audioBand[i];
            currentAmplitudeBuffer += audioBandBuffer[i];
        }

        if (currentAmplitude > amplitudeHighest)
        {
            amplitudeHighest = currentAmplitude;
        }

        amplitude = currentAmplitude / amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
    }

    private void SetFreqBandsBufferDecrease()
    {
        for (int i = 0; i < 8; i++)
        {
            freqBandsbufferDecrease[i] = bufferDecreasePercentageMultiplierPerSecond * freqBandHighest[i] * Time.deltaTime;
        }
    }

    private void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBands[i] > freqBandHighest[i])
            {
                freqBandHighest[i] = freqBands[i];
            }

            audioBand[i] = freqBands[i] / freqBandHighest[i];
            audioBandBuffer[i] = freqBandBuffers[i] / freqBandHighest[i];
        }
    }

    private void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData( samples, 0, FFTWindow.Blackman);
    }

    private void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBands[i] > freqBandBuffers[i])
            {
                freqBandBuffers[i] = freqBands[i];
            }
            else if (freqBands[i] <= freqBandBuffers[i])
            {
                freqBandBuffers[i] -= freqBandsbufferDecrease[i];
                if (freqBands[i] > freqBandBuffers[i])
                {
                    freqBandBuffers[i] = freqBands[i];
                }
            }
        }
    }

    private void PrepareFrequencyBands()
    {

        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int ii = 0; ii < sampleCount; ii++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBands[i] = average * 10;
        }

    }
}
