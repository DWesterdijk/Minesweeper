using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The GameController is what the name says.
/// It controlls the game.
/// </summary>
public class GameController : MonoBehaviour{

    [Header("Booleans")]
    public bool isMine;
    public bool isFlaged;
    public bool isFlagable = true;

    [Header("Sprites")]
    public Sprite[] tiles;
    public Sprite bomb;

    private void Start()
    {
        //RNG
        isMine = Random.value < 0.15;

        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        GridManager.gameController[x, y] = this;
    }

    /*
     * This void is to change the sprites
     * and "int nearBombs" is to keep track of the surrounding bombs.
     * */
    public void changeSprite (int nearBombs)
    {
        if (isMine)
        {
            GetComponent<SpriteRenderer>().sprite = bomb;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = tiles[nearBombs];
        }
    }

    //This is to set the blocks hidden at the start of the scene.
    public bool isHidden()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "HiddenTile";
    }

    private void OnMouseDown()
    {
        if (!isFlaged)
        {
            if (isMine)
            {
                GridManager.RevealBombs();
            }
            else
            {
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;

                changeSprite(GridManager.checkNeighbours(x, y));

                GridManager.FloodFill(x, y, new bool[GridManager.w, GridManager.h]);
                isFlagable = false;
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && isFlagable)
        {
            isFlaged = true;
            isFlagable = false;
            GetComponent<SpriteRenderer>().sprite = tiles[9];
        }
        else if (Input.GetMouseButtonDown(1) && isFlaged == true)
        {
            isFlagable = true;
            isFlaged = false;
            GetComponent<SpriteRenderer>().sprite = tiles[10];
        }
    }
}
