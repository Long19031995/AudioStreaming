using System;
using System.Collections;
using System.Collections.Generic;
using Unity.RenderStreaming;
using UnityEngine;
using UnityEngine.UI;

public class UIRhythm : MonoBehaviour
{
    public SpectrumData spectrumData;
    public Transform samplePrefab;
    public List<Transform> samples;
    public int count;
    public Button buttonConnect;
    public Button buttonDisconnect;
    public SingleConnection singleConnection;
    public SignalingManager signalingManager;
    public string connectionId;

    private void Start()
    {
        signalingManager.Run();
    }

    private void Awake()
    {
        buttonConnect.onClick.AddListener(Connect);
        buttonDisconnect.onClick.AddListener(Disconnect);
        count = (int)spectrumData.NumberSample;
        for (int i = 0; i < count; i++)
        {
            var sample = Instantiate(samplePrefab, transform);
            samples.Add(sample);
            if (i % 2 == 0)
            {
                sample.position = samplePrefab.position + i * 0.01f * Vector3.left;
            }
            else
            {
                sample.position = samplePrefab.position + (i - 1) * 0.01f * Vector3.right;
            }
        }
        samplePrefab.gameObject.SetActive(false);
    }

    public void Connect()
    {
        connectionId = Guid.NewGuid().ToString();
        singleConnection.CreateConnection(connectionId);
        buttonConnect.gameObject.SetActive(false);
        buttonDisconnect.gameObject.SetActive(true);
    }

    public void Disconnect()
    {
        singleConnection.DeleteConnection(connectionId);
        buttonConnect.gameObject.SetActive(true);
        buttonDisconnect.gameObject.SetActive(false);
    }

    private void Update()
    {
        for (int i = 0; i < count; i++)
        {
            samples[i].localScale = Vector3.one * (spectrumData.Left[i] + spectrumData.Right[i]) / 2 * 100;
        }
    }
}
