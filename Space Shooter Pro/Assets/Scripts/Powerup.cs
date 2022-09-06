/*using System.Collections;
using System.Collections.Generic;*/
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;

    //ID for Powerups;
    //0 = Triple shot
    //1 = Speed
    //2 = Sheilds
    [SerializeField]
    private int _powerupID;

    [SerializeField]
    private AudioClip _audioClip;

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
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch(_powerupID)
                {
                    case 0:
                        {
                            player.TripleShotActive();
                            break;
                        }
                    case 1:
                        {
                            player.SpeedUp();
                            break;
                        }
                    case 2:
                        {
                            player.ShieldActive();
                            break;
                        }
                    default:
                        {
                            Debug.Log("Default Case");
                            break;
                        }
                }
            }
            Destroy(this.gameObject);
        }
    }
}
