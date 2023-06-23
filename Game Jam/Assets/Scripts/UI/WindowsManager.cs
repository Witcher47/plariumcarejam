using Game;
using UnityEngine;
using UnityEngine.Video;

namespace Assets.Scripts
{
  public class WindowsManager : MonoBehaviour
  {
    public GameObject Intro;
    public GameObject Logo;
    public GameObject Victory;
    public GameObject GameOver;
    public GameObject CommentsText;

    private int currentLevel = 0;

    void Start()
    {
      Victory.GetComponentInChildren<ButtonExitScript>().ActionDelegate += ExitGame;
      Victory.GetComponentInChildren<ButtonRestartScript>().ActionDelegate += Restart;
      GameOver.GetComponentInChildren<ButtonExitScript>().ActionDelegate += ExitGame;
      GameOver.GetComponentInChildren<ButtonRestartScript>().ActionDelegate += Restart;
      Logo.GetComponentInChildren<ButtonScript>().ActionDelegate += ShowLevel;
      
      Victory.active = false;
      GameOver.active = false;
      CommentsText.active = false;

      Intro.active = true;
      var introVideo = Intro.GetComponentInChildren<VideoPlayer>();
      //introVideo.Play();
      //introVideo.loopPointReached += ShowLogo;

      ShowLogo(introVideo);
    }

    public void ShowLogo(VideoPlayer vp)
    {
      vp.Stop();
      //Level1.active = true;
      PreloaderAnimator.Instance.Play("Start_Level2");
      //PreloaderAnimator.Instance.Play("Game_Over2");
      Logo.active = true;
      //vp.Stop();
      Intro.active = false;
      Victory.active = false;
      GameOver.active = false;
      CommentsText.active = false;
    }

    public void ShowLevel()
    {
      var gameController = GetComponent<GameController>();
      PreloaderAnimator.Instance.Play("Start_Level2");
      Logo.active = false;
      Intro.active = false;
      Victory.active = false;
      GameOver.active = false;
      gameController.StartLevel();
      CommentsText.active = true;
      currentLevel = 1;
      TextManager.Instance.SetText(0);
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
