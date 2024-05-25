using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ResearchArcade;
public class MainGameButtons : MonoBehaviour
{
    private PlayerController player;
    public Tilemap groundTiles;
    public Tile ground;
    private TilePicker tilePicker;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        tilePicker = GameObject.Find("TileSpawn").GetComponent<TilePicker>();
    }

    // Update is called once per frame
    void Update()
    {
        bool godMoment = player.godMode;
        if (ArcadeInput.Exit.Down)
        {
            Debug.Log("Quit Application");
            ResearchArcade.Navigation.ExitGame();
        }
        if (!godMoment) return;
        else
        {
            if(ArcadeInput.Player1.F.Down)
            {
                Debug.Log("Input Called");
                tilePicker.removeAllColliders();
                for(int i = groundTiles.cellBounds.min.x; i < groundTiles.cellBounds.max.x; i++)
                {
                    for(int j = groundTiles.cellBounds.min.y; j < groundTiles.cellBounds.max.y; j++)
                    {
                        for(int k = groundTiles.cellBounds.min.z; k < groundTiles.cellBounds.max.z; k++)
                        {
                            if(groundTiles.GetTile(new Vector3Int(i, j, k)) == null)
                            {
                                groundTiles.SetTile(new Vector3Int(i, j, k), ground);
                            }
                        }
                    }
                }
            }
        }
    }
}
