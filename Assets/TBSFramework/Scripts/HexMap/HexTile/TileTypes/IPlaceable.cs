namespace DaleranGames.TBSFramework
{
    public interface IPlaceable : ICommandable
    {
        bool CanPlace(HexTile tile);
        void Place(HexTile tile);
    }
}
