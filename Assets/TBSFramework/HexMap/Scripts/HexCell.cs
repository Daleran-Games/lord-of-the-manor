using System;
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
        Land land;
        public Land HexLand { get { return land; } set { land = value; } }
        public Action<HexCell> LandChanged;

        [SerializeField]
        [ReadOnly]
        Improvement improvement;
        public Improvement HexImprovement { get { return improvement;} set { improvement = value; } }
        public Action<HexCell> ImprovementChanged;

        public HexCell(HexCoordinates coor, Vector3 pos)
        {
            Coord = coor;
            Position = pos;

            HexLand = new Land();
            HexImprovement = new Improvement();
        }



    }

}