using System;
using Game.Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Field
{
  public class CellView : MonoBehaviour
  {
    public Vector2int CellPosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public Vector3 NextWorldPosition { get; private set; }
    public Vector3? MoveTo { get; set; }

    public event Action<CellView> OnStartDrag;
    public event Action<CellView> OnDrag;
    public event Action<CellView> OnDragComplete;
    public event Action<CellView> OnPositionChanged;

    private bool _dragInProgress;

    public void SetPosition(Vector2int cellPosition)
    {
      CellPosition = cellPosition;
      WorldPosition = transform.position;
    }

    private void Update()
    {
      if (!MoveTo.HasValue)
        return;
      
      if (MoveTo.Value.IsEqual(transform.position))
      {
        if (!_dragInProgress && !MoveTo.Value.IsEqual(WorldPosition))
          OnPositionChanged?.Invoke(this);
        
        MoveTo = null;
        return; 
      }

      transform.position = Vector3.Lerp(transform.position, MoveTo.Value, Time.deltaTime * 80f);
    }

    private void OnMouseDown()
    {
      _dragInProgress = true;
      OnStartDrag?.Invoke(this);
    }
    
    private void OnMouseDrag()
    {
      OnDrag?.Invoke(this);
    }

    private void OnMouseUp()
    {
      if (!_dragInProgress)
        return;
      
      OnDragComplete?.Invoke(this);
      _dragInProgress = false;
    }
  }
}