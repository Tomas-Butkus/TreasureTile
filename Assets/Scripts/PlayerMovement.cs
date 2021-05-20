using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GridManager gridManager;

    private Vector3 lastPlayerPosition;

    private void Start()
    {
        gridManager = GameObject.Find("GridHolder").GetComponent<GridManager>();
    }

    private void Update()
    {
        CheckMovementInput();
    }

    private void CheckMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && CheckIfWalkableTile(transform.position + new Vector3(-1, 0, 0)))
        {
            lastPlayerPosition = transform.position;
            transform.position += new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) && CheckIfWalkableTile(transform.position + new Vector3(1, 0, 0)))
        {
            lastPlayerPosition = transform.position;
            transform.position += new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.W) && CheckIfWalkableTile(transform.position + new Vector3(0, 1, 0)))
        {
            lastPlayerPosition = transform.position;
            transform.position += new Vector3(0, 1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S) && CheckIfWalkableTile(transform.position + new Vector3(0, -1, 0)))
        {
            lastPlayerPosition = transform.position;
            transform.position += new Vector3(0, -1, 0);
        }
    }

    private bool CheckIfWalkableTile(Vector3 transformPosition)
    {
        for (int row = 0; row < gridManager.gridArray.GetLength(0); row++)
        {
            for (int column = 0; column < gridManager.gridArray.GetLength(1); column++)
            {
                if (gridManager.gridArray[row, column].GetXPosition() == transformPosition.x && gridManager.gridArray[row, column].GetYPosition() == transformPosition.y
                && gridManager.gridArray[row, column].GetIsWalkable())
                {
                    return true;
                }
            }
        }
        return false;
    }

    public Vector3 GetLastPlayerPosition()
    {
        return lastPlayerPosition;
    }
}
