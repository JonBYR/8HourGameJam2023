using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using ResearchArcade;
public class TilePicker : MonoBehaviour
{
    private SpriteRenderer tileSprite;
    public GameObject tile;
    public Tile ground;
    GameObject newTile;
    public GameObject collisionTile;
    public Transform playerPosition;
    private PlayerController play;
    float horizontal;
    float vertical;
    float speed = 10f;
    public static bool held = false;
    private Tilemap tilemap;
    public NavMeshSurface Surface2D;
    List<GameObject> collidables;
    public AudioSource yank;
    public AudioClip yanky;
    public AudioClip throwy;
    string originalString;
    // Start is called before the first frame update
    void Start()
    {
        Surface2D.BuildNavMesh();
        tileSprite = tile.GetComponent<SpriteRenderer>();
        play = GameObject.Find("Player").GetComponent<PlayerController>();
        tilemap = GameObject.Find("Floors").GetComponent<Tilemap>();
        collidables = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if(ArcadeInput.Player1.A.HeldDown)
        {
            Debug.Log("Input");
            if (play.godMode == false) held = false; //cannot hold a tile while not in god mode
            else
            {
                if(held == false) //if not holding a tile
                {
                    yank.clip = yanky; //set audioclip to be the yank clip 
                    yank.Play();
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
                    newTile.GetComponent<TileCull>().enabled = false;
                    Vector3Int pos = new Vector3Int(Mathf.FloorToInt(playerPosition.position.x), Mathf.FloorToInt(playerPosition.position.y), Mathf.FloorToInt(playerPosition.position.z));
                    tilemap.SetTile(tilemap.WorldToCell(pos), null); //tile at the current player position is now replaced by a void tile
                    GameObject c = Instantiate(collisionTile, pos, Quaternion.identity);
                    collidables.Add(c);
                    Rigidbody2D newRb = newTile.GetComponent<Rigidbody2D>();
                    newRb.isKinematic = true;
                    held = true;
                }
            }
        }
        if(ArcadeInput.Player1.B.HeldDown)
        {
            if (held == false) return;
            else
            {
                originalString = PlayerController.currentDirection;
                yank.clip = throwy;
                yank.Play();
                newTile.GetComponent<TileCull>().enabled = true;
                held = false;
                Rigidbody2D newRb = newTile.GetComponent<Rigidbody2D>();
                newRb.isKinematic = false;
                newRb.AddForce(playerPosition.right * speed, ForceMode2D.Impulse);
                newTile.GetComponent<TileCull>().RemoveTile();
            }
        }
        if (ArcadeInput.Player1.F.HeldDown && play.godMode)
        {
            Debug.Log("Input Called");
            removeAllColliders();
            for (int i = tilemap.cellBounds.min.x; i < tilemap.cellBounds.max.x; i++)
            {
                for (int j = tilemap.cellBounds.min.y; j < tilemap.cellBounds.max.y; j++)
                {
                    for (int k = tilemap.cellBounds.min.z; k < tilemap.cellBounds.max.z; k++)
                    {
                        if (tilemap.GetTile(new Vector3Int(i, j, k)) == null)
                        {
                           tilemap.SetTile(new Vector3Int(i, j, k), ground);
                        }
                    }
                }
            }
        }
    }
    private void LateUpdate()
    {
        Physics2D.SyncTransforms(); //this method should force the update of the navmesh at runtime
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);
    }
    public void removeAllColliders()
    {
        foreach(GameObject c in collidables)
        {
            Destroy(c);
        }
        collidables.Clear();
    }
}
