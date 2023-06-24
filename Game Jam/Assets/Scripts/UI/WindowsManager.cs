using Assets.Scripts.UI;
using Assets.Scripts.Vovkulaka;
using Game;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public float StartTime;

    private  AudioSource Source;
    private GameController gameController;

    private int currentLevel = 0;

    void Start()
    {
      SceneManager.UnloadSceneAsync("Level_1");
      Victory.GetComponentInChildren<ButtonExitScript>().ActionDelegate += ExitGame;
      Victory.GetComponentInChildren<ButtonRestartScript>().ActionDelegate += Restart;
      GameOver.GetComponentInChildren<ButtonExitScript>().ActionDelegate += ExitGame;
      GameOver.GetComponentInChildren<ButtonRestartScript>().ActionDelegate += Restart;
      Logo.GetComponentInChildren<ButtonScript>().ActionDelegate += ShowLevel;
      GameUi.GetComponentInChildren<ButtonScript>().ActionDelegate += ShowMenu;
      Menu.GetComponentInChildren<ButtonExitScript>().ActionDelegate += ExitGame;
      Menu.GetComponentInChildren<ButtonRestartScript>().ActionDelegate += Restart;

      gameController = GetComponent<GameController>();
      Source = GetComponent<AudioSource>();

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
      if(Source != null)
      StartCoroutine(FadeIn(Source, 0.7f));
      //PreloaderAnimator.Instance.Play("Game_Over2");
      Logo.SetActive(true);
      Intro.SetActive(false);
      Victory.SetActive(false);
      GameOver.SetActive(false);
      GameUi.SetActive(false);
      Menu.SetActive(false);
    }

    public void ShowLevel()
    {
      PreloaderAnimator.Instance.Play("Start_Level2");
      Logo.SetActive(false);
      Intro.SetActive(false);
      Victory.SetActive(false);
      GameOver.SetActive(false);
      Menu.SetActive(false);
      gameController.StartLevel();
      GameUi.SetActive(true);
      currentLevel = 1;
      TextManager.Instance.SetText(0);
      Timer.Instance.StartTimer(StartTime*60f);
      //Timer.Instance.OnAnimationChange += VovkulakaAnimation.Instance.PlayNextAnimation;
      Timer.Instance.OnTimerExpire += ShowGameOver;
      Timer.Instance.OnTextTimer += TextManager.Instance.PrintNextText;
    }

    public void ShowMenu()
    {
      Timer.Instance.StopTimer();
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
      VovkulakaAnimation.Instance.ResetState();
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
            ShowLevel();
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

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
      float startVolume = audioSource.volume;

      while (audioSource.volume > 0)
      {
        audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

        yield return null;
      }

      audioSource.Stop();
      audioSource.volume = startVolume;
      //levelCanLoad = true;
    }

    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
      float startVolume = 0.2f;

      audioSource.volume = 0;
      audioSource.Play();

      while (audioSource.volume < 1.0f)
      {
        audioSource.volume += startVolume * Time.deltaTime / FadeTime;

        yield return null;
      }
      audioSource.volume = 1f;
    }

    public void PlayVovkAnim()
    {
      VovkulakaAnimation.Instance.PlayNextAnimation();
    }

  }
}
