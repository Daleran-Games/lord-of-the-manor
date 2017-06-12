using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{

    [System.Serializable]
    public class HexCell
    {
        [SerializeField]
        [ReadOnly]
        HexCoordinates coord;
        public HexCoordinates Coord { get { return coord; } protected set { coord = value; } }

        [SerializeField]
        [ReadOnly]
        Vector3 position = Vector3.zero;
        public Vector3 Position { get { return position; } protected set { position = value; } }

        [SerializeField]
        [ReadOnly]
        TerrainType terrainType;
        public TerrainType HexTerrainType { get { return terrainType; } set { terrainType = value; } }

        [SerializeField]
        [ReadOnly]
        protected byte elevation = 0;
        public byte Elevation { get { return elevation; } set { elevation = value; } }

        [SerializeField]
        [ReadOnly]
        protected byte moisture = 0;
        public byte Moisture { get { return moisture; } set { moisture = value; } }

        public HexCell(HexCoordinates coor, Vector3 pos)
        {
            Coord = coor;
            Position = pos;
        }

        public HexCell(HexCoordinates coor, Vector3 pos, byte elevation, byte moisture)
        {
            Coord = coor;
            Position = pos;
            Elevation = elevation;
            Moisture = moisture;
        }

        public HexCell(HexCoordinates coor, Vector3 pos, TerrainType type)
        {
            Coord = coor;
            Position = pos;
            HexTerrainType = type;
        }

    }

}