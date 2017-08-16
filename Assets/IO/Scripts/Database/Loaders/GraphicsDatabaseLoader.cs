using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System.IO;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DaleranGames.IO
{

    //TODO: Move Database loaders into a scriptable object
    [CreateAssetMenu(fileName = "GraphicsDatabaseLoader", menuName = "DaleranGames/Database/Graphics", order = 0)]
    [System.Serializable]
    public class GraphicsDatabaseLoader : ScriptableObject
    {
        [SerializeField]
        string spriteFilePath = "Sprites";

        [SerializeField]
        TileAtlas atlas;

        [SerializeField]
        List<TileGraphic> graphics;


        public Database<TileGraphic> GenerateDatabase()
        {

            Database<TileGraphic> newDB = new Database<TileGraphic>();
            Sprite[] sprites = Resources.LoadAll<Sprite>(spriteFilePath);
            int id = 0;
            for (int i = 0; i < sprites.Length; i++)
            {
                newDB.Add(new TileGraphic(sprites[i].name, id, atlas.GetCoordFromRect(sprites[i].rect), sprites[i]));
                id++;
            }
            return newDB;
        }


#if UNITY_EDITOR
        [ContextMenu("Build Graphics")]
        public void LoadUIGraphicsFromFolder()
        {
            List<TileGraphic> newGraphics = new List<TileGraphic>();
            Sprite[] sprites = Resources.LoadAll<Sprite>(spriteFilePath);
            int id = 0;
            for (int i=0; i< sprites.Length;i++)
            {
                    newGraphics.Add(new TileGraphic(sprites[i].name, id,atlas.GetCoordFromRect(sprites[i].rect),sprites[i]));
                    id++;
            }
            graphics = newGraphics;
        }
#endif
    }
}
