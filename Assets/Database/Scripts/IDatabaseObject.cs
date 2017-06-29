using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.Database
{
    public interface IDatabaseObject
    {
        string Name { get; }
        int ID { get; }
        void OnDatabaseInitialization();
        string ToJson();
    }
}

