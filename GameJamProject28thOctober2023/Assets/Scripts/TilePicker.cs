using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TilePicker : MonoBehaviour
{
    private SpriteRenderer tileSprite;
    public GameObject tile;
    GameObject newTile;
    public Transform playerPosition;
    private PlayerController play;
    float horizontal;
    float vertical;
    float speed = 10f;
    bool held = false;
    private Tilemap tilemap;
    public Tile voidTile;
    // Start is called before the first frame update
    void Start()
    {
        tileSprite = tile.GetComponent<SpriteRenderer>();
        play = GameObject.Find("Player").GetComponent<PlayerController>();
        tilemap = GameObject.Find("Floors").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Input");
            if (play.godMode == false) held = false; //cannot hold a tile while not in god mode
            else
            {
                if(held == false) //if not holding a tile
                {
                    newTile = Instantiate(tile, playerPosition.position, playerPosition.rotation); //instantiate a tile prefab at the player position
                    newTile.transform.parent = GameObject.Find("Player").GetComponent<Transform>(); //tile is parent of player
                    Vector3Int pos = new Vector3Int(Mathf.FloorToInt(playerPosition.position.x), Mathf.FloorToInt(playerPosition.position.y), Mathf.FloorToInt(playerPosition.position.z));
                    tilemap.SetTile(pos, voidTile); //tile at the current player position is now replaced by a void tile
                    held = true;
                }
                else if(held == true)
                {
                    Rigidbody2D newRb = newTile.GetComponent<Rigidbody2D>();
                    newRb.AddForce(playerPosition.up * speed, ForceMode2D.Impulse); //add force to tile so it works as a bullet
                    held = false;
                }
            }
        }
    }
}
