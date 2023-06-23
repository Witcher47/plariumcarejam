﻿using System;
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

    public void StartTimer(float startingTime)
    {
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

    public void ResumeTimer()
    {
      _textMesh.text = GetTimeToStringFormat(currentTime);
      if (_displayCoroutine != null)
      {
        StopCoroutine(_displayCoroutine);
      }
      isRunning = true;
      _displayCoroutine = StartCoroutine("DisplayTimer");
    }

    public IEnumerator DisplayTimer()
    {
      _textMesh.color = Color.white;
      Debug.Log("strtrtrt");
      do
      {
        currentTime --;
        if (currentTime == _stopTime)
          _textMesh.color = Color.red;

        _textMesh.text = GetTimeToStringFormat(currentTime);
        yield return new WaitForSeconds(1f);
      }
      while (currentTime > _stopTime);
      yield return new WaitForSeconds(1f);
    }

    private string GetTimeToStringFormat(float time)
    {
      int minutes = (int)time / 60;
      int seconds = (int)time % 60;

      return $"{minutes.ToString("00")}:{seconds.ToString("00")}";
    }
  }
}
