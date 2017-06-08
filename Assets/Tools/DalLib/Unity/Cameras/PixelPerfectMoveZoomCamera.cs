using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DaleranGames.Tools
{
    [RequireComponent(typeof(Camera))]
    public class PixelPerfectMoveZoomCamera : MonoBehaviour
    {
        [SerializeField]
        int pixelsPerUnit = 32;
        [SerializeField]
        [Range(1,8)]
        int maxScale = 2;

        [SerializeField]
        float moveSpeed = 15f;

        Camera cam;
        [SerializeField]
        float[] orthoSizes;
        int sizeIndex = 0;

        void Start()
        {
            cam = gameObject.GetRequiredComponent<Camera>();
            orthoSizes = BuildSizeArray();
            cam.orthographicSize = orthoSizes[sizeIndex];

        }

        void LateUpdate()
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                Vector2 moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime;
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
