using UnityEngine;

namespace Game.Tools
{
  public static class Extensions
  {
    public static Vector2int ToVector2Int(this Vector3 vec)
    {
      return new Vector2int(Mathf.RoundToInt(vec.x), Mathf.RoundToInt(vec.y));
    }

    public static bool IsEqual(this Vector3 vec1, Vector3 vec2)
    {
      return (vec1 - vec2).sqrMagnitude < 0.0001f;
    }
  }
}