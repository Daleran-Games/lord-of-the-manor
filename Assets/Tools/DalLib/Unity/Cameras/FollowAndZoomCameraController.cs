using UnityEngine;

namespace DaleranGames.Tools
{
    [RequireComponent(typeof(Camera))]
    public class FollowAndZoomCameraController : MonoBehaviour
    {
        [SerializeField] private float ROTStep = 16f;
        [SerializeField] private float min = 4f;
        [SerializeField] private float max = 128f;
        [SerializeField] private float startZoom = 20f;
        [SerializeField] private GameObject target;

        private Vector3 offset;
        private Camera cam;

        void Start()
        {
            offset = new Vector3(0f, 0f, -10f);
            cam = gameObject.GetRequiredComponent<Camera>();
            cam.orthographicSize = startZoom;
            target = GameObject.FindGameObjectWithTag("Player");

        }

        void LateUpdate()
        {
            TrackTarget();

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                ZoomCameraIn();
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                ZoomCameraOut();
        }

        void TrackTarget()
        {
            if (target != null)
                transform.position = target.transform.position + offset;
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

