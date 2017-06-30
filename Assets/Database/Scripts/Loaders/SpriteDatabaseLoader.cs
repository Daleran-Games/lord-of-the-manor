using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DaleranGames.Database
{
    public class SpriteDatabaseLoader : MonoBehaviour
    {
        [SerializeField]
        [Reorderable]
        protected List<Sprite> sprites;
        [SerializeField]
        protected string spriteFilePath = "Assets/Graphics/Sprites";

        public Dictionary<string, Sprite> GetSpriteDictionary ()
        {
            Dictionary<string, Sprite> dict = new Dictionary<string, Sprite>();

            if (sprites != null)
            {

            }

            return dict;
        }

        #if UNITY_EDITOR
        [ContextMenu("Load Sprites")]
        public void LoadSpritesFromFolder()
        {

        }
        #endif
        
    }
}