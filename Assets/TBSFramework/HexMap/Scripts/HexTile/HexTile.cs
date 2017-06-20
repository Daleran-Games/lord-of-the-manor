using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{

    [System.Serializable]
    public class HexTile
    {
        [SerializeField]
        [ReadOnly]
        protected HexCoordinates coord;
        public HexCoordinates Coord { get { return coord; } protected set { coord = value; } }

        [SerializeField]
        [ReadOnly]
        protected Vector3 position = Vector3.zero;
        public Vector3 Position { get { return position; } protected set { position = value; } }

        [SerializeField]
        [ReadOnly]
        protected byte elevation = 0;
        public byte Elevation { get { return elevation; } set { elevation = value; } }

        [SerializeField]
        [ReadOnly]
        protected byte moisture = 0;
        public byte Moisture { get { return moisture; } set { moisture = value; } }

        [SerializeField]
        protected LandType land;
        public virtual LandType Land { get { return land; } }

        [SerializeField]
        protected ImprovementType improvement;
        public virtual ImprovementType Improvement { get { return improvement; } }

        public Action<HexTile> TileGraphicsChange;


        public HexTile(HexCoordinates coor, Vector3 pos)
        {
            Coord = coor;
            Position = pos;
        }

        public Vector2Int GetAtlasCoordAtLayer(HexLayers layer)
        {


            return Land.GetGraphicsAtLayer(layer);
        }

        public void SetToLandType(LandType type)
        {
            land = type;

            if (TileGraphicsChange != null)
                TileGraphicsChange(this);
        }


    }

}