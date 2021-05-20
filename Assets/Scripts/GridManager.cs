using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 5;
    [SerializeField] private int tileSize = 1;
    [SerializeField] private int numberOfEnemies = 2;
    private int enemyCount = 0;

    private Vector3 unoccupiedTilePosition;

    public Tile[,] gridArray;

    private void Start()
    {
        GenerateGrid();
        InstantiateEnemies();
    }

    private void GenerateGrid()
    {
        GameObject grassTile = (GameObject)Instantiate(Resources.Load("Grass_Tile"));
        GameObject waterTile = (GameObject)Instantiate(Resources.Load("Water_Tile"));
        gridArray = new Tile[rows, columns];

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if (Random.Range(1, 10) > 1)
                {
                    gridArray[row, column] = new Tile(column * tileSize, row * -tileSize, true, false);
                    InstantiateTile(grassTile, column, row);
                }
                else
                {
                    gridArray[row, column] = new Tile(column * tileSize, row * -tileSize, false, false);
                    InstantiateTile(waterTile, column, row);
                }
            }
        }
        Destroy(grassTile);
        Destroy(waterTile);
    }

    private void InstantiateTile(GameObject referenceTile, int column, int row)
    {
        GameObject tile = Instantiate(referenceTile, transform);

        float posX = column * tileSize;
        float posY = row * -tileSize;
        tile.transform.position = new Vector2(posX, posY);
    }

    private void InstantiateEnemies()
    {
        while (enemyCount < numberOfEnemies)
        {
            FindUnoccupiedTilePosition();
            GameObject enemy = (GameObject)Instantiate(Resources.Load("EnemyWeak"));
            enemy.transform.position = unoccupiedTilePosition;
            enemyCount++;
        }
    }

    private void FindUnoccupiedTilePosition()
    {
        int randomRow = Random.Range(0, rows);
        int randomColumn = Random.Range(0, columns);

        while (gridArray[randomRow, randomColumn].GetIsOccupied() != false)
        {
            randomRow = Random.Range(0, rows);
            randomColumn = Random.Range(0, columns);
        }
        gridArray[randomRow, randomColumn].SetIsOccupied(true);
        unoccupiedTilePosition = new Vector3(gridArray[randomRow, randomColumn].GetXPosition(), gridArray[randomRow, randomColumn].GetYPosition(), 0);
    }
}
