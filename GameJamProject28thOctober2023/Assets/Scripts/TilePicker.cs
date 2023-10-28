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
            if (play.godMode == false) held = false;
            else
            {
                if(held == false)
                {
                    newTile = Instantiate(tile, playerPosition.position, playerPosition.rotation);
                    newTile.transform.parent = GameObject.Find("Player").GetComponent<Transform>();
                    Vector3Int pos = new Vector3Int(Mathf.FloorToInt(playerPosition.position.x), Mathf.FloorToInt(playerPosition.position.y), Mathf.FloorToInt(playerPosition.position.z));
                    tilemap.SetTile(pos, voidTile);
                    held = true;
                }
                else if(held == true)
                {
                    Rigidbody2D newRb = newTile.GetComponent<Rigidbody2D>();
                    newRb.AddForce(playerPosition.up * speed, ForceMode2D.Impulse);
                    held = false;
                }
            }
        }
    }
}
