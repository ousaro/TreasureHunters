using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Osaro.Utilities;
using UnityEngine.EventSystems;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Osaro.Utilities
{
    public class GridSystem <TGridObject> 
    {
        // Const 
        public const int HEAT_MAP_MAX_VALUE = 100;
        public const int HEAT_MAP_MIN_VALUE = 0;

        

        // Events 
        public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
        public class OnGridObjectChangedEventArgs : EventArgs
        {
            public int x;
            public int y;
        }


        // variable needed
        private int _width;
        private int _height;
        private float _cellSize;
        private Vector3 _worldOrigin;

        private TGridObject[,] _gridArray;
        private TextMesh[,] _debugArray;


        public GridSystem(int width, int height, float cellSize, Vector3 worldOrigin, Func<GridSystem<TGridObject>,int,int,TGridObject> createGridObject)
        {
            this._width = width;
            this._height = height;
            this._cellSize = cellSize;
            this._worldOrigin = worldOrigin;

            this._gridArray = new TGridObject[_width, _height];
            this._debugArray = new TextMesh[_width, _height];

            GridInit(createGridObject);

            bool showDebug = true;

            if (showDebug)
            {
               DebugMode();
            }


        }


        public void DebugMode(){
             for (int x = 0; x < _gridArray.GetLength(0); x++)
                {
                    for (int y = 0; y < _gridArray.GetLength(1); y++)
                    {
                        Vector3 localpositionWithOffset = GetWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * .5f;
                        _debugArray[x, y] = UtilsClass.CreateWorldText(_gridArray[x, y]?.ToString(), null, localpositionWithOffset, 20, Color.white, TextAnchor.MiddleCenter);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);

                    }
                }

                Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);

                OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
                {
                    _debugArray[eventArgs.x, eventArgs.y].text = _gridArray[eventArgs.x, eventArgs.y]?.ToString();
                };
        }

        private void GridInit(Func<GridSystem<TGridObject>,int,int,TGridObject> createGridObject){
            for (int x = 0; x < _gridArray.GetLength(0); x++)
                {
                    for (int y = 0; y < _gridArray.GetLength(1); y++)
                    {
                        _gridArray[x,y] = createGridObject(this,x,y);
                    }
                }
        }

        public int GetWidth()
        {
            return _width;

        }

        public int GetHeight()
        {
            return _height;
        }


        public float GetCellSize()
        {
            return _cellSize;
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * _cellSize + _worldOrigin;
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - _worldOrigin).x / _cellSize);
            y = Mathf.FloorToInt((worldPosition - _worldOrigin).y / _cellSize);
        }

        public void SetGridObjectr(int x, int y , TGridObject value)
        {
            if(x>=0 && y>=0 && x<_width && y < _height)
            {
                _gridArray[x, y] = value;
                if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
            }
        }

        public void TriggerGridChagedValueEvent(int x, int y){
             if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }

        public void SetGridObjectr(Vector3 worldPosition, TGridObject value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetGridObjectr(x, y, value);
        }


        public TGridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                return _gridArray[x, y];
            }
            else
            {
                return default(TGridObject);
            }
        }

        public TGridObject GetGridObject(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetGridObject(x,y);
        }



    }

}
