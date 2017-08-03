using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    public class CommandMediator : MonoBehaviour
    {
        [SerializeField]
        bool activeMode = false;
        [SerializeField]
        float timeSkip = 0.1f;
        float lastClickTime;

        HexCursor mouse;

        Command currentCommand = null;
        Group activeOwner = null;

        private void Awake()
        {
            mouse = GameObject.FindObjectOfType<HexCursor>();
        }

        public void EnterCommandMode (string commandName)
        {
            currentCommand = GameDatabase.Instance.Commands[commandName];
            activeOwner = GroupManager.Instance.PlayerGroup;

            EnterActivityMode(currentCommand);
        }

        public void EnterCommandMode (Command command, Group owner)
        {
            currentCommand = command;
            activeOwner = owner;

            EnterActivityMode(currentCommand);
        }

        protected void EnterActivityMode(Command activity)
        {
            if (activity != null)
            {
                activeMode = true;
                lastClickTime = Time.time;
                mouse.HexTileLMBClicked += OnLeftTileClick;
                mouse.HexTileEntered += OnTileEnter;
                InputManager.Instance.RMBClick.MouseButtonUp += OnRightClick;
                UpdateCursor(mouse.CurrentTile);
            }
        }

        public void DoActivityOnTile(Command activity, HexTile tile, Group owner)
        {
            if (activeMode == true && activity.IsValidCommand(tile) && tile.Owner == owner)
                activity.PreformCommand(tile);
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
                DoActivityOnTile(currentCommand, tile, activeOwner);
                UpdateCursor(tile);
                //Debug.Log("Recieved Left Click");
            }

        }

        void OnRightClick()
        {
            activeMode = false;
            currentCommand = null;
            mouse.CursorUIIcon = TileGraphic.Clear;
            mouse.CursorTerrainIcon = TileGraphic.Clear;
            mouse.CursorMode = HexCursor.HexCursorMode.Ring;
            mouse.HexTileLMBClicked -= OnLeftTileClick;
            mouse.HexTileEntered -= OnTileEnter;
            InputManager.Instance.RMBClick.MouseButtonUp -= OnRightClick;
        }

        void UpdateCursor (HexTile tile)
        {
            if (mouse.CurrentTile != null)
            {
                mouse.CursorUIIcon = currentCommand.GetUIIcon(tile);
                mouse.CursorTerrainIcon = currentCommand.GetTerrainIcon(tile);
                

                if (currentCommand.IsValidCommand(tile) && tile.Owner == activeOwner)
                    mouse.CursorMode = HexCursor.HexCursorMode.Positive;
                else
                    mouse.CursorMode = HexCursor.HexCursorMode.Negative;
            }
        }
    }
}
