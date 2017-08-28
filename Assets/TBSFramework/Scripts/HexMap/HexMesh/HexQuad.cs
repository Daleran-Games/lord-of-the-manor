using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexQuad : MonoBehaviour
    {
        Mesh quadMesh;
        List<Vector3> vertices;
        List<int> triangles;
        List<Vector2> uvs;
        MeshRenderer meshRenderer;
        TileAtlas meshAtlas;

        [SerializeField]
        bool quadBuilt = false;
        public bool QuadBuilt { get { return quadBuilt; } protected set { quadBuilt = value; } }

        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = quadMesh = new Mesh();
            quadMesh.name = "HexMesh";
            vertices = new List<Vector3>();
            triangles = new List<int>();
            uvs = new List<Vector2>();
            meshRenderer = gameObject.GetRequiredComponent<MeshRenderer>();
        }

        public void BuildQuad(Vector3 position, TileAtlas atlas, Material material)
        {
            quadMesh.Clear();
            vertices.Clear();
            triangles.Clear();
            uvs.Clear();
            quadMesh.MarkDynamic();

            meshAtlas = atlas;
            meshRenderer.material = material;

            vertices.AddRange(HexMetrics.CalculateVerticies(position));
            triangles.AddRange(HexMetrics.CalculateTriangles(0));
            uvs.AddRange(atlas.CalculateUVs(Vector2Int.zero));

            quadMesh.vertices = vertices.ToArray();
            quadMesh.triangles = triangles.ToArray();
            quadMesh.uv = uvs.ToArray();
            quadMesh.RecalculateNormals();
            QuadBuilt = true;
        }

        public void SwitchMaterial (Material mat)
        {
            meshRenderer.material = mat;
        }

        public void SetUV (Vector2Int coord)
        {
            Vector2[] newUV = meshAtlas.CalculateUVs(coord);

            uvs[0] = newUV[0];
            uvs[1] = newUV[1];
            uvs[2] = newUV[2];
            uvs[3] = newUV[3];
            quadMesh.uv = uvs.ToArray();
        }
    }
}
