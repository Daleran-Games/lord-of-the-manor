using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


namespace DaleranGames.IO
{
    public class MouseCursor : Singleton<MouseCursor>
    {
        protected MouseCursor() { }

        public enum MouseButton
        {
            LMB = 0,
            RMB = 1,
            MMB = 2
        }

        Image sprite;
        RectTransform trans;
        Canvas mouseCanvas;

        [SerializeField]
        bool toggleHardwareCursor = false;

        [SerializeField]
        Vector3 position;  //For DEBUG only!!
        public Vector3 Position { get { return position; } }

        [SerializeField]
        public Vector3 worldPosition; //For DEBUG only!!
        public Vector3 WorldPosition { get { return worldPosition; } }

        public MouseEvent LMBClick;
        public MouseEvent RMBClick;
        public MouseEvent MMBClick;


        private void Awake()
        {
            sprite = gameObject.GetRequiredComponent<Image>();
            trans = gameObject.GetRequiredComponent<RectTransform>();
            mouseCanvas = GetComponentInParent<Canvas>();

            DontDestroyOnLoad(transform.root);

            LMBClick = new MouseEvent(MouseButton.LMB);
            RMBClick = new MouseEvent(MouseButton.RMB);
            MMBClick = new MouseEvent(MouseButton.MMB);

            ToggleHardwareCursor(toggleHardwareCursor);


        }
        // Update is called once per frame
        void Update()
        {
            MoveCursor(Input.mousePosition);

            LMBClick.CheckForClicks();
            RMBClick.CheckForClicks();
            MMBClick.CheckForClicks();
        }


        void MoveCursor(Vector3 pos)
        {
            transform.position = ClampToWindow(pos);
            position = transform.position;
            worldPosition = Camera.main.ScreenToWorldPoint(position);
        }

        Vector3 ClampToWindow(Vector3 newPos)
        {
            float x = newPos.x;
            float y = newPos.y;

            if (newPos.x < 0f)
                x = 0f;
            else if (newPos.x > mouseCanvas.pixelRect.width)
                x = mouseCanvas.pixelRect.width;

            if (newPos.y < 0f)
                y = 0f;
            else if (newPos.y > mouseCanvas.pixelRect.height)
                y = mouseCanvas.pixelRect.height;

            return new Vector3(x, y, newPos.z);
        }


        public void ToggleCursor (bool state)
        {
            if (state == true)
                sprite.enabled = true;
            else
                sprite.enabled = false;
        }

        public void ToggleHardwareCursor (bool state)
        {
            if (state == true)
            {
                Cursor.visible = true;
                ToggleCursor(false);
            }
            else
            {
                Cursor.visible = false;
                ToggleCursor(true);
            }
        }

        public class MouseEvent
        {
            int button;
            public event Action MouseButtonPressed;
            public event Action MouseButtonUp;
            public event Action MouseButtonDown;

            public MouseEvent(MouseButton button)
            {
                this.button = (int)button;
            }

            public MouseButton GetButtonType()
            {
                return (MouseButton)button;
            }

            public bool IsPressed()
            {
                return Input.GetMouseButton(button);
            }

            public bool IsMouseButtonDown()
            {
                return Input.GetMouseButtonDown(button);
            }

            public bool IsMouseButtonUp()
            {
                return Input.GetMouseButtonUp(button);
            }

            public void CheckForClicks()
            {
                if (IsPressed() && MouseButtonPressed != null)
                    MouseButtonPressed();

                if (IsMouseButtonDown() && MouseButtonDown != null)
                    MouseButtonDown();

                if (IsMouseButtonUp() && MouseButtonUp != null)
                    MouseButtonUp();
            }

        }
    }
}