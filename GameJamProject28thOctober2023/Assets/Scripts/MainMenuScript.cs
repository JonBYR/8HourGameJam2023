using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ResearchArcade;
public class MainMenuScript : MonoBehaviour
{
    public GameObject playButton, quitButton;
    bool playSelected;
    public GameLoader gameLoader;
    private Image playImage;
    private Image quitImage;
    // Start is called before the first frame update
    void Start()
    {
        playSelected = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playButton);
        playImage = playButton.GetComponent<Image>();
        quitImage = quitButton.GetComponent<Image>();
        playImage.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if(ArcadeInput.Player1.JoyRight.HeldDown || ArcadeInput.Player1.JoyLeft.HeldDown)
        {
            MenuNavigator();
        }
        if(ArcadeInput.Player1.A.HeldDown)
        {
            if(playSelected)
            {
                gameLoader.LoadScene("SampleScene");
            }
            else
            {
                ResearchArcade.Navigation.ExitGame();
            }
        }
    }
    void MenuNavigator()
    {
        EventSystem.current.SetSelectedGameObject(null); //needs to clear the current selected object
        if(playSelected)
        {
            EventSystem.current.SetSelectedGameObject(quitButton); //set this to be the new selected game object
            playSelected = false;
            playImage.color = Color.white;
            quitImage.color = Color.green;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(playButton);
            playSelected = true;
            playImage.color = Color.green;
            quitImage.color = Color.white;
        }
    }
}
