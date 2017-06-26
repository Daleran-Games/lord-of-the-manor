using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMeshChunk : MonoBehaviour
    {
        Mesh hexMesh;
        List<Vector3> verticieBuffer;
        List<int> triangleBuffer;
        List<Vector2> uvBuffer;
        MeshRenderer meshRenderer;
        TileAtlas meshAtlas;


        [SerializeField]
        bool meshBuilt = false;
        public bool MeshBuilt { get { return meshBuilt; } protected set { meshBuilt = value; } }

        [SerializeField]
        Vector2Int chunk;
        public Vector2Int Chunk { get { return chunk; } protected set { chunk = value; } }

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

        public void BuildMesh(Vector2Int chunk, HexGrid grid, TileAtlas atlas, bool ui)
        {

        }

        public void RefreshMesh()
        {

        }

        public void SwitchMateiral(Material mat)
        {
            meshRenderer.material = mat;
        }

        private void LateUpdate()
        {

        }

    }
}
