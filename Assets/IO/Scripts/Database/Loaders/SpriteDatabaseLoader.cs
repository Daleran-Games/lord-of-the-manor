using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DaleranGames.IO
{
    [CreateAssetMenu(fileName = "SpriteDatabaseLoader", menuName = "DaleranGames/Database/Sprite", order = 0)]
    public class SpriteDatabaseLoader : ScriptableObject
    {

        [SerializeField]
        string uiSpriteFilePath = "UIAtlas.png";
        public string UISpritePath { get { return GameDatabase.SpritePath + uiSpriteFilePath; } } 
        [SerializeField]
        string springSpriteFilePath = "SpringAtlas.png";
        public string SpringSpritePath { get { return GameDatabase.SpritePath + springSpriteFilePath; } }

        [SerializeField]
        protected List<Sprite> sprites;

        public Dictionary<string, Sprite> GenerateDatabase ()
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
            Object[] ui = AssetDatabase.LoadAllAssetsAtPath(UISpritePath);
            Object[] spr = AssetDatabase.LoadAllAssetsAtPath(SpringSpritePath);
            Object[] objs = new Object[ui.Length + spr.Length];
            System.Array.Copy(ui, objs, ui.Length);
            System.Array.Copy(spr, 0, objs, ui.Length, spr.Length);

            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] as Sprite != null)
                {
                    Sprite sprite = objs[i] as Sprite;
                    newSprites.Add(sprite);
                }
            }
            sprites = newSprites;

        }
        #endif
        
    }
}