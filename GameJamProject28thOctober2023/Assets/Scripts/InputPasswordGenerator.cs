using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class InputPasswordGenerator : MonoBehaviour
{
    private PlayerController cont;
    private List<string> buttons = new List<string>();
    private List<string> existing = new List<string>();
    public AudioSource cursedAudio;
    public AudioSource whimsicalAudio;
    public TMP_Text guessText;
    // Start is called before the first frame update
    void Awake()
    {
        List<string> potentialButtons = new List<string>() { "A", "B", "C", "D", "E" };
        cont = GameObject.Find("Player").GetComponent<PlayerController>();
        for (int i = 0; i < 4; i++)
        {
            int position = Random.Range(0, potentialButtons.Count - 1);
            string button = potentialButtons[position];
            if (existing.Contains(button)) button = "F"; //if the button is present more than once, use button F
            else if(buttons.Contains(button)) existing.Add(button); //if button exists once in code then add to a seperate list that will check if the button is present twice
            buttons.Add(button);
        }
    }

    public bool passCodeGuesser(List<string> playerCombo)
    {
        List<string> correctButtons = new List<string>();
        List<int> correctPositions = new List<int>();
        for(int i = 0; i < buttons.Count; i++)
        {
            if (buttons.Contains(playerCombo[i])) correctButtons.Add(playerCombo[i]);
            if (playerCombo[i] == buttons[i])
            {
                correctPositions.Add(i + 1);
            }
        }
        if(correctPositions.Count == buttons.Count)
        {
            guessText.text = "Code Word guessed! GOD MODE UNLOCKED";
            cont.godMode = true; //activates God Mode in player
            cursedAudio.Pause();
            whimsicalAudio.Play();
            return true;
        }
        else
        {
            if(correctPositions.Count == 0 && correctButtons.Count == 0)
            {
                guessText.text = "No buttons guessed correctly";
                return false;
            }
            else
            {
                int[] correctPos = correctPositions.ToArray();
                string[] correctCodes = correctButtons.ToArray();
                guessText.text = "Correct Positions: " + string.Join(", ", correctPos) + " Correct Buttons in wrong position: " + string.Join(", ", correctCodes);
                return false;
            }
        }
    }
}
