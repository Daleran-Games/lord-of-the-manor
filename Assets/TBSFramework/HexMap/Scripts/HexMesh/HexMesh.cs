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
        TileAtlas atlas;

        [SerializeField]
        HexLayers layer;
        public HexLayers Layer { get { return layer; } protected set { layer = value; } }

        [SerializeField]
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
            atlas = gameObject.GetComponentInParent<HexGrid>().Generator.Atlas;

        }

        private void OnDestroy()
        {

        }

        public void BuildMesh(HexTile[,] tiles, HexLayers layer)
        {
            hexMesh.Clear();
            vertices.Clear();
            triangles.Clear();
            uvs.Clear();
            hexMesh.MarkDynamic();
            meshRenderer.material = atlas.SpringAtlas;
            Layer = layer;

            for (int y= tiles.GetLength(1)-1; y >= 0; y--)
            {
                for (int x= tiles.GetLength(0)-1; x >= 0; x--)
                {
                    BuildTile(tiles[x, y]);
                }
            }

            hexMesh.vertices = vertices.ToArray();
            hexMesh.triangles = triangles.ToArray();
            hexMesh.uv = uvs.ToArray();
            hexMesh.RecalculateNormals();
            MeshBuilt = true;
        }

        void BuildTile (HexTile tile)
        {
            int vertexIndex = vertices.Count;
            Vector3 tilePosition = new Vector3(tile.Position.x, tile.Position.y, HexMetrics.standardZ);
            vertices.AddRange(HexMetrics.CalculateVerticies(tile.Position));
            triangles.AddRange(HexMetrics.CalculateTriangles(vertexIndex));
            uvs.AddRange(atlas.CalculateUVs(tile.GetGraphicsAtLayer(Layer)));
            tile.TileGraphicsChange += UpdateUV;
        }

        public void UpdateUV (HexTile tile)
        {

            Vector2Int newCoord = tile.GetGraphicsAtLayer(Layer);
            Vector2[] newUV = atlas.CalculateUVs(newCoord);
            //Debug.Log("Updating " + Layer.ToString() + " to " + newCoord.ToString());
            int i = (HexTile.MaxID - tile.ID) * 4;
            //Debug.Log("Max: " + HexTile.MaxID + " ID: " + tile.ID + " Estimate: "+i);
          
            uvs[i] = newUV[0];
            uvs[i+1] = newUV[1];
            uvs[i+2] = newUV[2];
            uvs[i+3] = newUV[3];
            hexMesh.uv = uvs.ToArray();
        }

        public void SwitchMateiral (Material mat)
        {
            meshRenderer.material = mat;
        }

    } 
}