using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
  public class Timer : MonoBehaviour
  {
    public static Timer Instance;
    private TMP_Text _textMesh;
    private string _startTimer = "02:00";
    private Coroutine _displayCoroutine;

    float currentTime = 0f;
    float _startTime = 01.00f;
    float _stopTime;
    private bool isRunning = false;

    void Awake()
    {
      Instance = this;
      _textMesh = GetComponent<TMP_Text>();
    }

    //private void Update()
    //{
    //  currentTime -= Time.deltaTime;
    //  if (currentTime <= _stopTime)
    //  {
    //    currentTime = _stopTime;
    //    SetTimerText();
    //    _textMesh.color = Color.red;
    //    enabled = false;
    //  }
    //  SetTimerText();
    //}

    public void StartTimer(float startingTime)
    {
      RestartTimer();

      _startTime = startingTime;
      if (_displayCoroutine != null)
      {
        StopCoroutine(_displayCoroutine);
      }
      currentTime = _startTime;
      isRunning = true;
      _displayCoroutine = StartCoroutine("DisplayTimer");
    }

    public void StopTimer()
    { 
      isRunning = false;
      if (_displayCoroutine != null)
      {
        StopCoroutine(_displayCoroutine);
        _displayCoroutine = null;
      }
    }

    public void RestartTimer()
    {
      _textMesh.text = _startTimer;
      if (_displayCoroutine != null)
      {
        StopCoroutine(_displayCoroutine);
        _displayCoroutine = null;
      }
    }

    public void ResumeTimer()
    {
      _textMesh.text = _startTimer;
      if (_displayCoroutine != null)
      {
        StopCoroutine(_displayCoroutine);
      }
      isRunning = true;
      _displayCoroutine = StartCoroutine("DisplayTimer");
    }

    public IEnumerator DisplayTimer()
    {
      currentTime -= currentTime - Time.time;
      int minutes = (int)currentTime / 60;
      int seconds = (int)currentTime % 60;

      _textMesh.text = $"{minutes}:{seconds}";
      yield return new WaitForSeconds(1);
    }

    public void SetTimerText()
    {
      _textMesh.text = currentTime.ToString("00.00");
    }
  }
}
