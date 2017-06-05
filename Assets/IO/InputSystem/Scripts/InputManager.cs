using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.IO
{
    public class InputManager : MonoBehaviour
    {

        protected InputManager() { }
        public static InputManager Instance = null;

        [SerializeField]
        bool controlsLocked = false;
        public Action<bool> ControlStateChanged;
        public bool ControlsLocked
        {
            get { return controlsLocked; }
            set
            {
                ControlStateChanged(value);
                controlsLocked = value;
            }
        }

        public EventKey MenuKey;
        public EventKey SubmitKey;
        public EventKey CancelKey;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            MenuKey = new EventKey("Menu");
            SubmitKey = new EventKey("Submit");
            CancelKey = new EventKey("Cancel");

    }

        private void Update()
        {
            if (MenuKey.IsPressedOnce() && MenuKey.EventKeyPressed != null)
                MenuKey.EventKeyPressed();

            if (SubmitKey.IsPressedOnce() && SubmitKey.EventKeyPressed != null && ControlsLocked == false)
                SubmitKey.EventKeyPressed();

            if (CancelKey.IsPressedOnce() && CancelKey.EventKeyPressed != null && ControlsLocked == false)
                CancelKey.EventKeyPressed();

        }

        public class ToggleKey
        {
            bool isAxisInUse = false;
            bool keyState = false;
            string axis;
            public Action<bool> KeyToggled;

            public ToggleKey(string axisName, bool startingState)
            {
                axis = axisName;
                keyState = startingState;
            }

            public string GetAxisName()
            {
                return axis;
            }

            public bool GetToggleState()
            {
                return keyState;
            }

            public bool SetToggleState()
            {
                if (Input.GetAxisRaw(axis) != 0)
                {
                    if (isAxisInUse == false)
                    {
                        keyState = !keyState;

                        isAxisInUse = true;
                        return true;
                    }
                }
                if (Input.GetAxisRaw(axis) == 0)
                {
                    isAxisInUse = false;

                }
                return false;
            }

        }

        public class EventKey
        {
            string axis;
            bool alreadyPressed = false;
            public Action EventKeyPressed;

            public EventKey(string axisName)
            {
                axis = axisName;
            }

            public string GetAxisName()
            {
                return axis;
            }

            public bool IsPressed()
            {
                if (Input.GetAxisRaw(axis) != 0f)
                    return true;
                else
                    return false;
            }

            public bool IsPressedOnce()
            {
                if (Input.GetAxisRaw(axis) != 0f && alreadyPressed == false)
                {
                    alreadyPressed = true;
                    return true;
                }
                else if (Input.GetAxisRaw(axis) != 0f && alreadyPressed == true)
                {
                    return false;
                }
                else if (Input.GetAxisRaw(axis) == 0f && alreadyPressed == true)
                {
                    alreadyPressed = false;
                    return false;
                }
                else
                    return false;

            }
        }

        public class ControlAxis
        {
            string axis;

            public ControlAxis(string axisName)
            {
                axis = axisName;
            }

            public string GetAxisName()
            {
                return axis;
            }

            public float GetAxisValue()
            {
                return Input.GetAxis(axis);
            }

            public float GetAxisRaw()
            {
                return Input.GetAxisRaw(axis);
            }

            public bool IsAxisInUse()
            {
                if (Input.GetAxis(axis) != 0)
                    return true;
                else
                    return false;
            }

            public bool IsPositiveAndInUse()
            {
                if (Input.GetAxis(axis) > 0 && IsAxisInUse())
                    return true;
                else
                    return false;
            }

        }

    }
}
