using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public RawImage videoImage;
    public VideoPlayer videoPlayer;
    public TextMeshProUGUI buttonText;

    public void PlayStopVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoImage.enabled = false;
            videoPlayer.Stop();
            buttonText.text = "Play";
        }
        else
        {
            videoImage.enabled = true;
            videoPlayer.Play();
            buttonText.text = "Stop";
        }
    }
}
