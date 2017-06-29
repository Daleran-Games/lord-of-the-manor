using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;

namespace DaleranGames.Database
{
    public class GraphicsDatabaseLoader : DatabaseLoader<TileGraphic>
    {
        [SerializeField]
        [Reorderable]
        protected TileGraphic[] landGraphics;
        [SerializeField]
        [Reorderable]
        protected TileGraphic[] improvementGraphics;
        [SerializeField]
        [Reorderable]
        protected TileGraphic[] textGraphics;
        [SerializeField]
        [Reorderable]
        protected TileGraphic[] uiIcons;
        [SerializeField]
        [Reorderable]
        protected TileGraphic[] cursors;

        public override Database<TileGraphic> GenerateDatabase()
        {
            Database<TileGraphic> newDB = new Database<TileGraphic>();
            int id = 0;
            for(int i=0; i < landGraphics.Length; i++ )
            {
                newDB.Add(new TileGraphic(landGraphics[i], id));
                id++;
            }
            for (int i = 0; i < improvementGraphics.Length; i++)
            {
                newDB.Add(new TileGraphic(improvementGraphics[i], id));
                id++;
            }
            for (int i = 0; i < textGraphics.Length; i++)
            {
                newDB.Add(new TileGraphic(textGraphics[i], id));
                id++;
            }
            for (int i = 0; i < uiIcons.Length; i++)
            {
                newDB.Add(new TileGraphic(uiIcons[i], id));
                id++;
            }
            for (int i = 0; i < cursors.Length; i++)
            {
                newDB.Add(new TileGraphic(cursors[i], id));
                id++;
            }
            return newDB;
        }

        public override void InitializeDatabase(Database<TileGraphic> newDB)
        {

        }
    }
}
