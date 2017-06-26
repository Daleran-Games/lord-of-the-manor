using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        Mesh hexMesh;
        List<Vector3> verticieBuffer;
        List<int> triangleBuffer;
        List<Vector2> uvBuffer;
        MeshRenderer meshRenderer;
        TileAtlas meshAtlas;

        HexLayers hexLayer;
        public HexLayers Layer { get { return hexLayer; } protected set { hexLayer = value; } }

        [SerializeField]
        bool meshBuilt = false;
        public bool MeshBuilt { get { return meshBuilt; }  protected set { meshBuilt = value; } }

        [SerializeField]
        bool uiMesh = false;
        public bool UIMesh { get { return uiMesh; } set { uiMesh = value; } }

        [SerializeField]
        bool isDirty = false;

        void Awake()
        {
            GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
            hexMesh.name = "HexMesh";
            verticieBuffer = new List<Vector3>();
            triangleBuffer = new List<int>();
            uvBuffer = new List<Vector2>();
            meshRenderer = gameObject.GetRequiredComponent<MeshRenderer>();

        }

        public void BuildMesh(HexTile[,] tiles, HexLayers lay, TileAtlas atlas, bool ui)
        {
            hexMesh.Clear();
            verticieBuffer.Clear();
            triangleBuffer.Clear();
            uvBuffer.Clear();
            hexMesh.MarkDynamic();
            meshAtlas = atlas;
            UIMesh = ui;

            if (UIMesh)
                meshRenderer.material = meshAtlas.UIAtlas;
            else
                meshRenderer.material = meshAtlas.SpringAtlas;

            Layer = lay;

            for (int y= tiles.GetLength(1)-1; y >= 0; y--)
            {
                for (int x= tiles.GetLength(0)-1; x >= 0; x--)
                {
                    BuildTile(tiles[x, y]);
                }
            }

            hexMesh.vertices = verticieBuffer.ToArray();
            hexMesh.triangles = triangleBuffer.ToArray();
            hexMesh.uv = uvBuffer.ToArray();
            hexMesh.RecalculateNormals();
            MeshBuilt = true;
        }

        void BuildTile (HexTile tile)
        {
            int vertexIndex = verticieBuffer.Count;

            verticieBuffer.AddRange(HexMetrics.CalculateVerticies(tile.Position + Layer.ToVector3()));
            triangleBuffer.AddRange(HexMetrics.CalculateTriangles(vertexIndex));
            uvBuffer.AddRange(meshAtlas.CalculateUVs(tile.GetGraphicsAtLayer(Layer)));
            tile.TileGraphicsChange += UpdateUVBuffer;
        }

        public void UpdateUVBuffer (HexTile tile, HexLayers layer)
        {
            if (layer == Layer)
            {
                Vector2Int newCoord = tile.GetGraphicsAtLayer(Layer);
                UpdateUVBuffer(tile,layer, newCoord);
            }
        }

        public void UpdateUVBuffer(HexTile tile, HexLayers layer, Vector2Int coord)
        {
            if (layer == Layer)
            {
                Vector2[] newUV = meshAtlas.CalculateUVs(coord);
                //Debug.Log("Updating " + Layer.ToString() + " to " + newCoord.ToString());
                int i = tile.ID * 4;
                //Debug.Log("Max: " + HexTile.MaxID + " ID: " + tile.ID + " Estimate: "+i);

                uvBuffer[i] = newUV[0];
                uvBuffer[i + 1] = newUV[1];
                uvBuffer[i + 2] = newUV[2];
                uvBuffer[i + 3] = newUV[3];
                isDirty = true;
            }
        }

        private void LateUpdate()
        {
            if (isDirty)
                CommitUVBuffer();
        }

        public void CommitUVBuffer ()
        {
            hexMesh.uv = uvBuffer.ToArray();
            isDirty = false;
        }


        public void SwitchMateiral (Material mat)
        {
            meshRenderer.material = mat;
        }

    } 
}