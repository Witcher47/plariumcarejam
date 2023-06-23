using System;
using UnityEngine;

namespace Game.Field
{
  public class CellMoveStrategyBase : ICellMoveStrategy
  {
    private Camera _camera;

    private Vector3 _startScreenPosition;
    private Vector3 _startMousePosition;

    public CellMoveStrategyBase(Camera camera)
    {
      _camera = camera;
    }

    public void StartMove(Vector3 position)
    {
      _startScreenPosition = _camera.WorldToScreenPoint(position);
      _startMousePosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _startScreenPosition.z));
    }

    public Vector3 GetPositionOffsetFor(Vector3 direction)
    {
      var curMousePosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _startScreenPosition.z));
      var offset = curMousePosition - _startMousePosition;
      _startMousePosition = curMousePosition;
      
      if (direction != Vector3.left && direction != Vector3.right)
        offset = new Vector3(0f, offset.y, 0f);
      if (direction != Vector3.up && direction != Vector3.down)
        offset = new Vector3(offset.x, 0f, 0f);
      
      return offset;
    }
  }
}