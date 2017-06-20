using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        Mesh hexMesh;
        List<Vector3> vertices;
        List<int> triangles;
        List<Vector2> uvs;
        MeshRenderer meshRenderer;

        HexLayers layer;
        public HexLayers Layer { get { return layer; } protected set { layer = value; } }

        bool meshBuilt = false;
        public bool MeshBuilt { get { return meshBuilt; }  protected set { meshBuilt = value; } }

        void Awake()
        {
            GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
            hexMesh.name = "HexMesh";
            vertices = new List<Vector3>();
            triangles = new List<int>();
            uvs = new List<Vector2>();
            meshRenderer = gameObject.GetRequiredComponent<MeshRenderer>();

        }

        public void BuildMesh(HexTile[,] tiles, TileAtlas atlas, HexLayers layer)
        {
            hexMesh.Clear();
            vertices.Clear();
            triangles.Clear();
            uvs.Clear();
            meshRenderer.material = atlas.SpringAtlas;
            Layer = layer;

            for (int y= tiles.GetLength(1)-1; y >= 0; y--)
            {
                for (int x= tiles.GetLength(0)-1; x >= 0; x--)
                {
                    BuildTile(tiles[x, y], atlas, layer);
                }
            }

            hexMesh.vertices = vertices.ToArray();
            hexMesh.triangles = triangles.ToArray();
            hexMesh.uv = uvs.ToArray();
            hexMesh.RecalculateNormals();
            MeshBuilt = true;
        }

        void BuildTile (HexTile tile, TileAtlas atlas, HexLayers layer)
        {
            int vertexIndex = vertices.Count;
            Vector3 tilePosition = new Vector3(tile.Position.x, tile.Position.y, HexMetrics.standardZ);
            vertices.AddRange(HexMetrics.CalculateVerticies(tile.Position));
            triangles.AddRange(HexMetrics.CalculateTriangles(vertexIndex));
            uvs.AddRange(atlas.CalculateUVs(tile.GetAtlasCoordAtLayer(layer)));
        }

        public void UpdateUV (HexTile tile, TileAtlas atlas)
        {

        }

        public void SwitchMateiral (Material mat)
        {
            meshRenderer.material = mat;
        }

    } 
}