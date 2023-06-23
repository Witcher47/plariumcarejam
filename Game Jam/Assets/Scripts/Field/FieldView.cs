using System;
using Game.Tools;
using UnityEngine;

namespace Game.Field
{
    public class FieldView : MonoBehaviour
    {
        public class Cell
        {
            public Vector3 Direction { get; set; }
            public bool IsFree { get; set; }
        
            public Cell(Vector3 direction, bool free)
            {
                Direction = direction;
                IsFree = free;
            }
        }
        
        [SerializeField] private Vector2 _cellSize;
        [SerializeField] private CellView _cellPrefab;
        [SerializeField] private Camera _camera;
        [SerializeField] public bool UseSound = false;

    private AudioSource _audioSource;

    private Cell[,] _field;
        private ICellMoveStrategy _moveStrategy;

        private void Start()
        {
            _moveStrategy = new CellMoveStrategyBase(_camera);
            if(UseSound)
              _audioSource = GetComponent<AudioSource>();
             
        }
        
        public void Build(Level level)
        {
            _field = new Cell[level.Size.X, level.Size.Y];
            for (var i = 0; i < _field.GetLength(0); i++)
            {
                for (var j = 0; j < _field.GetLength(1); j++)
                {
                    _field[i, j] = new Cell(Vector3.zero, level.IsEmptyPosition(i + 1, j + 1));
                    if (_field[i, j].IsFree)
                        continue;
                    
                    var cellView = Instantiate(_cellPrefab, new Vector3(_cellSize.x * i, -_cellSize.y * j, 0f), Quaternion.identity, transform);
                    cellView.SetPosition(new Vector2int(i, j));
                    cellView.OnStartDrag += OnStartDrag;
                    cellView.OnDrag += OnDragCell;
                    cellView.OnDragComplete += OnDragComplete;
                    cellView.OnPositionChanged += CellPositionChanged;
                }
            }

            UpdateDirections();
        }

        private void UpdateDirections()
        {
            var freePosition = new Vector2int();
            for (var i = 0; i < _field.GetLength(0); i++)
            {
                for (var j = 0; j < _field.GetLength(1); j++)
                {
                    if (_field[i, j].IsFree)
                    {
                        freePosition.X = i;
                        freePosition.Y = j;
                    }

                    _field[i, j].Direction = Vector3.zero;
                }
            }
            
            if (freePosition.X - 1 >= 0)
                _field[freePosition.X - 1, freePosition.Y].Direction = Vector3.right;
            
            if (freePosition.X + 1 < _field.GetLength(0))
                _field[freePosition.X + 1, freePosition.Y].Direction = Vector3.left;
            
            if (freePosition.Y + 1 < _field.GetLength(1))
                _field[freePosition.X, freePosition.Y + 1].Direction = Vector3.up;
            
            if (freePosition.Y - 1 >= 0)
                _field[freePosition.X, freePosition.Y - 1].Direction = Vector3.down;
        }

        private void SetFreeCell(Cell freeCell)
        {
            for (var i = 0; i < _field.GetLength(0); i++)
            {
                for (var j = 0; j < _field.GetLength(1); j++)
                {
                    _field[i, j].IsFree = _field[i, j] == freeCell;
                }
            }
        }

        private void OnStartDrag(CellView view)
        {
            _moveStrategy.StartMove(view.transform.position);
      
           if(UseSound)
            _audioSource.Play();
        }
        
        private void OnDragCell(CellView view)
        {
            var possibleDirection = _field[view.CellPosition.X, view.CellPosition.Y].Direction;
            var moveTo = view.transform.position + _moveStrategy.GetPositionOffsetFor(possibleDirection);
            var clampXMin = view.WorldPosition.x - (possibleDirection == Vector3.left ? _cellSize.x : 0);
            var clampXMax = view.WorldPosition.x + (possibleDirection == Vector3.right ? _cellSize.x : 0);
            var clampYMin = view.WorldPosition.y - (possibleDirection == Vector3.down ? _cellSize.y : 0);
            var clampYMax = view.WorldPosition.y + (possibleDirection == Vector3.up ? _cellSize.y : 0);
            
            view.MoveTo = new Vector3(
                Mathf.Clamp(moveTo.x, clampXMin, clampXMax),
                Mathf.Clamp(moveTo.y, clampYMin, clampYMax),
                moveTo.z);
        }

        private void OnDragComplete(CellView view)
        {
           if(UseSound)
            _audioSource.Stop();
            var possiblePosition = view.WorldPosition + (Vector3)(_field[view.CellPosition.X, view.CellPosition.Y].Direction * _cellSize);
            var startPosition = view.WorldPosition;
            var currentPosition = view.transform.position;

            if ((possiblePosition - currentPosition).sqrMagnitude > (startPosition - currentPosition).sqrMagnitude)
                view.MoveTo = startPosition;
            else
                view.MoveTo = possiblePosition;
        }

        private void CellPositionChanged(CellView view)
        {
            var freePosition = view.CellPosition;
            var freeCell = _field[freePosition.X, freePosition.Y];
            var direction = freeCell.Direction.ToVector2Int();
            view.SetPosition(view.CellPosition + new Vector2int(direction.X, -direction.Y));
            SetFreeCell(freeCell);
            UpdateDirections();
        }
    }
}
