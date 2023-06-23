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
      Instance = this;
      //_animator = GetComponent<Animator>();
      currentState = 1;
    }

    public void PlayNextAnimation()
    {
      //if (currentState < 4)
      //  currentState++;
      //if (TriggerHierarcy.ContainsKey(currentState))
      //{
      //  _animator.SetTrigger(TriggerHierarcy[currentState]);
      //}
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
