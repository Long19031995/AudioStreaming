using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UILinkYoutube : MonoBehaviour
{
    public Button buttonPlay;
    public Button buttonStop;
    public TMP_InputField inputField;
    public YoutubePlayer.YoutubePlayer youtubePlayer;
    public VideoPlayer videoPlayer;

    private void Start()
    {
        buttonPlay.onClick.AddListener(Play);
        buttonStop.onClick.AddListener(Stop);
    }

    public async void Play()
    {
        buttonPlay.interactable = false;
        inputField.interactable = false;
        try
        {
            await youtubePlayer.PlayVideoAsync(inputField.text);
            buttonPlay.gameObject.SetActive(false);
            inputField.gameObject.SetActive(false);

            buttonStop.gameObject.SetActive(true);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            buttonPlay.interactable = true;
            inputField.interactable = true;
        }
    }

    public void Stop()
    {
        videoPlayer.Stop();

        buttonPlay.gameObject.SetActive(true);
        inputField.gameObject.SetActive(true);

        buttonPlay.interactable = true;
        inputField.interactable = true;

        buttonStop.gameObject.SetActive(false);
    }
}
