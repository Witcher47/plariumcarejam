using UnityEngine;
using UnityEngine.Video;

namespace Assets.Scripts
{
  public class WindowsManager : MonoBehaviour
  {
    public GameObject Intro;
    public GameObject Logo;
    public GameObject Level1;
    public GameObject Victory;
    public GameObject GameOver;

    private int currentLevel = 0;

    void Start()
    {
      Victory.GetComponentInChildren<ButtonExitScript>().ActionDelegate += ExitGame;
      Victory.GetComponentInChildren<ButtonRestartScript>().ActionDelegate += Restart;
      GameOver.GetComponentInChildren<ButtonExitScript>().ActionDelegate += ExitGame;
      GameOver.GetComponentInChildren<ButtonRestartScript>().ActionDelegate += Restart;

      Level1.active = false;
      Victory.active = false;
      GameOver.active = false;

      Intro.active = true;
      var introVideo = Intro.GetComponentInChildren<VideoPlayer>();
      introVideo.Play();
      introVideo.loopPointReached += ShowLogo;
    }

    public void ShowLogo(VideoPlayer vp)
    {
      vp.Stop();
      //Level1.active = true;
      PreloaderAnimator.Instance.Play("Start_Level2");
      //PreloaderAnimator.Instance.Play("Game_Over2");
      Level1.active = true;
      //vp.Stop();
      Intro.active = false;
    }

    public void ShowLevel()
    { 
      currentLevel = 1;
    }

    public void ShowGameOver()
    { 
    }

    public void ShowVictory()
    {
      currentLevel = 0;
    }

    public void Restart()
    {
      switch (currentLevel)
      {
        case 1:
          {
            ShowLevel();
            break;
          }
        default:
          {
            //ShowLogo();
            break;
          }
      }
    }

    public void ExitGame()
    {
      Application.Quit();
    }
  }
}
