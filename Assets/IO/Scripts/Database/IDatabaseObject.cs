using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.IO
{
    public interface IDatabaseObject
    {
        string Name { get; }
        int ID { get; }
        string Type { get; }
        void OnDatabaseInitialization();
    }
}

