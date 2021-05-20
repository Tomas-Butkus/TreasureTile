public class Tile
{
    private int xPosition;
    private int yPosition;

    private bool isWalkable;
    private bool isOccupied;

    public Tile(int xPosition, int yPosition, bool isWalkable, bool isOccupied)
    {
        this.xPosition = xPosition;
        this.yPosition = yPosition;
        this.isWalkable = isWalkable;
        this.isOccupied = isOccupied;
    }

    public int GetXPosition()
    {
        return xPosition;
    }

    public int GetYPosition()
    {
        return yPosition;
    }

    public bool GetIsWalkable()
    {
        return isWalkable;
    }

    public bool GetIsOccupied()
    {
        return isOccupied;
    }

    public void SetIsOccupied(bool occupiedTile)
    {
        isOccupied = occupiedTile;
    }
}
