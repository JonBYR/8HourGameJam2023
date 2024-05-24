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
    public static bool held = false;
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
                    newTile.transform.parent = GameObject.Find("TileHolder").GetComponent<Transform>(); //tile is child of player
                    Vector3Int pos = new Vector3Int(Mathf.FloorToInt(playerPosition.position.x), Mathf.FloorToInt(playerPosition.position.y), Mathf.FloorToInt(playerPosition.position.z));
                    tilemap.SetTile(pos, voidTile); //tile at the current player position is now replaced by a void tile
                    Rigidbody2D newRb = newTile.GetComponent<Rigidbody2D>();
                    newRb.isKinematic = true;
                    held = true;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (held == false) return;
            else
            {
                Rigidbody2D newRb = newTile.GetComponent<Rigidbody2D>();
                newRb.isKinematic = false;
                newRb.AddForce(playerPosition.up * speed, ForceMode2D.Impulse);
                held = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (held == false) return;
            else
            {
                Rigidbody2D newRb = newTile.GetComponent<Rigidbody2D>();
                newRb.isKinematic = false;
                newRb.AddForce(playerPosition.right * speed, ForceMode2D.Impulse);
                held = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            if (held == false) return;
            else
            {
                Rigidbody2D newRb = newTile.GetComponent<Rigidbody2D>();
                newRb.isKinematic = false;
                newRb.AddForce(-playerPosition.up * speed, ForceMode2D.Impulse);
                held = false;
            }
        }
        //else if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    if (held == false) return;
        //    else
        //    {
        //        Rigidbody2D newRb = newTile.GetComponent<Rigidbody2D>();
        //        newRb.isKinematic = false;
        //        newRb.AddForce(-playerPosition.right * speed, ForceMode2D.Impulse);
        //        held = false;
        //    }
        //}
    }
}
