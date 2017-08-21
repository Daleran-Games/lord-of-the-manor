using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using DaleranGames.UI;

namespace DaleranGames.TBSFramework
{
    public class CommandMediator : Singleton<CommandMediator>
    {
        [SerializeField]
        bool activeMode = false;
        public bool ActiveMode { get { return activeMode; } }
        [SerializeField]
        float timeSkip = 0.1f;
        float lastClickTime;

        HexCursor mouse;

        [SerializeField]
        Command currentCommand = Command.Null;
        public Command CurrentCommand { get { return currentCommand; } }

        [SerializeField]
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

        public void EnterBuyLandMode()
        {
            currentCommand = new BuyLandCommand();
            activeOwner = GroupManager.Instance.PlayerGroup;
            EnterCommandMode(currentCommand);
        }

        public void EnterCheatStealLandMode()
        {
            currentCommand = new CheatStealLandCommand();
            activeOwner = GroupManager.Instance.PlayerGroup;
            EnterCommandMode(currentCommand);
        }

        public void BuyCommand(int good)
        {
            PreformImmediateCommand(new BuyCommand(GoodType.FromValue<GoodType>(good)), GroupManager.Instance.PlayerGroup);
        }

        public void SellCommand(int good)
        {
            PreformImmediateCommand(new SellCommand(GoodType.FromValue<GoodType>(good)), GroupManager.Instance.PlayerGroup);
        }

        public void EnterCommandMode (Command command, Group owner)
        {
            currentCommand = command;
            activeOwner = owner;
            EnterCommandMode(currentCommand);
        }

        protected void EnterCommandMode(Command command)
        {
            if (command != null)
            {
                activeMode = true;
                lastClickTime = Time.time;
                mouse.HexTileLMBClicked += OnLeftTileClick;
                mouse.HexTileEntered += OnTileEnter;
                MouseCursor.Instance.RMBClick.MouseButtonUp += OnRightClick;
                UpdateCursor(mouse.CurrentTile);
            }
        }

        public void ExitCommandMode()
        {
            mouse.HexTileEntered -= OnTileEnter;
            mouse.HexTileLMBClicked -= OnLeftTileClick;
            MouseCursor.Instance.RMBClick.MouseButtonUp -= OnRightClick;
            mouse.CursorUIIcon = TileGraphic.Clear;
            mouse.CursorTerrainIcon = TileGraphic.Clear;
            mouse.CursorMode = HexCursor.HexCursorMode.Ring;
            currentCommand = Command.Null;
            activeOwner = Group.Null;
            activeMode = false;

        }

        public void PreformCommandOnTile(Command command, HexTile tile, Group owner)
        {
            if (activeMode == true && command.IsValidCommand(tile,owner))
                command.PreformCommand(tile,owner);
        }

        public void PreformImmediateCommand(Command command, Group group)
        {
            if (command.IsValidCommand(null, group))
                command.PreformCommand(null, group);
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
                TooltipManager.Instance.ForceHexTileTooltipUpdate();
                //Debug.Log("Recieved Left Click");
            }

        }

        void OnRightClick()
        {
            ExitCommandMode();
            TooltipManager.Instance.ForceHexTileTooltipUpdate();

        }

        void UpdateCursor (HexTile tile)
        {
            if (mouse.CurrentTile != null && activeMode)
            {
                mouse.CursorUIIcon = currentCommand.GetUIIcon(tile);
                mouse.CursorTerrainIcon = currentCommand.GetTerrainIcon(tile);

                if (currentCommand.IsValidCommand(tile, activeOwner))
                    mouse.CursorMode = HexCursor.HexCursorMode.Positive;
                else
                    mouse.CursorMode = HexCursor.HexCursorMode.Negative;
            } else if (!activeMode)
            {
                mouse.CursorUIIcon = TileGraphic.Clear;
                mouse.CursorTerrainIcon = TileGraphic.Clear;
                mouse.CursorMode = HexCursor.HexCursorMode.Ring;
            }
        }
    }
}
