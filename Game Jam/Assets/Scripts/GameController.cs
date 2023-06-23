using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.UI;
using Assets.Scripts.Vovkulaka;
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

        private bool levelIsLoaded;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void StartLevel()
        {
            StartCoroutine(Load());
        }

        private IEnumerator Load()
        {
            if(levelIsLoaded)
              SceneManager.UnloadSceneAsync(CurLevelName);
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
            levelIsLoaded = true;
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
        public LevelCell[] Cells;
        public Vector2int EmptyPosition;

        public bool IsEmptyPosition(int x, int y) => EmptyPosition.X == x && EmptyPosition.Y == y;
    }

    [Serializable]
    public class LevelCell
    {
        public Vector2int Position;
        public CellView Prefab;
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
        
        public static Vector2int operator +(Vector2int a, Vector2int b) => new Vector2int(a.X + b.X, a.Y + b.Y);
    }
}
