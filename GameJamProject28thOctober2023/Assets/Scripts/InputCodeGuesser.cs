using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResearchArcade;
public class InputCodeGuesser : MonoBehaviour
{
    List<string> inputCodes = new List<string>();
    InputPasswordGenerator gen;
    private PlayerController cont;
    // Start is called before the first frame update
    void Start()
    {
        inputCodes.Clear();
        gen = GameObject.Find("PassCode").GetComponent<InputPasswordGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputCodes.Count == 4)
        {
            if (gen.passCodeGuesser(inputCodes) == true) this.gameObject.SetActive(false);
            else inputCodes.Clear();
        }
        if (ArcadeInput.Player1.A.Down)
        {
            inputCodes.Add("A");
        }
        else if (ArcadeInput.Player1.B.Down)
        {
            inputCodes.Add("B");
        }
        else if (ArcadeInput.Player1.C.Down)
        {
            inputCodes.Add("C");
        }
        else if (ArcadeInput.Player1.D.Down)
        {
            inputCodes.Add("D");
        }
        else if (ArcadeInput.Player1.E.Down)
        {
            inputCodes.Add("E");
        }
        else if (ArcadeInput.Player1.F.Down)
        {
            inputCodes.Add("F");
        }  
    }
}
