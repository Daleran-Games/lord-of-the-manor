using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    public class ActivityDirector : MonoBehaviour
    {
        [SerializeField]
        bool activeMode = false;
        [SerializeField]
        float timeSkip = 0.1f;
        float lastClickTime;

        HexCursor mouse;

        Activity currentActivity = null;

        private void Awake()
        {
            mouse = GameObject.FindObjectOfType<HexCursor>();
        }

        public void EnterActivityMode (Activity activity)
        {
            activeMode = true;
            lastClickTime = Time.time;
            currentActivity = activity;
            mouse.HexTileLMBClicked += OnLeftTileClick;
            mouse.HexTileEntered += OnTileEnter;
            InputManager.Instance.RMBClick.MouseButtonUp += OnRightClick;
            UpdateCursor(mouse.CurrentTile);

        }

        public void DoActivityOnTile(Activity activity, HexTile tile)
        {
            if (activeMode == true && activity.IsActivityValid(tile))
                activity.DoActivityOnTile(tile);
        }

        void OnTileEnter(HexTile tile)
        {
            UpdateCursor(tile);
        }

        void OnLeftTileClick(HexTile tile)
        {
            
            if (Time.time - lastClickTime > timeSkip)
            {
                lastClickTime = Time.time;
                DoActivityOnTile(currentActivity, tile);
                UpdateCursor(tile);
                //Debug.Log("Recieved Left Click");
            }

        }

        void OnRightClick()
        {
            activeMode = false;
            currentActivity = null;
            mouse.CursorUIIcon = Vector2Int.zero;
            mouse.CursorTerrainIcon = Vector2Int.zero;
            mouse.CursorMode = HexCursor.HexCursorMode.Ring;
            mouse.HexTileLMBClicked -= OnLeftTileClick;
            mouse.HexTileEntered -= OnTileEnter;
            InputManager.Instance.RMBClick.MouseButtonUp -= OnRightClick;
        }

        void UpdateCursor (HexTile tile)
        {
            if (mouse.CurrentTile != null)
            {
                mouse.CursorUIIcon = currentActivity.GetUIIcon(tile);
                mouse.CursorTerrainIcon = currentActivity.GetTerrainIcon(tile);
                

                if (currentActivity.IsActivityValid(tile))
                    mouse.CursorMode = HexCursor.HexCursorMode.Positive;
                else
                    mouse.CursorMode = HexCursor.HexCursorMode.Negative;
            }
        }
    }
}
