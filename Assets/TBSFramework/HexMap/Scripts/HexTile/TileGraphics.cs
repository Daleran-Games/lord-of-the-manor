using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class TileGraphics
    {
        TileAtlas atlas;
        bool meshDirty = true;
        Vector3 tilePosition = Vector3.zero;

        public System.Action GraphicsChanged;

        MeshData meshData;
        public MeshData MeshData
        {
            get
            {
                if (meshDirty)
                {
                    meshData = GenerateNewMeshData();
                    meshDirty = false;
                }

                return meshData;
            }
        }

        Dictionary<TileLayers, TileGraphic> graphics;

        public TileGraphics(TileAtlas atlas, Vector3 pos)
        {
            this.atlas = atlas;
            meshData = new MeshData();
            graphics = new Dictionary<TileLayers, TileGraphic>();
            tilePosition = pos;
        }

        public MeshData GenerateNewMeshData()
        {
            MeshData newData = new MeshData();
            foreach (KeyValuePair<TileLayers, TileGraphic> kvp in graphics)
            {
                if (kvp.Value.AtlasCoord != Vector2Int.zero)
                {
                    int vertIndex = newData.verticies.Count;
                    newData.verticies.AddRange(HexMetrics.CalculateVerticies(kvp.Key.ToVector3WithOffset(tilePosition)));
                    newData.triangles.AddRange(HexMetrics.CalculateTriangles(vertIndex));
                    newData.uvs.AddRange(atlas.CalculateUVs(kvp.Value.AtlasCoord));
                }
            }
            return newData;
        }


        public bool Contains(TileLayers layer)
        {
            return graphics.ContainsKey(layer);
        }

        public void Add(TileLayers layer, TileGraphic graphic)
        {
            if (graphics.ContainsKey(layer))
            {
                Debug.LogWarning("Tile at " + layer + " already has a graphic");
                graphics.Remove(layer);
            }
            graphics.Add(layer, graphic);
            Changed();
        }

        public void Remove(TileLayers layer)
        {
            if (graphics.ContainsKey(layer))
            {
                graphics.Remove(layer);
                Changed();
            }
        }

        public void ClearGraphics()
        {
            graphics.Clear();
            Changed();
        }

        void Changed()
        {
            meshDirty = true;
            if (GraphicsChanged != null)
                GraphicsChanged();
        }
    }
}

