using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using DaleranGames.IO;
using DaleranGames.Database;

namespace DaleranGames.UI
{
    public class ClearButton : MonoBehaviour
    {

        [SerializeField]
        bool buildMode = false;

        HexCursor mouse;

        private void Awake()
        {
            mouse = GameObject.FindObjectOfType<HexCursor>();
        }

        public void ClearLand()
        {
            buildMode = true;

            mouse.HexTileLMBClicked += OnLeftTileClick;
            InputManager.Instance.RMBClick.MouseButtonUp += OnRightClick;
            mouse.CursorIcon = Vector2Int.zero;

            mouse.HexTileEntered += OnTileEnter;

            if (mouse.CurrentTile.Land.CanClear())
                mouse.CursorMode = HexCursor.HexCursorMode.Positive;
            else
                mouse.CursorMode = HexCursor.HexCursorMode.Negative;
        }

        void OnTileEnter(HexTile tile)
        {
            if (tile.Land.CanClear())
                mouse.CursorMode = HexCursor.HexCursorMode.Positive;
            else
                mouse.CursorMode = HexCursor.HexCursorMode.Negative;
        }

        void OnLeftTileClick(HexTile tile)
        {

            if (tile.Land.CanClear() && buildMode == true)
                tile.Land.ClearTile(tile);

        }

        void OnRightClick()
        {
            buildMode = false;
            mouse.CursorMode = HexCursor.HexCursorMode.Ring;
            mouse.HexTileLMBClicked -= OnLeftTileClick;
            mouse.HexTileEntered -= OnTileEnter;
            InputManager.Instance.RMBClick.MouseButtonUp -= OnRightClick;
        }
    }
}
