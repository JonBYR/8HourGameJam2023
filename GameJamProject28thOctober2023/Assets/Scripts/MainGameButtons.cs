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
    }
}
