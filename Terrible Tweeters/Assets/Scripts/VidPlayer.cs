using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    [SerializeField] VideoPlayer _player;
    //[SerializeField] string videoFileName;
    // Start is called before the first frame update
    //void Start()
    //{
    //PlayVideo();
    //}

    //public void PlayVideo()
    //{
    //VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

    //if (videoPlayer)
    //{
    //string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
    //videoPlayer.url = videoPath;
    //videoPlayer.Play();
    //}
    //}

    private void Start()
    {
        _player.loopPointReached += VideoDone;
    }

    void VideoDone(VideoPlayer player)
    {
        Debug.Log("Video Finished");
        SceneManager.LoadScene("Level1");
    }
}
