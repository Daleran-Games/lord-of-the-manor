using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DaleranGames.Database
{

    //TODO: Move Database loaders into a scriptable object

    public class GraphicsDatabaseLoader : DatabaseLoader<TileGraphic>
    {
        [SerializeField]
        protected string terrainSpriteFilePath = "Assets/Graphics/Sprites/SpringAtlas.png";
        [SerializeField]
        protected string uiSpriteFilePath = "Assets/Graphics/Sprites/UIAtlas.png";
        [SerializeField]
        protected string graphicRefFilePath = "GraphicNames.txt";
        [SerializeField]
        [Reorderable]
        protected TileGraphic[] graphics;


        public override Database<TileGraphic> GenerateDatabase()
        {
            Database<TileGraphic> newDB = new Database<TileGraphic>();
            string[] files = Directory.GetFiles(JSONFilePath, "*.json",SearchOption.TopDirectoryOnly);

            for(int i=0; i < files.Length; i++ )
            {
                string jsonString = File.ReadAllText(files[i]);
                newDB.Add(new TileGraphic(JsonUtility.FromJson<TileGraphic>(jsonString), id));
                id++;
            }

            return newDB;
        }

        public override void InitializeDatabase(Database<TileGraphic> newDB)
        {

        }

        public override void BuildJSONFiles()
        {
            Directory.CreateDirectory(JSONFilePath);

            for (int i=0; i<graphics.Length;i++)
            {
                File.WriteAllText(JSONFilePath + graphics[i].Name + ".json", graphics[i].ToJson());
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Build Graphics")]
        public void LoadUIGraphicsFromFolder()
        {
            List<TileGraphic> newGraphics = new List<TileGraphic>();

            Object[] ui = AssetDatabase.LoadAllAssetsAtPath(uiSpriteFilePath);
            Object[] spr = AssetDatabase.LoadAllAssetsAtPath(terrainSpriteFilePath);
            Object[] objs = new Object[ui.Length + spr.Length];
            System.Array.Copy(ui, objs, ui.Length);
            System.Array.Copy(spr, 0, objs, ui.Length, spr.Length);

            StreamWriter writer = new StreamWriter(JSONFilePath + graphicRefFilePath, false);
            writer.WriteLine("Graphic Names");
            writer.WriteLine("");

            for (int i=0; i< objs.Length;i++)
            {
                if (objs[i] as Sprite != null)
                {
                    Sprite sprite = objs[i] as Sprite;
                    newGraphics.Add(new TileGraphic(sprite.name, -2, GameDatabase.Instance.Atlas.GetCoordFromRect(sprite.rect)));
                    writer.WriteLine(sprite.name);
                }
            }
            writer.Close();

            graphics = newGraphics.ToArray();
        }
#endif
    }
}
