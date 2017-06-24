using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using DaleranGames.IO;
using DaleranGames.Database;

namespace DaleranGames.UI
{
    public class BuildButton : MonoBehaviour
    {

        [SerializeField]
        bool buildMode = false;

        [SerializeField]
        string improvementName;

        HexCursor mouse;
        ImprovementType typeToBuild;

        private void Awake()
        {
            mouse = GameObject.FindObjectOfType<HexCursor>();
        }

        public void BuildImprovement (string name)
        {
            buildMode = true;
            improvementName = name;
            mouse.HexTileLMBClicked += OnLeftTileClick;
            InputManager.Instance.RMBClick.MouseButtonUp += OnRightClick;
            typeToBuild = GameDatabase.Instance.GetDatabaseObject<ImprovementType>(improvementName);

            mouse.CursorUIIcon = typeToBuild.IconGraphic;
            mouse.HexTileEntered += OnTileEnter;

            if (typeToBuild.CheckIfCanBuild(mouse.CurrentTile))
                mouse.CursorMode = HexCursor.HexCursorMode.Positive;
            else
                mouse.CursorMode = HexCursor.HexCursorMode.Negative;

        }

        void OnTileEnter (HexTile tile)
        {
            if (typeToBuild.CheckIfCanBuild(tile))
                mouse.CursorMode = HexCursor.HexCursorMode.Positive;
            else
                mouse.CursorMode = HexCursor.HexCursorMode.Negative;
        }

        void OnLeftTileClick (HexTile tile)
        {

            if (tile.Improvement == null && buildMode == true && typeToBuild.CheckIfCanBuild(tile))
                tile.Improvement = typeToBuild;

        }

        void OnRightClick ()
        {
            buildMode = false;
            mouse.CursorUIIcon = Vector2Int.zero;
            mouse.CursorMode = HexCursor.HexCursorMode.Ring;
            typeToBuild = null;
            mouse.HexTileLMBClicked -= OnLeftTileClick;
            mouse.HexTileEntered -= OnTileEnter;
            InputManager.Instance.RMBClick.MouseButtonUp -= OnRightClick;
        }


    }
}

