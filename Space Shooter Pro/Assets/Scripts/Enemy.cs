/*using System.Collections;
using System.Collections.Generic;*/
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private void Start()
    {
        transform.position = new Vector3(Random.Range(-9.22f,9.22f),7.65f);
    }
    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if bottom of screen
        //respawn at top
        if(transform.position.y < -5.6f)
        {
            float randomX = Random.Range(-9.22f, 9.22f);
            transform.position = new Vector3(randomX, 7.65f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*Debug.Log("Hit: " + other.transform.name);*/
        //if other is Player
        //dammage the player
        //Destroy us
        if(other.CompareTag("Player"))
        {
            //dammage player
            //this is the first way, won't be able to null check
            /*other.transform.GetComponent<Player>().PLayerHit();*/

            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.PLayerHit();
            }
            Destroy(this.gameObject);
        }

        //if other is Laser
        //destroy laser
        //destroy us
        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
