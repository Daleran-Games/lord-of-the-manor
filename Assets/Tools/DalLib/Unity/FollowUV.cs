using UnityEngine;

namespace DaleranGames.Effects
{

    public class FollowUV : MonoBehaviour
    {

        [SerializeField] private float parralax = 2f;

        void Update()
        {

            MeshRenderer mr = GetComponent<MeshRenderer>();
            Material mat = mr.material;  
            Vector2 offset = mat.mainTextureOffset;

            offset.x = transform.position.x / transform.localScale.x / parralax;
            offset.y = transform.position.y / transform.localScale.y / parralax;

            mat.mainTextureOffset = offset;

        }

    }
}
