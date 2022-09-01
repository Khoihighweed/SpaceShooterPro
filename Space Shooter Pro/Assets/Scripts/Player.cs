/*using System.Collections;
using System.Collections.Generic;*/
using UnityEditor.Timeline;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public or private reference
    //data type (int, float, bool, string)
    //every variable has a name
    //optional value assigned

    [SerializeField]
    private float _speed = 10f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.1f;
    private float _canFire = -1f;

    [SerializeField]
    private float _playerHP = 3;

    private SpawnManager _spawnManager;

    //===========================================================================
    // Start is called before the first frame update
    void Start()
    {  
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        FireLaser();
    }

    void FireLaser()
    {
        // if i hit the space key
        // spawn gameObject

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.01f, 0), Quaternion.identity);
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        /*transform.Translate(Vector3.right);*/
        /*transform.Translate(new Vector3(1, 0, 0));*/
        /*transform.Translate(new Vector3(-5, 0, 0) * 1 * Time.deltaTime);*/

        //new Vector3(1, 0, 0) * 0 * 3.5f * real time
        /*transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);*/
        /*transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);*/

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(_speed * Time.deltaTime * direction);

        //if player position on the y is greater than 0
        //y position = 0
        //else if position on the y is less than -3.8f
        //y pos = -3.8f

        /*if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4.5f)
        {
            transform.position = new Vector3(transform.position.x, -4.5f, 0);
        }*/

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.5f, 0));

        //if player on the x > 11
        //x pos = -11
        //else if player on the x is less than -11
        //x pos = 11

        if (transform.position.x > 10.5f)
        {
            transform.position = new Vector3(-10.5f, transform.position.y);
        }
        else if (transform.position.x < -10.5f)
        {
            transform.position = new Vector3(10.5f, transform.position.y);
        }
    }
    public void PLayerHit()
    {
        _playerHP--;

        //if player death
        if (_playerHP < 1)
        {
            //Communicate with Spawn Manager
            //Let them know to stop Spawing
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
