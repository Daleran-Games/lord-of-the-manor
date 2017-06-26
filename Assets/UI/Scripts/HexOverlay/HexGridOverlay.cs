using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;

namespace DaleranGames.UI
{
    public class HexGridOverlay : MonoBehaviour
    {

        [Header("Numbers")]
        [SerializeField]
        Vector2Int zero;
        [SerializeField]
        Vector2Int one;
        [SerializeField]
        Vector2Int two;
        [SerializeField]
        Vector2Int three;
        [SerializeField]
        Vector2Int four;
        [SerializeField]
        Vector2Int five;
        [SerializeField]
        Vector2Int six;
        [SerializeField]
        Vector2Int seven;
        [SerializeField]
        Vector2Int eight;
        [SerializeField]
        Vector2Int nine;
        [SerializeField]
        Vector2Int dash;
        [SerializeField]
        Vector2Int plus;

        HexGrid grid;

        TileAtlas atlas;

        //HexMesh iconMesh;
        //HexMesh digit1;
        //HexMesh digit2;
        //HexMesh digit3;

        Dictionary<int, int[]> lookupTable;

        // Use this for initialization
        void Awake()
        {
            grid = gameObject.GetRequiredComponent<HexGrid>();
            //grid.MeshBuildComplete += OnMeshGenrationComplete;
            lookupTable = new Dictionary<int, int[]>();

            BuildLookupTable();
        }

        private void OnDestroy()
        {
            //grid.MeshBuildComplete -= OnMeshGenrationComplete;
        }

        /*
        void OnMeshGenrationComplete()
        {
            atlas = grid.Generator.Atlas;

            iconMesh = grid.GetHexMeshAtLayer(HexLayers.OverlayIcon);
            digit1 = grid.GetHexMeshAtLayer(HexLayers.OverlayDigit1);
            digit2 = grid.GetHexMeshAtLayer(HexLayers.OverlayDigit2);
            digit3 = grid.GetHexMeshAtLayer(HexLayers.OverlayDigit3);

            //Vector3 iconPosition = new Vector3(iconMesh.transform.position.x,iconMesh.transform.position.y + (12* atlas.SinglePixelInUnity), iconMesh.transform.position.z);
            //Vector3 digit1Position = new Vector3(digit1.transform.position.x - (10 * atlas.SinglePixelInUnity), digit1.transform.position.y - (2 * atlas.SinglePixelInUnity), digit1.transform.position.z);
            //Vector3 digit2Position = new Vector3(digit2.transform.position.x, digit2.transform.position.y - (2 * atlas.SinglePixelInUnity), digit3.transform.position.z);
            //Vector3 digit3Position = new Vector3(digit3.transform.position.x + (10 * atlas.SinglePixelInUnity), digit3.transform.position.y - (2 * atlas.SinglePixelInUnity), digit3.transform.position.z);

            iconMesh.transform.Translate(new Vector3(0f,0.375f,0f));
            digit1.transform.Translate(new Vector3(-0.3125f, -0.0625f, 0f));
            digit2.transform.Translate(new Vector3(0f, -0.0625f, 0f));
            digit3.transform.Translate(new Vector3(0.3125f, -0.0625f, 0f));


            SetLabelNunmber(grid[0, 0], -152);
            SetLabelNunmber(grid[0, 1], -52);
            SetLabelNunmber(grid[0, 2], -4);
            SetLabelNunmber(grid[0, 3], 7);
            SetLabelNunmber(grid[0, 4], 26);
            SetLabelNunmber(grid[0, 5], 576);
            SetLabelNunmber(grid[0, 6], 1087);

        }
        */
        public void SetLabelIcon (HexTile tile, Vector2Int coord)
        {
            //iconMesh.UpdateUVBuffer(tile, HexLayers.OverlayIcon, coord);
            tile.AddGraphic(HexLayers.OverlayIcon, coord);
        }

        public void ClearLabelIcon(HexTile tile)
        {
            tile.RemoveGraphic(HexLayers.OverlayIcon);
        }

        public void SetLabelNunmber (HexTile tile, int number)
        {
            if (number < -99)
                Debug.LogError("HexGridOverlay: Trying to set to a number less than -99");
            else if (number > 999)
                Debug.LogError("HexGridOverlay: Trying to set to a number over 999");
            else
            {
                Vector2Int[] coords = GetDigitCoordArray(number);

                tile.AddGraphic(HexLayers.OverlayDigit1, coords[0]);
                tile.AddGraphic(HexLayers.OverlayDigit2, coords[1]);
                tile.AddGraphic(HexLayers.OverlayDigit3, coords[2]);

                //digit1.UpdateUVBuffer(tile, HexLayers.OverlayDigit1, coords[0]);
                //digit2.UpdateUVBuffer(tile, HexLayers.OverlayDigit2, coords[1]);
                //digit3.UpdateUVBuffer(tile, HexLayers.OverlayDigit3, coords[2]);
            }
        }

        public void ClearLabelNunmber(HexTile tile)
        {
            tile.RemoveGraphic(HexLayers.OverlayDigit1);
            tile.RemoveGraphic(HexLayers.OverlayDigit2);
            tile.RemoveGraphic(HexLayers.OverlayDigit3);

            //digit1.UpdateUVBuffer(tile, HexLayers.OverlayDigit1, Vector2Int.zero);
            //digit2.UpdateUVBuffer(tile, HexLayers.OverlayDigit2, Vector2Int.zero);
            //digit3.UpdateUVBuffer(tile, HexLayers.OverlayDigit3, Vector2Int.zero);
        }

        /*
        public void CommitUVChanges ()
        {
            iconMesh.CommitUVBuffer();
            digit1.CommitUVBuffer();
            digit2.CommitUVBuffer();
            digit3.CommitUVBuffer();
        }
        */

        Vector2Int[] GetDigitCoordArray (int number)
        {
            Vector2Int[] output = { Vector2Int.zero, Vector2Int.zero, Vector2Int.zero };

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

        Vector2Int ParseDigit(int digit)
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
                    return Vector2Int.zero;
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
