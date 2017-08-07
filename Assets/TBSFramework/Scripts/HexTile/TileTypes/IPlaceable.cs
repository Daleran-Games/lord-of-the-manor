namespace DaleranGames.TBSFramework
{
    public interface IPlaceable
    {
        bool CanPlace(HexTile tile);
        void Place(HexTile tile);
    }
}
