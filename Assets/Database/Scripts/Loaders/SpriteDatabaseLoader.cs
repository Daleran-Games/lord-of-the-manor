using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DaleranGames.Database
{
    [CreateAssetMenu(fileName = "SpriteDatabaseLoader", menuName = "DaleranGames/Database/Sprite", order = 0)]
    public class SpriteDatabaseLoader : ScriptableObject
    {
        [SerializeField]
        [Reorderable]
        protected List<Sprite> sprites;

        [SerializeField]
        protected string uiSpriteFilePath = "Assets/Graphics/Sprites/UIAtlas.png";
        [SerializeField]
        protected string springSpriteFilePath = "Assets/Graphics/Sprites/SpringAtlas.png";
        [SerializeField]
        protected string refTextFilePath = "Assets/Graphics/Sprites/SpriteNames.txt";

        public Dictionary<string, Sprite> GetSpriteDictionary ()
        {
            Dictionary<string, Sprite> dict = new Dictionary<string, Sprite>();

            if (sprites != null)
            {
                foreach(Sprite s in sprites)
                {
                    dict.Add(s.name, s);
                }
            }

            return dict;
        }

        #if UNITY_EDITOR
        [ContextMenu("Load Sprites")]
        public void LoadSpritesFromFolder()
        {
            List<Sprite> newSprites = new List<Sprite>();
            Object[] ui = AssetDatabase.LoadAllAssetsAtPath(uiSpriteFilePath);
            Object[] spr = AssetDatabase.LoadAllAssetsAtPath(springSpriteFilePath);
            Object[] objs = new Object[ui.Length + spr.Length];
            System.Array.Copy(ui, objs, ui.Length);
            System.Array.Copy(spr, 0, objs, ui.Length, spr.Length);

            StreamWriter writer = new StreamWriter(refTextFilePath, false);
            writer.WriteLine("Sprite Names");
            writer.WriteLine("");

            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] as Sprite != null)
                {
                    Sprite sprite = objs[i] as Sprite;
                    newSprites.Add(sprite);
                    writer.WriteLine(sprite.name);
                }
            }

            writer.Close();

            sprites = newSprites;

        }
        #endif
        
    }
}