﻿using System.Collections;
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
        TileType hexType;
        public TileType HexType { get { return hexType; } set { hexType = value; } }

        [SerializeField]
        [Range(0, 255)]
        protected byte elevation = 0;
        public byte Elevation { get { return elevation; } set { elevation = value; } }

        [SerializeField]
        [Range(0, 255)]
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

        public HexCell(HexCoordinates coor, Vector3 pos, TileType type)
        {
            Coord = coor;
            Position = pos;
            HexType = type;
        }

    }

}