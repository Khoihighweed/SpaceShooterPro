/*using System.Collections;
using System.Collections.Generic;*/
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //public or private reference
    //data type (int, float, bool, string)
    //every variable has a name
    //optional value assigned

    [SerializeField]
    private float _speed = 15f;
    private float _speedMultiplier = 1.5f;

    //laser
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.1f;
    private float _canFire = -1f;

    [SerializeField]
    private int _playerHP = 3;

    private SpawnManager _spawnManager;

    //variable for isTripleShotActive
    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    //UI related
    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    [SerializeField]
    private GameObject _rightEngine, _leftEngine;

    //variable to store audio clip
    [SerializeField]
    private AudioClip _laserAudioClip;

    private AudioSource _audioSource;

    //===========================================================================
    // Start is called before the first frame update
    void Start()
    {  
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is Null.");
        }

        if(_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is NULL.");
        }
        else
        {
            _audioSource.clip = _laserAudioClip;
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
            _audioSource.Play();
            _canFire = Time.time + _fireRate;

            if(_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 0.47f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.01f, 0), Quaternion.identity);
            }
        }
        

        //play audio clip
        /*_laserSound.Play();*/
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
        //if shields is active
        //do nothing...
        //deactive shields
        //return;
        if (_isShieldActive == true)
        {
            _shieldVisualizer.SetActive(false);
            _isShieldActive = false;
            return;
        }


        _playerHP--;
        if (_playerHP == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if(_playerHP == 1)
        {
            _leftEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_playerHP);

        //if player death
        if (_playerHP < 1)
        {
            //Communicate with Spawn Manager
            //Let them know to stop Spawing
            _spawnManager.OnPlayerDeath();
            
            Destroy(this.gameObject);
        }
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        //start the power down coroutine for triple shot
        StartCoroutine(TripleShotDown());
    }

    //IEnumerator TripleShotPowerDownRoutine
    //wait 5 second
    //set the triple shot to false
    IEnumerator TripleShotDown()
    {
        yield return new WaitForSeconds(4.0f);
        _isTripleShotActive = false;
    }
    public void SpeedUp()
    {
        _speed *= _speedMultiplier;
        /*Debug.LogError(_speed);*/
        StartCoroutine(SpeedBackToNormal());
    }

    IEnumerator SpeedBackToNormal()
    {
        yield return new WaitForSeconds(4.0f);
        _speed /= _speedMultiplier;
        /*Debug.LogError(_speed);*/

    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }
    
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
        //communicate with the UI to update the score!
    }
}
