using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.Tools
{
    [RequireComponent(typeof(Camera))]
    public class PixelPerfectMoveZoomCamera : MonoBehaviour
    {
        [SerializeField]
        protected int pixelsPerUnit = 32;
        [SerializeField]
        [Range(1, 8)]
        protected int maxScale = 2;

        [SerializeField]
        protected float panSpeed = 15f;
        [SerializeField]
        protected float panBorderThickness = 20f;

        [SerializeField]
        protected Vector2 bottomLeftExtent = Vector2.zero;

        [SerializeField]
        protected Vector2 topRightExtent = new Vector2(10f, 10f);

        protected Camera cam;
        [SerializeField]
        protected float[] orthoSizes;
        [SerializeField]
        protected int sizeIndex = 0;
        public virtual int SizeIndex
        {
            get { return sizeIndex; }
            protected set
            {
                sizeIndex = value;

                if (CameraZoomChange != null)
                    CameraZoomChange(sizeIndex);
            }
        }
        public event System.Action<int> CameraZoomChange;

        protected virtual void Start()
        {
            cam = gameObject.GetRequiredComponent<Camera>();
            orthoSizes = BuildSizeArray();
            cam.orthographicSize = orthoSizes[sizeIndex];

        }

        protected virtual void LateUpdate()
        {
            Vector2 moveDir = new Vector2();

            if (Input.GetAxis("Horizontal") > 0 || Input.mousePosition.x > Screen.width - panBorderThickness)
                moveDir.x = panSpeed;
            if (Input.GetAxis("Horizontal") < 0 || Input.mousePosition.x < panBorderThickness)
                moveDir.x = -panSpeed;
            if (Input.GetAxis("Vertical") > 0 || Input.mousePosition.y > Screen.height - panBorderThickness)
                moveDir.y = panSpeed;
            if (Input.GetAxis("Vertical") < 0 || Input.mousePosition.y < panBorderThickness)
                moveDir.y = -panSpeed;


            Vector3 newPosition = transform.position + (Vector3)moveDir.normalized * panSpeed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, bottomLeftExtent.x, topRightExtent.x);
            newPosition.y = Mathf.Clamp(newPosition.y, bottomLeftExtent.y, topRightExtent.y);

            transform.position = newPosition;

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                ZoomCameraIn();
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                ZoomCameraOut();
        }

        protected virtual void ZoomCameraIn()
        {
            if (SizeIndex < maxScale-1)
            {
                SizeIndex++;
                cam.orthographicSize = orthoSizes[sizeIndex];
            }
        }

        protected virtual void ZoomCameraOut()
        {
            if (SizeIndex > 0)
            {
                SizeIndex--;
                cam.orthographicSize = orthoSizes[sizeIndex];
            }
        }

        protected virtual float CalculateOrthographicSize (int scale)
        {
            return (((float)Screen.height)/((float)scale * (float)pixelsPerUnit)) * 0.5f;
        }

        protected virtual float[] BuildSizeArray ()
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
