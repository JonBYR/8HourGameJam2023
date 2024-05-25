using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TilePicker : MonoBehaviour
{
    private SpriteRenderer tileSprite;
    public GameObject tile;
    GameObject newTile;
    public GameObject collisionTile;
    public Transform playerPosition;
    private PlayerController play;
    float horizontal;
    float vertical;
    float speed = 10f;
    public static bool held = false;
    private Tilemap tilemap;
    private Tilemap voidMap;
    public Tile voidTile;
    public NavMeshSurface Surface2D;
    // Start is called before the first frame update
    void Start()
    {
        Surface2D.BuildNavMesh();
        tileSprite = tile.GetComponent<SpriteRenderer>();
        play = GameObject.Find("Player").GetComponent<PlayerController>();
        tilemap = GameObject.Find("Floors").GetComponent<Tilemap>();
        voidMap = GameObject.Find("Void").GetComponent<Tilemap>();
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
                    Collider2D[] allCollisions = Physics2D.OverlapCircleAll(playerPosition.position, 0.1f);
                    if(allCollisions.Length != 0)
                    {
                        foreach(var col in allCollisions)
                        {
                            if (col.gameObject.name.Contains("CollisionTile")) return;
                        }
                    }
                    newTile = Instantiate(tile, playerPosition.position, playerPosition.rotation); //instantiate a tile prefab at the player position
                    newTile.transform.parent = GameObject.Find("TileHolder").GetComponent<Transform>(); //tile is child of player
                    Vector3Int pos = new Vector3Int(Mathf.FloorToInt(playerPosition.position.x), Mathf.FloorToInt(playerPosition.position.y), Mathf.FloorToInt(playerPosition.position.z));
                    tilemap.SetTile(tilemap.WorldToCell(pos), null); //tile at the current player position is now replaced by a void tile
                    //voidMap.SetColliderType(voidMap.WorldToCell(pos), Tile.ColliderType.Sprite);
                    //TileBase voidTile = voidMap.GetTile(pos);
                    //voidTile.GetComponent<Collider2D>().enabled = true;     //needs to be investigated more              
                    Instantiate(collisionTile, pos, Quaternion.identity);
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
                newRb.AddForce(playerPosition.right * speed, ForceMode2D.Impulse);
                held = false;
            }
        }
    }
    private void LateUpdate()
    {
        Physics2D.SyncTransforms(); //this method should force the update of the navmesh at runtime
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);
    }
}
