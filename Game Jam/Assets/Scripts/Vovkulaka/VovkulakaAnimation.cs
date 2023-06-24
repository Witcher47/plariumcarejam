using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Vovkulaka
{
  public class VovkulakaAnimation : MonoBehaviour
  {
    public static VovkulakaAnimation Instance;
    public Animator _animator;

    private Dictionary<int, string> TriggerHierarcy = new Dictionary<int, string>
    {
      { 2, "play2"},
      { 3, "play3"},
      { 4, "play4"},
      { 5, "play5"},
    };

    private int currentState;

    void Awake()
    {
      //Debug.Log("vovk");
      Instance = this;
      _animator = GetComponentInChildren<Animator>();
      currentState = 2;
    }

    public void Start()
    {
      //Debug.Log("st vovk");
      Instance = this;
      _animator = GetComponentInChildren<Animator>();
      currentState = 2;
    }

    public void PlayNextAnimation()
    {
      if(_animator == null)
        _animator = GetComponentInChildren<Animator>();

      if (currentState < 5)
        currentState++;

      Debug.Log($"play{currentState}");
      if (TriggerHierarcy.ContainsKey(currentState))
      {
        _animator.SetTrigger(TriggerHierarcy[currentState]);
      }
    }

    public void PlayVictoryAnomation()
    {
      
    }

    public void ResetState()
    {
      currentState = 1;
      PlayNextAnimation();
    }
  }
}
