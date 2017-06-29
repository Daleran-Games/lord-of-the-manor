using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Game;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public abstract class TileType : IDatabaseObject
    {
        [SerializeField]
        protected string name;
        public  virtual string Name { get { return name; } }

        protected int id;
        public virtual int ID { get { return id; } }

        [SerializeField]
        protected string iconName;
        public string IconName { get { return iconName; } }
        protected TileGraphic iconGraphic;
        public TileGraphic IconGraphic { get { return iconGraphic; } }

        public virtual void OnDatabaseInitialization()
        {
            Debug.Log("Create tile icon " + iconName);
            iconGraphic = GameDatabase.Instance.TileGraphics.Get(iconName);
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

        public virtual void OnTurnChange(BaseTurn turn, HexTile tile)
        {

        }

        public virtual void OnDeactivation(HexTile tile)
        {

        }
    
    }
}
