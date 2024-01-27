using UnityEngine;
using UnityEngine.Video;
using System;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public float startFromSecond = 3f;

    // Event to be invoked when the video ends
    public event Action CutsceneEnded;

    private bool isPlaying = false;

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;

        // Listen to the "StartCutscene" event
        EventManager.StartCutsceneEvent.AddListener(StartCutscene);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        EventManager.StartCutsceneEvent.RemoveListener(StartCutscene);
    }

    public void StartCutscene()
    {
        // Start the video playback when the "StartCutscene" event is triggered
        PlayVideo();
    }

    public void PlayVideo()
    {
        if (!isPlaying)
        {
            videoPlayer.time = startFromSecond;
            videoPlayer.Play();

            isPlaying = true;
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Video has ended, invoke the event
        if (CutsceneEnded != null)
        {
            CutsceneEnded.Invoke();
        }

        // Reset the flag to allow replaying
        isPlaying = false;
    }
}
