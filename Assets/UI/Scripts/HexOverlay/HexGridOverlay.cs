using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using DaleranGames.Database;

namespace DaleranGames.UI
{
    public class HexGridOverlay : MonoBehaviour
    {
#pragma warning disable 0649
        [Header("Numbers")]
        [SerializeField]
        string zeroKey;
        TileGraphic zero;
        [SerializeField]
        string oneKey;
        TileGraphic one;
        [SerializeField]
        string twoKey;
        TileGraphic two;
        [SerializeField]
        string threeKey;
        TileGraphic three;
        [SerializeField]
        string fourKey;
        TileGraphic four;
        [SerializeField]
        string fiveKey;
        TileGraphic five;
        [SerializeField]
        string sixKey;
        TileGraphic six;
        [SerializeField]
        string sevenKey;
        TileGraphic seven;
        [SerializeField]
        string eightKey;
        TileGraphic eight;
        [SerializeField]
        string nineKey;
        TileGraphic nine;
        [SerializeField]
        string dashKey;
        TileGraphic dash;
        [SerializeField]
        string plusKey;
        TileGraphic plus;
#pragma warning restore 0649
        HexGrid grid;
        Dictionary<int, int[]> lookupTable;

        // Use this for initialization
        void Awake()
        {
            grid = gameObject.GetRequiredComponent<HexGrid>();
            grid.MeshBuildComplete += OnMeshGenrationComplete;
            lookupTable = new Dictionary<int, int[]>();

            BuildLookupTable();
        }

        private void OnDestroy()
        {
            grid.MeshBuildComplete -= OnMeshGenrationComplete;
        }


        void OnMeshGenrationComplete()
        {
            zero = GameDatabase.Instance.TileGraphics[zeroKey];
            one = GameDatabase.Instance.TileGraphics[oneKey];
            two = GameDatabase.Instance.TileGraphics[twoKey];
            three = GameDatabase.Instance.TileGraphics[threeKey];
            four = GameDatabase.Instance.TileGraphics[fourKey];
            five = GameDatabase.Instance.TileGraphics[fiveKey];
            six = GameDatabase.Instance.TileGraphics[sixKey];
            seven = GameDatabase.Instance.TileGraphics[sevenKey];
            eight = GameDatabase.Instance.TileGraphics[eightKey];
            nine = GameDatabase.Instance.TileGraphics[nineKey];
            dash = GameDatabase.Instance.TileGraphics[dashKey];
            plus = GameDatabase.Instance.TileGraphics[plusKey];
        }

        public void SetLabelIcon (HexTile tile, TileGraphic graphic)
        {
            //iconMesh.UpdateUVBuffer(tile, HexLayers.OverlayIcon, coord);
            tile.UIGraphics.Add(TileLayers.OverlayIcon, graphic);
        }

        public void ClearLabelIcon(HexTile tile)
        {
            tile.UIGraphics.Remove(TileLayers.OverlayIcon);
        }

        public void SetLabelNunmber (HexTile tile, int number)
        {
            if (number < -99)
                Debug.LogError("HexGridOverlay: Trying to set to a number less than -99");
            else if (number > 999)
                Debug.LogError("HexGridOverlay: Trying to set to a number over 999");
            else
            {
                TileGraphic[] graphics = GetDigitCoordArray(number);

                SetDigit1(tile, graphics[0]);
                SetDigit2(tile, graphics[1]);
                SetDigit3(tile, graphics[2]);

            }
        }

        public void ClearLabelNunmber(HexTile tile)
        {
            ClearDigit1(tile);
            ClearDigit2(tile);
            ClearDigit3(tile);
        }

        public void SetDigit1(HexTile tile, TileGraphic graphic)
        {
            ClearDigit1(tile);
            tile.UIGraphics.Add(TileLayers.OverlayDigit1, graphic);
        }

        public void SetDigit2(HexTile tile, TileGraphic graphic)
        {
            ClearDigit2(tile);
            tile.UIGraphics.Add(TileLayers.OverlayDigit2, graphic);
        }

        public void SetDigit3(HexTile tile, TileGraphic graphic)
        {
            ClearDigit3(tile);
            tile.UIGraphics.Add(TileLayers.OverlayDigit3, graphic);
        }

        public void ClearDigit1(HexTile tile)
        {
            tile.UIGraphics.Remove(TileLayers.OverlayDigit1);
        }

        public void ClearDigit2(HexTile tile)
        {
            tile.UIGraphics.Remove(TileLayers.OverlayDigit2);
        }

        public void ClearDigit3(HexTile tile)
        {
            tile.UIGraphics.Remove(TileLayers.OverlayDigit3);
        }

        TileGraphic[] GetDigitCoordArray (int number)
        {
            TileGraphic[] output = { TileGraphic.clear, TileGraphic.clear, TileGraphic.clear };

            if (number < -99)
                Debug.LogError("HexGridOverlay: Trying to set to a number less than -99");
            else if (number > -100 && number < -9)
            {
                int[] digits = GetDigitArray(number);

                output[0] = dash;
                output[1] = ParseDigit(digits[0]);
                output[2] = ParseDigit(digits[1]);
            } else if (number > -10 && number < 0)
            {
                int[] digits = GetDigitArray(number);

                output[1] = dash;
                output[2] = ParseDigit(digits[0]);

            } else if (number > -1 && number < 10)
            {
                int[] digits = GetDigitArray(number);

                output[1] = ParseDigit(digits[0]);
 
            } else if (number > 9 && number < 100)
            {
                int[] digits = GetDigitArray(number);

                output[1] = ParseDigit(digits[0]);
                output[2] = ParseDigit(digits[1]);
            } else if (number > 99 && number < 1000)
            {
                int[] digits = GetDigitArray(number);

                output[0] = ParseDigit(digits[0]);
                output[1] = ParseDigit(digits[1]);
                output[2] = ParseDigit(digits[2]);
            } else
                Debug.LogError("HexGridOverlay: Trying to set to a number over 999");

            return output;
        }

        int[] GetDigitArray(int number)
        {
            int[] output = { 0 };

            if (lookupTable.TryGetValue(number, out output))
                return output;

            return output;
        }

        TileGraphic ParseDigit(int digit)
        {
            switch (digit)
            {
                case 0:
                    return zero;
                case 1:
                    return one;
                case 2:
                    return two;
                case 3:
                    return three;
                case 4:
                    return four;
                case 5:
                    return five;
                case 6:
                    return six;
                case 7:
                    return seven;
                case 8:
                    return eight;
                case 9:
                    return nine;
                default:
                    return TileGraphic.clear;
            }
        }

        void BuildLookupTable()
        {

            for (int i = -99; i < -9; i++)
            {
                int[] digits = new int[2];
                digits[0] = (i / 10) % 10 * -1;
                digits[1] = i % 10 * -1;
                lookupTable.Add(i, digits);
            }
            for (int i=-9; i < 0; i++)
            {
                int[] digits = new int[1];
                digits[0] = i % 10 * -1;
                lookupTable.Add(i, digits);
            }
            for (int i = 0; i < 10; i++)
            {
                int[] digits = new int[1];
                digits[0] = i % 10;
                lookupTable.Add(i, digits);
            }
            for (int i = 10; i < 100; i++)
            {
                int[] digits = new int[2];
                digits[0] = (i / 10) % 10;
                digits[1] = i % 10;
                lookupTable.Add(i, digits);
            }

            for (int i = 100; i < 999; i++)
            {
                int[] digits = new int[3];
                digits[0] = (i / 100) % 10;
                digits[1] = (i / 10) % 10;
                digits[2] = i % 10;
                lookupTable.Add(i, digits);
            }
        }


    }
}
