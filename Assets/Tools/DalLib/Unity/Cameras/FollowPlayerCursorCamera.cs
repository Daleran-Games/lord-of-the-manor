using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.Tools
{
    [RequireComponent(typeof(Camera))]
    public class FollowPlayerCursorCamera : MonoBehaviour
    {
        //[SerializeField] private float dampTime = 0.15f;
        //[SerializeField] private float maxRange = 2f;

        [SerializeField]
        [ReadOnly]
        private Transform player;

        Camera cam;
        float zOffset = -10f;
        //Vector3 velocity = Vector3.zero;

        // Use this for initialization
        void Start()
        {
            cam = gameObject.GetRequiredComponent<Camera>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            //.position = new Vector3(player.position.x, player.position.y, zOffset);

        }

        // Update is called once per frame
        void LateUpdate()
        {
            TrackTarget();
        }

        void TrackTarget()
        {

            // Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 targetPosition = (Vector2)player.position + (( mousePos - (Vector2)player.position).normalized * maxRange);
            //Vector3 newPos = Vector3.MoveTowards(player.transform.position, mousePos, maxRange);
            //Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, dampTime);
            //Vector3 newPos = Vector3.SmoothDamp(transform.position, player.position, ref velocity, dampTime);
            transform.position = new Vector3(player.position.x, player.position.y, zOffset);
        }


    } 
}
