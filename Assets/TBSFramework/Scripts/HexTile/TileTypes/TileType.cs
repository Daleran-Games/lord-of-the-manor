using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public abstract class TileType : IDatabaseObject, IType<HexTile>
    {
        [Header("Tile Type Info")]
        [SerializeField]
        protected string name;
        public  virtual string Name { get { return name; } }

        [SerializeField]
        protected int id;
        public virtual int ID { get { return id; } }

        [SerializeField]
        [HideInInspector]
        protected string type = "TileType";
        public virtual string Type { get { return type; } }

        [SerializeField]
        protected string iconName;

        [SerializeField]
        protected TileGraphic iconGraphic;
        public virtual TileGraphic IconGraphic { get { return iconGraphic; } }

        public virtual void OnDatabaseInitialization()
        {
            if (iconName != null)
                iconGraphic = GameDatabase.Instance.TileGraphics[iconName];
            else
                iconGraphic = TileGraphic.clear;
        }

        public virtual void OnActivation(HexTile tile)
        {
            if (GameManager.Instance.CurrentState is PlayState)
            {
                OnGameStart(tile);
            }
        }

        public virtual void OnGameStart(HexTile tile)
        {

        }

        public virtual void OnTurnEnd(BaseTurn turn, HexTile tile)
        {

        }

        public virtual void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {

        }

        public virtual void OnTurnStart(BaseTurn turn, HexTile tile)
        {

        }

        public virtual void OnDeactivation(HexTile tile)
        {

        }

    }
}
