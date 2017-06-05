using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DaleranGames.Tools
{
    [RequireComponent(typeof(Camera), typeof(Rigidbody2D))]
    public class PixelPerfectMoveZoomCamera : MonoBehaviour
    {
        [SerializeField]
        int pixelsPerUnit = 32;
        [SerializeField]
        [Range(1,8)]
        int maxScale = 2;

        [SerializeField]
        float moveForce = 15f;

        Camera cam;
        Rigidbody2D camRB;
        [SerializeField]
        float[] orthoSizes;
        int sizeIndex = 0;

        void Start()
        {
            cam = gameObject.GetRequiredComponent<Camera>();
            camRB = gameObject.GetRequiredComponent<Rigidbody2D>();
            orthoSizes = BuildSizeArray();
            cam.orthographicSize = orthoSizes[sizeIndex];

        }

        void LateUpdate()
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                Vector2 moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                camRB.AddForce(moveDir * moveForce);
            }


            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                ZoomCameraIn();
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                ZoomCameraOut();
        }

        void ZoomCameraIn()
        {
            if (sizeIndex < maxScale-1)
            {
                sizeIndex++;
                cam.orthographicSize = orthoSizes[sizeIndex];
            }
        }

        void ZoomCameraOut()
        {
            if (sizeIndex > 0)
            {
                sizeIndex--;
                cam.orthographicSize = orthoSizes[sizeIndex];
            }
        }

        float CalculateOrthographicSize (int scale)
        {
            return (((float)Screen.height)/((float)scale * (float)pixelsPerUnit)) * 0.5f;
        }

        float[] BuildSizeArray ()
        {
            float[] sizes = new float[maxScale];

            for (int i=0; i < sizes.Length; i++)
            {
                sizes[i] = CalculateOrthographicSize(i + 1);
            }

            return sizes;
        }

    }
}
