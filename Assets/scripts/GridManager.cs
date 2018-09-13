using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The GridManager keeps track of the grid,
/// Where everything is,
/// And the surrounding tiles of the one clicked.
/// </summary>
public class GridManager : MonoBehaviour {

    [Header("Ints")]
    public static int w = 10;
    public static int h = 13;

    [Header("Scripts")]
    public static GameController[,] gameController = new GameController[w, h];

    //This one works together with locateBomb.
    public static void RevealBombs()
    {
        foreach (GameController gc in gameController)
        {
            if (gc.isMine)
            {
                gc.changeSprite(0);
            }
        }
    }

    //This gets called when you click on a bomb, this will make all other bombs appear.
    public static bool locateBomb(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            return gameController[x, y].isMine;
        }
        return false;
    }
    
    /*
     * This part here checks for the neighbours using the if statements below.
     * Looking around all sides of the tile.
     * 
     * (LocateBomb (x, y + 1) is the block above, and so on with the other statements, it goes round clockwise)
     */
    public static int checkNeighbours(int x, int y)
    {
        int count = 0;

        //To reduce number of rows, I use this way of writing down these small if statements.
        if (locateBomb(x, y + 1)) count++;
        if (locateBomb(x + 1, y + 1)) count++;
        if (locateBomb(x + 1, y)) count++;
        if (locateBomb(x + 1, y - 1)) count++;
        if (locateBomb(x, y - 1)) count++;
        if (locateBomb(x - 1, y - 1)) count++;
        if (locateBomb(x - 1, y)) count++;
        if (locateBomb(x - 1, y + 1)) count++;

        return count;
    }

    //A small FloodFill algorithm.
    public static void FloodFill(int x, int y, bool[,] checkedTile)
    {
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            if (checkedTile[x, y])
            {
                return;
            }

            gameController[x, y].changeSprite(checkNeighbours(x, y));
            if (checkNeighbours(x, y) > 0)
            {
                return;
            }

            checkedTile[x, y] = true;

            FloodFill(x - 1, y, checkedTile);
            FloodFill(x + 1, y, checkedTile);
            FloodFill(x, y - 1, checkedTile);
            FloodFill(x, y + 1, checkedTile);
        }
    }
}
