using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WindowsManager : MonoBehaviour
{
  public GameObject Intro;
  public GameObject Level1;

  void Start()
  {
    Intro.active = true;
    Level1.active = false;
    var introVideo = Intro.GetComponentInChildren<VideoPlayer>();
    introVideo.Play();
    introVideo.loopPointReached += ShowLevel;
  }

  public void ShowLevel(VideoPlayer vp)
  {
    vp.Stop();
    //Level1.active = true;
    //PreloaderAnimator.Instance.Play("Start_Level");
    //PreloaderAnimator.Instance.Play("Game_Over2");

    PreloaderAnimator.Instance.Play("Game_Over2");
    //PreloaderAnimator.Instance.Play("Start_Level2");

    Intro.active = false;
  }

  public void ToNextState()
  {

    PreloaderAnimator.Instance.Play("Start_Level2");
    Level1.active = true;

  }

  public void ShowEnd()
  {
    PreloaderAnimator.Instance.Play("Start_Level2");
    Level1.active = true;
  }
}
