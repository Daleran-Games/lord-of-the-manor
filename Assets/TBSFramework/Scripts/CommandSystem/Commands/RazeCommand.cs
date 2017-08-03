using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class RazeCommand : Command
    {
        protected TileGraphic icon;

        public RazeCommand(CSVEntry entry)
        {
            this.id = entry.ID;
            name = entry["name"];
            type = entry["type"];
            commandIconName = entry["workIcon"];
            activity = CommandType.Cancel;
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public override void OnDatabaseInitialization()
        {
            icon = GameDatabase.Instance.TileGraphics["UIAtlas_Highlight_Cross"];
        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return icon;
        }

        public override bool IsValidCommand(HexTile tile)
        {
            if (tile.Improvement != null)
                return true;
            else
                return false;
        }

        public override void PreformCommand(HexTile tile)
        {
            tile.Improvement = null;
        }


    }
}
