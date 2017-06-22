using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DaleranGames.IO
{
    public class MouseCursor : MonoBehaviour
    {
        //HexCursor hexCursor;
        Image sprite;
        RectTransform trans;
        Canvas mouseCanvas;

        public Vector3 WorldPosition { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); } }

        private void Awake()
        {
            Cursor.visible = false;
            //hexCursor = GameObject.FindObjectOfType<HexCursor>();
            sprite = gameObject.GetRequiredComponent<Image>();
            trans = gameObject.GetRequiredComponent<RectTransform>();
            mouseCanvas = GetComponentInParent<Canvas>();
        }
        // Update is called once per frame
        void Update()
        {
            transform.position = Input.mousePosition;
        }

        public void ToggleCursor (bool state)
        {
            if (state == true)
                sprite.enabled = true;
            else
                sprite.enabled = false;
        }
    }
}