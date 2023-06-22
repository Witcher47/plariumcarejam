using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WindowsManager : MonoBehaviour
{
  public GameObject Intro;

  void Start()
  {
    Intro.active = true;
    var introVideo = Intro.GetComponentInChildren<VideoPlayer>();
    introVideo.Play();
    introVideo.loopPointReached += ShowLevel;
  }

  public void ShowLevel(VideoPlayer vp)
  {
    vp.Stop();
    Intro.active = false;
    //PreloaderAnimator.Instance.Play("Start_Level");
    //PreloaderAnimator.Instance.Play("Game_Over2");
    PreloaderAnimator.Instance.Play("Start_Level2");
  }

  public void ToNextState()
  {
    
    
  }

  public void ShowEnd()
  {


  }
}
