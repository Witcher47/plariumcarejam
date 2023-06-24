using System;
using System.Collections;
using System.Linq;
using Game.Field;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        private const string PathTemplate = "Level_";

        private string CurLevelName => PathTemplate + (_curLevelIdx + 1);
        private Level CurLevel => _levels[_curLevelIdx];
        
        [SerializeField] private Level[] _levels;
        [SerializeField] private GameObject[] _menuObjectsToHide;

        private int _curLevelIdx;
        private FieldView _curGameField;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Start()
        {
            StartLevel();
        }
        
        public void StartLevel()
        {
            StartCoroutine(Load());
        }

        private IEnumerator Load()
        {
            var asyncLoad = SceneManager.LoadSceneAsync(CurLevelName, LoadSceneMode.Additive);
            
            Debug.Log("Loading progress:");
            while (!asyncLoad.isDone)
            {
                Debug.Log($"Loading progress: {asyncLoad.progress}");
                yield return null;
            }
            Debug.Log("Loading complete.");

            PrepareScene();
            BuildLevel();
        }

        private void PrepareScene()
        {
            foreach (var menuObject in _menuObjectsToHide)
                menuObject.SetActive(false);
        }

        private void BuildLevel()
        {
            if (!SceneManager.GetSceneByName(CurLevelName)
                    .GetRootGameObjects()
                    .Any(go => go.TryGetComponent(out _curGameField)))
            {
                return;
            }

            _curGameField.Build(CurLevel);
        }
    }

    [Serializable]
    public class Level
    {
        public Vector2int Size;
        public Group[] Groups;
        public Vector2int[] EmptyPositions;

        public bool IsEmptyPosition(int x, int y) => EmptyPositions.Any(i => i.X == x && i.Y == y);
    }

    [Serializable]
    public struct Group
    {
        public Vector2int[] Positions;
    }

    [Serializable]
    public struct Vector2int
    {
        public int X;
        public int Y;

        public Vector2int(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public static Vector2int operator +(Vector2int a, Vector2int b) => new (a.X + b.X, a.Y + b.Y);
        public static Vector2int operator -(Vector2int a, Vector2int b) => new (a.X - b.X, a.Y - b.Y);
        public static bool operator ==(Vector2int a, Vector2int b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Vector2int a, Vector2int b) => a.X != b.X && a.Y != b.Y;
    }
}
