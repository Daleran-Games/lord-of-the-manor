using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public class MeshData
    {
        public List<Vector3> verticies;
        public List<int> triangles;
        public List<Vector2> uvs;

        public MeshData ()
        {
            verticies = new List<Vector3>();
            triangles = new List<int>();
            uvs = new List<Vector2>();
        }

        public MeshData(List<Vector3> verts, List<int> tris, List<Vector2> uv)
        {
            verticies = verts;
            triangles = tris;
            uvs = uv;
        }

        public static MeshData operator +(MeshData l,MeshData r)
        {
            MeshData newData = new MeshData(l.verticies, l.triangles, l.uvs);
            newData.verticies.AddRange(r.verticies);
            newData.triangles.AddRange(r.triangles);
            newData.uvs.AddRange(r.uvs);
            return newData;
        }
    }
}
