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

        Command currentCommand = Command.Null;
        Group activeOwner = Group.Null;

        private void Awake()
        {
            mouse = GameObject.FindObjectOfType<HexCursor>();
        }

        public void EnterPlaceCommandMode (string featureName)
        {
            currentCommand = new PlaceCommand(featureName);
            activeOwner = GroupManager.Instance.PlayerGroup;
            EnterCommandMode(currentCommand);
        }

        public void EnterWorkCommandMode ()
        {
            currentCommand = new WorkCommand();
            activeOwner = GroupManager.Instance.PlayerGroup;
            EnterCommandMode(currentCommand);
        }

        public void EnterCancelCommandMode ()
        {
            currentCommand = new CancelCommand();
            activeOwner = GroupManager.Instance.PlayerGroup;
            EnterCommandMode(currentCommand);
        }

        public void EnterUpgradeCommandMode()
        {
            currentCommand = new UpgradeCommand();
            activeOwner = GroupManager.Instance.PlayerGroup;
            EnterCommandMode(currentCommand);
        }

        public void EnterCheatStealLandMode()
        {
            currentCommand = new CheatStealLandCommand();
            activeOwner = GroupManager.Instance.PlayerGroup;
            EnterCommandMode(currentCommand);
        }

        public void EnterCommandMode (Command command, Group owner)
        {
            currentCommand = command;
            activeOwner = owner;
            EnterCommandMode(currentCommand);
        }

        protected void EnterCommandMode(Command activity)
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

        public void PreformCommandOnTile(Command activity, HexTile tile, Group owner)
        {
            if (activeMode == true && activity.IsValidCommand(tile,owner))
                activity.PreformCommand(tile,owner);
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
                PreformCommandOnTile(currentCommand, tile, activeOwner);
                UpdateCursor(tile);
                //Debug.Log("Recieved Left Click");
            }

        }

        void OnRightClick()
        {
            activeMode = false;
            currentCommand = Command.Null;
            activeOwner = Group.Null;
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
                

                if (currentCommand.IsValidCommand(tile, activeOwner))
                    mouse.CursorMode = HexCursor.HexCursorMode.Positive;
                else
                    mouse.CursorMode = HexCursor.HexCursorMode.Negative;
            }
        }
    }
}
