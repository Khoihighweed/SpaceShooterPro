/*using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3 (adjust in the inspector)
        //when we leave the screen, destroy this object
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if(transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    //OnTriggerCollision
    //Only be collectable by the Player (use tag)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.TripleShotActive();
            }
            Destroy(this.gameObject);
        }
    }
}
