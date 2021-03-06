﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;
using DaleranGames.UI;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public abstract class Command 
    {

        public static readonly Command Null = new NullCommand();

        public abstract string GetInfo(HexTile tile, Group group);

        public virtual bool IsValidCommand(HexTile tile, Group owner)
        {
            return false;
        }

        public virtual void PreformCommand(HexTile tile, Group owner)
        {

        }

        public virtual TileGraphic GetUIIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

        public virtual TileGraphic GetTerrainIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

    }
}

