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

        protected TileDatabase tileDB;
        protected bool antsAlive = true;

        public override HexCell[,] GenerateMap()
        {
            tileDB = GameDatabase.Instance.TileDB;
            HexCell[,] cells = new HexCell[Width, Height];
            List<Ant> activeAnts = new List<Ant>();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    cells[x, y] = CreateCell(x, y);
                    cells[x, y].Elevation = InitalElevation;
                    cells[x, y].Moisture = InitialMoisture;
                }
            }

            SpawnAnts(activeAnts);
            RunSimulation(activeAnts, cells);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    SetTileType(cells[x, y]);
                }
            }

            return cells;
        }

        void SpawnAnts (List<Ant> ants)
        {
            ants.Clear();
            for (int i = 0; i < antSpawners.Length; i++)
            {
                int antsToSpawn = Random.Range(antSpawners[i].minAntCount, antSpawners[i].maxAntCount);
                Vector2Int spawnPoint = new Vector2Int((int)((float)Width * antSpawners[i].positionX), (int)((float)Height * antSpawners[i].positionY));

                for (int j =0; j < antsToSpawn; j++)
                {
                    int minLife = (int)((float)Width * antSpawners[i].minLifespan);
                    int maxLife = (int)((float)Width * antSpawners[i].maxLifespan);

                    int life = Random.Range(minLife, maxLife );
                    int power = Random.Range(antSpawners[i].minPower, antSpawners[i].maxPower);
                    float turn = Random.Range(antSpawners[i].minTurnChance, antSpawners[i].maxTurnCHance);

                    ants.Add(new Ant(spawnPoint, life, power, antSpawners[i].food, turn, antSpawners[i].dieOnBorder));
                }
            }
            //Debug.Log( antSpawners.Length + " spawners created " + ants.Count + " ants.");
        }

        void RunSimulation (List<Ant> ants, HexCell[,] cells)
        {
            antsAlive = true;

            while (antsAlive)
            {
                MoveAnts(ants, cells);
                antsAlive = CheckAlive(ants);
            }
        }

        void MoveAnts (List<Ant> ants, HexCell[,] cells)
        {
            foreach (Ant a in ants)
            {
                if (a.lifespan > 0)
                    a.MoveAnt(cells);
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

            public void ModifyCell(HexCell[,] cells)
            {
                if (food == AntFood.Elevation)
                {
                    cells[position.x, position.y].Elevation = (byte) Mathf.Clamp(cells[position.x, position.y].Elevation + power, 0f,255f);
                } else
                {
                    cells[position.x, position.y].Moisture = (byte)Mathf.Clamp(cells[position.x, position.y].Moisture + power, 0f, 255f);            
                }

                lifespan--;
            }

            public void MoveAnt(HexCell[,] cells)
            {
                moveDirection = pickMoveDirection(cells);
                //Debug.Log("Ant at " + position + " moving " + moveDirection);
                position += moveDirection;
                ModifyCell(cells);

                if (dieOnBorder == true && checkIfAtBorder(cells))
                {
                    lifespan = 0;
                }
            }

            bool checkIfAtBorder (HexCell[,] cells)
            {
                bool border = false;

                if (position.x == 0)
                    border = true;
                if (position.x == cells.GetLength(0) - 1)
                    border = true;
                if (position.y == 0)
                    border = true;
                if (position.y == cells.GetLength(1) - 1)
                    border = true;

                return border;
            }

            Vector2Int pickMoveDirection (HexCell[,] cells)
            {
                List<Vector2Int> directions = new List<Vector2Int>();

                if (position.x > 0)
                    directions.Add(Vector2Int.left);
                if (position.x < cells.GetLength(0)-1)
                    directions.Add(Vector2Int.right);
                if (position.y > 0)
                    directions.Add(Vector2Int.down);
                if (position.y < cells.GetLength(1)-1)
                    directions.Add(Vector2Int.up);

                int randomIndex = Random.Range(0, directions.Count);

                if (!directions.Contains(moveDirection))
                    return directions[randomIndex];
                else if (Random.Range(0f, 1f) < turnChance)
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
