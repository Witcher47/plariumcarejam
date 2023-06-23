using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Vovkulaka
{
  public class VovkulakaAnimation : MonoBehaviour
  {
    public static VovkulakaAnimation Instance;
    private Animator _animator;

    private Dictionary<int, string> TriggerHierarcy = new Dictionary<int, string>
    {
      { 1, "play1"},
      { 2, "play2"},
      { 3, "play3"},
      { 4, "play4"},
    };

    public int currentState;

    void Awake()
    {
      Instance = this;
      _animator = GetComponentInChildren<Animator>();
      currentState = 0;
      PlayNextAnimation();
    }

    public void PlayNextAnimation()
    {
      if (currentState < 4)
        currentState++;
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
      currentState = 0;
      PlayNextAnimation();
    }
  }
}
