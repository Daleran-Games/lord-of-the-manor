using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        [SerializeField]
        Material hexMeshMaterial;
        public Material HexMeshMaterial { get { return hexMeshMaterial; } set { hexMeshMaterial = value; } }

        Mesh hexMesh;
        List<Vector3> vertices;
        List<int> triangles;
        List<Vector2> uvs;

        void Awake()
        {
            GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
            hexMesh.name = "HexMesh";
            vertices = new List<Vector3>();
            triangles = new List<int>();
            uvs = new List<Vector2>();
        }

        public void BuildMesh(HexCell[,] cells, TileAtlas atlas)
        {
            hexMesh.Clear();
            vertices.Clear();
            triangles.Clear();
            uvs.Clear();
            HexMeshMaterial = atlas.AtlasMaterial;

            for (int y= cells.GetLength(1)-1; y >= 0; y--)
            {
                for (int x= cells.GetLength(0)-1; x >= 0; x--)
                {
                    BuildCell(cells[x, y], atlas);
                }
            }

            hexMesh.vertices = vertices.ToArray();
            hexMesh.triangles = triangles.ToArray();
            hexMesh.uv = uvs.ToArray();
            hexMesh.RecalculateNormals();
        }

        void BuildCell (HexCell cell, TileAtlas atlas)
        {
            int vertexIndex = vertices.Count;
            vertices.AddRange(HexMetrics.CalculateVerticies(cell.Position));
            triangles.AddRange(HexMetrics.CalculateTriangles(vertexIndex));
            uvs.AddRange(atlas.CalculateUVs(cell.HexType.AtlasCoord));
        }

    } 
}