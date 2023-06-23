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
    public GameObject GameUi;
    public GameObject Menu;
    private GameController gameController;

    private int currentLevel = 0;

    void Start()
    {
      Victory.GetComponentInChildren<ButtonExitScript>().ActionDelegate += ExitGame;
      Victory.GetComponentInChildren<ButtonRestartScript>().ActionDelegate += Restart;
      GameOver.GetComponentInChildren<ButtonExitScript>().ActionDelegate += ExitGame;
      GameOver.GetComponentInChildren<ButtonRestartScript>().ActionDelegate += Restart;
      Logo.GetComponentInChildren<ButtonScript>().ActionDelegate += ShowLevel;
      //GameUi.GetComponentInChildren<ButtonScript>().ActionDelegate += ShowMenu;
      Menu.GetComponentInChildren<ButtonExitScript>().ActionDelegate += ExitGame;
      Menu.GetComponentInChildren<ButtonRestartScript>().ActionDelegate += Restart;
      gameController = GetComponent<GameController>();

      Victory.SetActive(false);
      GameOver.SetActive(false);
      GameUi.SetActive(false);
      Menu.SetActive(false);

      Intro.SetActive(true);
      var introVideo = Intro.GetComponentInChildren<VideoPlayer>();
      //introVideo.Play();
      //introVideo.loopPointReached += ShowLogo;

      ShowLogo(introVideo);
    }

    public void ShowLogo(VideoPlayer vp = null)
    {
      if(vp != null)
      vp.Stop();
      PreloaderAnimator.Instance.Play("Start_Level2");
      //PreloaderAnimator.Instance.Play("Game_Over2");
      Logo.SetActive(true);
      Intro.SetActive(false);
      Victory.SetActive(false);
      GameOver.SetActive(false);
      GameUi.SetActive(false);
    }

    public void ShowLevel()
    {
      PreloaderAnimator.Instance.Play("Start_Level2");
      Logo.SetActive(false);
      Intro.SetActive(false);
      Victory.SetActive(false);
      GameOver.SetActive(false);
      gameController.StartLevel();
      GameUi.SetActive(true);
      currentLevel = 1;
      TextManager.Instance.SetText(0);
    }

    public void ShowMenu()
    {
      Menu.SetActive(true);
      Logo.SetActive(false);
      Intro.SetActive(false);
      Victory.SetActive(false);
      GameOver.SetActive(false);
      GameUi.SetActive(true);
    }

    public void ShowGameOver()
    {
      GameOver.SetActive(true);
      Menu.SetActive(false);
      Logo.SetActive(false);
      Intro.SetActive(false);
      Victory.SetActive(false);
      GameUi.SetActive(false);
    }

    public void ShowVictory()
    {
      currentLevel = 0;

      Victory.SetActive(true);
      GameOver.SetActive(false);
      Menu.SetActive(false);
      Logo.SetActive(false);
      Intro.SetActive(false);
      GameUi.SetActive(false);
    }

    public void Restart()
    {
      switch (currentLevel)
      {
        case 1:
          {
            gameController.StartLevel();
            break;
          }
        default:
          {
            ShowLogo();
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
