using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osaro.Utilities
{

    public class HeatMapGenericVisual<HeatMapObject> : MonoBehaviour
    {
        private GridSystem<HeatMapObject> _grid;
        private Mesh _mesh;
        private bool _updateMesh;

        private void Awake() {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
        }   

      
        public void SetGrid(GridSystem<HeatMapObject> grid)
        {
            this._grid = grid;
            UpdateHeatMapVisual();

            _grid.OnGridObjectChanged += Grid_OnGridObjectChanged;
            
        }

        private void Grid_OnGridObjectChanged(object sender, GridSystem<HeatMapObject>.OnGridObjectChangedEventArgs e)
        {
            _updateMesh = true;
        }

        private void LateUpdate() {
            if(_updateMesh){
                _updateMesh = false;
                UpdateHeatMapVisual();
            }
        }


        private void UpdateHeatMapVisual(){

            MeshUtils.CreateEmptyMeshArrays(_grid.GetWidth() * _grid.GetHeight(), out Vector3[] vertices, out Vector2[] uvs, out int[] triangles);

            for(int x=0; x<_grid.GetWidth(); x++){
                for(int y=0; y<_grid.GetHeight(); y++){
                    int index = x * _grid.GetHeight() + y; // index of rhe cell 
                    Vector3 quadSize = new Vector3(1,1) * _grid.GetCellSize();
                    Vector3 quadPostion = _grid.GetWorldPosition(x,y) + quadSize * .5f;

                    // MeshUtils.AddToMeshArrays(vertices, uvs, triangles, index, quadPostion, 0f, quadSize, Vector2.zero, Vector2.zero); using zero uvoo and uv 11 will make the color of the heat map to be the same and start from the firt color in the material

                    HeatMapObject gridValue = _grid.GetGridObject(x,y);
                    float gridValueNormalized = 0f;
                    //float gridValueNormalized = gridValue.GetValueNormalized();
                    Vector2 gridValueUV = new Vector2(gridValueNormalized, 0f);
                    MeshUtils.AddToMeshArrays(vertices, uvs, triangles, index, quadPostion, 0f, quadSize, gridValueUV, gridValueUV);
                    
                


                }
            }

            _mesh.vertices = vertices;
            _mesh.uv = uvs;
            _mesh.triangles = triangles;

        }

    }

}
