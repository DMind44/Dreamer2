using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public Sprite levelIcon;
    public int levelWidth;
    public int levelHeight;
    public int levelDepth;
}

public class LevelGrid
{
    public int width;
    public int height;
    public int depth;
    public LevelBlock[][,] grid;
    public LevelGrid(int x, int y, int z)
    {
        width = x;
        height = y;
        depth = z;
        grid = new LevelBlock[height][,];
        for (int i = 0; i < height; i++)
        {
            grid[i] = new LevelBlock[width, depth];
        }
    }

    public LevelBlock this[int x, int y, int z] => grid[y][x, z];
}
