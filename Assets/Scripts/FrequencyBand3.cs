using UnityEngine;


[RequireComponent(typeof(SpectrumData))]
public class FrequencyBand3 : MonoBehaviour
{
    public float AverageBass;
    public float AverageMidrange;
    public float AverageTreble;


    int minBass;
    int maxBass;
    int minMidrange;
    int maxMidrange;
    int minTreble;
    int maxTreble;
    SpectrumData spectrumData;
    NumberSample numberSample;


    private void Awake()
    {
        spectrumData = GetComponent<SpectrumData>();
        GetMinMaxBand();
    }


    public void GetMinMaxBand()
    {
        int number = (int)numberSample;
        GetMinMaxBass(number);
        GetMinMaxMidrange(number);
        GetMinMaxTreble(number);
    }


    public void GetMinMaxBass(int number)
    {
        minBass = 0;
        maxBass = (int)(number * 1.13f / 100);
    }


    public void GetMinMaxMidrange(int number)
    {
        minMidrange = (int)(number * 1.13f / 100);
        maxMidrange = (int)(number * 18.19f / 100);
    }


    public void GetMinMaxTreble(int number)
    {
        minTreble = (int)(number * 18.19f / 100);
        maxTreble = number;
    }


    private void Update()
    {
        if (numberSample != spectrumData.NumberSample)
        {
            numberSample = spectrumData.NumberSample;
            GetMinMaxBand();
        }
        GetAverage();
    }


    public void GetAverage()
    {
        AverageBass = GetAverageInRange(minBass, maxBass);
        AverageMidrange = GetAverageInRange(minMidrange, maxMidrange);
        AverageTreble = GetAverageInRange(minTreble, maxTreble);
    }


    public float GetAverageInRange(int min, int max)
    {
        float average = 0;
        for (int i = min; i < max; i++)
        {
            average += Mathf.Max(spectrumData.Left[i], spectrumData.Right[i]);
        }
        average = Mathf.Log10(average / (max - min) * 1000000000);
        return average;
    }
}
