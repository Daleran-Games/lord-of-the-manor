using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaleranGames.IO;
using DaleranGames.TBSFramework;

namespace DaleranGames.UI
{
    public class HexCoordTextBox : MonoBehaviour
    {
        HexCursor cursor;
        Text label;

        void Awake()
        {
            label = GetComponent<Text>();
            cursor = GameObject.FindObjectOfType<HexCursor>();

            cursor.HexTileEntered += OnTileChange;
        }

        void OnDestroy()
        {
            cursor.HexTileEntered += OnTileChange;
        }

        void OnTileChange (HexTile tile)
        {
            if (tile != null)
                label.text = tile.Coordinate.ToString();
        }
    }
}

