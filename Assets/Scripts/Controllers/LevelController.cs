using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers
{ 
    public class LevelGenerator : MonoBehaviour
    {
        public Tilemap tilemap; // Assign your tilemap in the inspector
        public Tile[] tiles; // Assign your tiles in the inspector

        private void Start()
        {
            GenerateLevel();
        }

        void GenerateLevel()
        {
            tilemap.ClearAllTiles(); // Clear the tilemap

            // Define the size of your level
            int width = 10;
            int height = 10;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Randomly choose a tile from the array
                    Tile tile = tiles[Random.Range(0, tiles.Length)];

                    // Set the tile at the x, y position
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }
}