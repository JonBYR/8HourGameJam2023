using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int dam)
    {
        health -= dam;
        if (health < 0)
        {
            if ((GameObject.FindGameObjectsWithTag("Enemy").Length) - 1 == 0)
            {
                Debug.Log("Win Scene Loaded");
                SceneManager.LoadScene("OpeningScene"); //if no enemies are left player wins
            }
            else Destroy(gameObject); 
        }
    }
}
