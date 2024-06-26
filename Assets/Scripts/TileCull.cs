using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TileCull : MonoBehaviour
{
    // private void Update()
    // {
        //if (PlayerController.changedInput == true) Destroy(this.gameObject);
    // }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy" && !TilePicker.held)
        {
            col.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            Destroy(this.gameObject);    
        }
        else if(col.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
            TilePicker.held = false;
        }
        
    }
    public void RemoveTile()
    {
        Destroy(this.gameObject, 3f);
        TilePicker.held = false;
    }
}
