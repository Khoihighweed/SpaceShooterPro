/*using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 9f;

    private Player _player;

    //handle to animator component
    private Animator _enemyDeathAnimator;

    private void Start()
    {
        _player = GameObject.Find("Player(Clone)").GetComponent<Player>();
        //null check player
        //assign the component
        _enemyDeathAnimator = GetComponent<Animator>();
        if(_enemyDeathAnimator == null)
        {
            Debug.LogError("animator get component not working");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        //if bottom of screen
        //respawn at top
        if(transform.position.y < -5.6f)
        {
            float randomX = Random.Range(-9.22f, 9.22f);
            transform.position = new Vector3(randomX, 7.65f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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

            //trigger anim
            _enemyDeathAnimator.SetTrigger("OnEnemyDeath");
            _speed = 5;
            Destroy(this.gameObject, 0.5f);
        }

        //if other is Laser
        //destroy laser
        //destroy us
        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            /*Player player = GameObject.Find("Player").GetComponent<Player>();*/

            if (_player != null)

            {
                _player.AddScore(10);
            }

            //trigger anim
            _enemyDeathAnimator.SetTrigger("OnEnemyDeath");
            _speed = 5;
            Destroy(this.gameObject, 0.5f);
        }
    }
}
