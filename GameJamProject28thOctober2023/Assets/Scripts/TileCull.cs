using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TileCull : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy" && !TilePicker.held)
        {
            col.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            Destroy(this.gameObject);
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) SceneManager.LoadScene("WinScene"); //if no enemies are left player wins
        }
        else if(col.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        
    }
}
