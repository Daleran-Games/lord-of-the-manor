namespace DaleranGames.TBSFramework
{
    public interface ISeasonable : IWorkable
    {
        void WorkSeason(HexTile tile, Seasons season, bool work);
    }
}
