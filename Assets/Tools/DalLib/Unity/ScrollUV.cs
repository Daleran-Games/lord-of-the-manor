using UnityEngine;

namespace DaleranGames.Effects
{
    public class ScrollUV : MonoBehaviour
    {

        void Update()
        {

            MeshRenderer mr = GetComponent<MeshRenderer>();
            Material mat = mr.material;
            Vector2 offset = mat.mainTextureOffset;
            offset.x += Time.deltaTime / 10f;
            mat.mainTextureOffset = offset;
        }

    }
}
