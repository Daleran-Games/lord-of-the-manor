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
    public class GraphicsDatabaseLoader : ScriptableObject
    {
        [SerializeField]
        string uiSpriteFilePath = "UIAtlas.png";
        public string UISpritePath { get { return GameDatabase.SpritePath + uiSpriteFilePath; } }
        [SerializeField]
        string springSpriteFilePath = "SpringAtlas.png";
        public string SpringSpritePath { get { return GameDatabase.SpritePath + springSpriteFilePath; } }

        //[SerializeField]
        //protected string graphicRefFilePath = "GraphicNames.txt";
        //protected string RefFilePath { get { return GameDatabase.GameDataPath + graphicRefFilePath; } }

        [SerializeField]
        TileAtlas atlas;

        [SerializeField]
        TileGraphic[] graphics;


        public Database<TileGraphic> GenerateDatabase()
        {

            Database<TileGraphic> newDB = new Database<TileGraphic>();

            for(int i=0; i < graphics.Length; i++ )
            {
                newDB.Add(graphics[i]);
            }
            return newDB;
        }


#if UNITY_EDITOR
        [ContextMenu("Build Graphics")]
        public void LoadUIGraphicsFromFolder()
        {
            List<TileGraphic> newGraphics = new List<TileGraphic>();

            UnityEngine.Object[] ui = AssetDatabase.LoadAllAssetsAtPath(UISpritePath);
            UnityEngine.Object[] spr = AssetDatabase.LoadAllAssetsAtPath(SpringSpritePath);
            UnityEngine.Object[] objs = new UnityEngine.Object[ui.Length + spr.Length];
            System.Array.Copy(ui, objs, ui.Length);
            System.Array.Copy(spr, 0, objs, ui.Length, spr.Length);

            //StreamWriter writer = new StreamWriter(RefFilePath, false);
           // writer.WriteLine("Graphics");
            //writer.WriteLine("id  Name");
            //writer.WriteLine(" ");
            
            int id = 0;

            for (int i=0; i< objs.Length;i++)
            {
                if (objs[i] as Sprite != null)
                {
                    Sprite sprite = objs[i] as Sprite;
                    newGraphics.Add(new TileGraphic(sprite.name, id,atlas.GetCoordFromRect(sprite.rect)));
                    //writer.WriteLine(id + "  " +sprite.name);
                    id++;

                }
            }
            //writer.Close();

            graphics = newGraphics.ToArray();
        }
#endif
    }
}
