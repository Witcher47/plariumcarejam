using UnityEngine;

namespace Game.Field
{
  public interface ICellMoveStrategy
  {
    void StartMove(Vector3 position);
    Vector3 GetPositionOffsetFor(Vector3 directions);
  }
}