using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Subscribe to the videoPlayer's loopPointReached event
        videoPlayer.loopPointReached += OnVideoEnd;

        // Play the video
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // This method will be called when the video reaches the end

        // Load the next scene
        SceneManager.LoadScene("Menu");
    }
}