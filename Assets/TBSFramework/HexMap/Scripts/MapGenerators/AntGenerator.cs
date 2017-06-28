using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewAntGenerator", menuName = "DaleranGames/TBS/Map Generators/Ant", order = 0)]
    public class AntGenerator : MapGenerator
    {
        [Header("Ant Config")]
        [SerializeField]
        [Range(0, 255)]
        protected byte initalElevation = 255;
        public byte InitalElevation { get { return initalElevation; } }


        [SerializeField]
        [Range(0, 255)]
        protected byte initialMoisture = 0;
        public byte InitialMoisture { get { return initialMoisture; } }

        [SerializeField]
        protected AntSpawner[] antSpawners;
        protected bool antsAlive = true;

        public override HexTile[,] GenerateMap()
        {
            HexTile[,] tiles = new HexTile[Width, Height];
            List<Ant> activeAnts = new List<Ant>();
            int id = 0;
            float z = HexMetrics.startingZ;

            for (int y = 0; y < Height ; y++)
            {
                for (int x = 0; x < Width ; x++)
                {
                    tiles[x, y] = CreateTile(x, y, id,z);
                    tiles[x, y].Elevation = InitalElevation;
                    tiles[x, y].Moisture = InitialMoisture;
                    id++;
                    z += HexMetrics.tileSperation;
                }
            }

            SpawnAnts(activeAnts);
            RunSimulation(activeAnts, tiles);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    SetTileType(tiles[x, y]);
                }
            }

            return tiles;
        }

        void SpawnAnts (List<Ant> ants)
        {
            ants.Clear();
            for (int i = 0; i < antSpawners.Length; i++)
            {
                int antsToSpawn = Random.Int(antSpawners[i].minAntCount, antSpawners[i].maxAntCount);
                Vector2Int spawnPoint = new Vector2Int((int)((float)Width * antSpawners[i].positionX), (int)((float)Height * antSpawners[i].positionY));

                for (int j =0; j < antsToSpawn; j++)
                {
                    int minLife = (int)((float)Width * antSpawners[i].minLifespan);
                    int maxLife = (int)((float)Width * antSpawners[i].maxLifespan);

                    int life = Random.Int(minLife, maxLife );
                    int power = Random.Int(antSpawners[i].minPower, antSpawners[i].maxPower);
                    float turn = Random.Float(antSpawners[i].minTurnChance, antSpawners[i].maxTurnCHance);

                    ants.Add(new Ant(spawnPoint, life, power, antSpawners[i].food, turn, antSpawners[i].dieOnBorder));
                }
            }
            //Debug.Log( antSpawners.Length + " spawners created " + ants.Count + " ants.");
        }

        void RunSimulation (List<Ant> ants, HexTile[,] tiles)
        {
            antsAlive = true;

            while (antsAlive)
            {
                MoveAnts(ants, tiles);
                antsAlive = CheckAlive(ants);
            }
        }

        void MoveAnts (List<Ant> ants, HexTile[,] tiles)
        {
            foreach (Ant a in ants)
            {
                if (a.lifespan > 0)
                    a.MoveAnt(tiles);
            }
        }

        bool CheckAlive(List<Ant> ants)
        {
            bool check = false;
            foreach (Ant a in ants)
            {
                if (a.lifespan > 0)
                    check = true;
            }
            return check;

        }

        void CarveRiver ()
        {
            //TODO Add a river ant that runs after the other ants and carves rivers
            //Derrives from normal ant and can move from higher to lower cells or vice versa. It also can eat or grow height
            //Runs after the man simuatlion.
        }


        [System.Serializable]
        public class AntSpawner
        {
            [Range(0,1)]
            public float positionX = 1f;
            [Range(0, 1)]
            public float positionY = 0.5f;


            public float minLifespan = 0.5f;
            public float maxLifespan = 1f;

            public int minAntCount = 3;
            public int maxAntCount = 5;

            public int minPower = 1;
            public int maxPower = 1;

            [Range(0, 1)]
            public float minTurnChance = 0.3f;
            [Range(0, 1)]
            public float maxTurnCHance = 0.7f;

            public AntFood food = AntFood.Elevation;
            public bool dieOnBorder = false;
        }

        [System.Serializable]
        public class Ant
        {
            public Vector2Int position = Vector2Int.zero;
            public int lifespan = 1;
            public int power = 1;
            public AntFood food = AntFood.Elevation;
            public float turnChance = 0.5f;
            public bool dieOnBorder = false;

            public Vector2Int moveDirection = Vector2Int.zero;

            public Ant (Vector2Int position, int life, int power, AntFood antFood, float turn, bool die)
            {
                this.position = position;
                lifespan = life;
                this.power = power;
                food = antFood;
                turnChance = turn;
                dieOnBorder = die;
            }

            public void ModifyTile(HexTile[,] tile)
            {
                if (food == AntFood.Elevation)
                {
                    tile[position.x, position.y].Elevation = (byte) Mathf.Clamp(tile[position.x, position.y].Elevation + power, 0f,255f);
                } else
                {
                    tile[position.x, position.y].Moisture = (byte)Mathf.Clamp(tile[position.x, position.y].Moisture + power, 0f, 255f);            
                }

                lifespan--;
            }

            public void MoveAnt(HexTile[,] tile)
            {
                moveDirection = pickMoveDirection(tile);
                //Debug.Log("Ant at " + position + " moving " + moveDirection);
                position += moveDirection;
                ModifyTile(tile);

                if (dieOnBorder == true && checkIfAtBorder(tile))
                {
                    lifespan = 0;
                }
            }

            bool checkIfAtBorder (HexTile[,] tiles)
            {
                bool border = false;

                if (position.x == 0)
                    border = true;
                if (position.x == tiles.GetLength(0) - 1)
                    border = true;
                if (position.y == 0)
                    border = true;
                if (position.y == tiles.GetLength(1) - 1)
                    border = true;

                return border;
            }

            Vector2Int pickMoveDirection (HexTile[,] tile)
            {
                List<Vector2Int> directions = new List<Vector2Int>();

                if (position.x > 0)
                    directions.Add(Vector2Int.left);
                if (position.x < tile.GetLength(0)-1)
                    directions.Add(Vector2Int.right);
                if (position.y > 0)
                    directions.Add(Vector2Int.down);
                if (position.y < tile.GetLength(1)-1)
                    directions.Add(Vector2Int.up);

                int randomIndex = directions.RandomIndex();

                if (!directions.Contains(moveDirection))
                    return directions[randomIndex];
                else if (Random.Float01() < turnChance)
                    return directions[randomIndex];
                else
                    return moveDirection;
            }
        }

        public enum AntFood
        {
            Elevation,
            Moisture
        }

    }
}
