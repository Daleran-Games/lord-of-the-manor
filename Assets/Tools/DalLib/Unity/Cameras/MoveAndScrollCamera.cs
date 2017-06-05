using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.Tools
{
    [RequireComponent(typeof(Camera), typeof(Rigidbody2D))]
    public class MoveAndScrollCamera : MonoBehaviour
    {

        [SerializeField]
        private float ROTStep = 16f;
        [SerializeField]
        private float min = 4f;
        [SerializeField]
        private float max = 128f;
        [SerializeField]
        private float startZoom = 20f;
        [SerializeField]
        private float moveSpeed = 1f;

        private Camera cam;
        private Rigidbody2D camRB;

        void Start()
        {
            cam = gameObject.GetRequiredComponent<Camera>();
            camRB = gameObject.GetRequiredComponent<Rigidbody2D>();
            cam.orthographicSize = startZoom;

        }

        void LateUpdate()
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                Vector2 moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                camRB.AddForce(moveDir * moveSpeed);
            }


            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                ZoomCameraIn();
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                ZoomCameraOut();
        }

        void ZoomCameraIn()
        {
            cam.orthographicSize -= ROTStep;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, min, max);
        }

        void ZoomCameraOut()
        {
            cam.orthographicSize += ROTStep;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, min, max);
        }
    }
}
