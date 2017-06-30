using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace DaleranGames.Database
{
    public class GraphicsDatabaseLoader : DatabaseLoader<TileGraphic>
    {
        [SerializeField]
        protected string terrainSpriteFilePath = "Assets/Graphics/Sprites/SpringAtlas.png";
        [SerializeField]
        protected string terRefFilePath = "Assets/Graphics/Sprites/SpringNames.txt";
        [SerializeField]
        [Reorderable]
        protected TileGraphic[] terrainGraphics;
        [SerializeField]
        protected string uiSpriteFilePath = "Assets/Graphics/Sprites/UIAtlas.png";
        [SerializeField]
        protected string uiRefFilePath = "Assets/Graphics/Sprites/UINames.txt";
        [SerializeField]
        [Reorderable]
        protected TileGraphic[] uiIcons;


        public override Database<TileGraphic> GenerateDatabase()
        {
            Database<TileGraphic> newDB = new Database<TileGraphic>();
            for(int i=0; i < terrainGraphics.Length; i++ )
            {
                newDB.Add(new TileGraphic(terrainGraphics[i], id));
                id++;
            }
            for (int i = 0; i < uiIcons.Length; i++)
            {
                newDB.Add(new TileGraphic(uiIcons[i], id));
                id++;
            }
            return newDB;
        }

        public override void InitializeDatabase(Database<TileGraphic> newDB)
        {

        }

#if UNITY_EDITOR
        [ContextMenu("Build Terrain Graphics")]
        public void LoadTerrainGraphicsFromFolder()
        {
            List<TileGraphic> newGraphics = new List<TileGraphic>();
            Object[] sprites = AssetDatabase.LoadAllAssetsAtPath(terrainSpriteFilePath);
            //Debug.Log("Found " + sprites.Length + " objects");

            StreamWriter writer = new StreamWriter(terRefFilePath, false);
            writer.WriteLine("Terrain Graphic Names");
            writer.WriteLine("");

            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i] as Sprite != null)
                {
                    Sprite sprite = sprites[i] as Sprite;
                    newGraphics.Add(new TileGraphic(sprite.name, -2, GameDatabase.Instance.Atlas.GetCoordFromRect(sprite.rect)));
                    writer.WriteLine(sprite.name);
                }
            }

            writer.Close();

            terrainGraphics = newGraphics.ToArray();
        }

        [ContextMenu("Build UI Graphics")]
        public void LoadUIGraphicsFromFolder()
        {
            List<TileGraphic> newGraphics = new List<TileGraphic>();
            Object[] sprites = AssetDatabase.LoadAllAssetsAtPath(uiSpriteFilePath);
            //Debug.Log("Found " + sprites.Length + " objects");

            StreamWriter writer = new StreamWriter(uiRefFilePath, false);
            writer.WriteLine("UI Graphic Names");
            writer.WriteLine("");

            for (int i=0; i< sprites.Length;i++)
            {
                if (sprites[i] as Sprite != null)
                {
                    Sprite sprite = sprites[i] as Sprite;
                    newGraphics.Add(new TileGraphic(sprite.name, -2, GameDatabase.Instance.Atlas.GetCoordFromRect(sprite.rect)));
                    writer.WriteLine(sprite.name);
                }
            }
            writer.Close();

            uiIcons = newGraphics.ToArray();
        }
#endif
    }
}
