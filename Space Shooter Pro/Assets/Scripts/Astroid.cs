
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 35;
    /*[SerializeField]
    private float _fallSpeed = 3;*/

    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;

    private Player _player;
    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }
    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(_rotateSpeed * Time.deltaTime * Vector3.forward);
        /*transform.Translate(_fallSpeed * 3 * Time.deltaTime * Vector3.down);*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Laser"))
        {
            /*_fallSpeed = 0;*/
            Instantiate(_explosion,this.gameObject.transform);

            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject,0.5f);
        }
        if(other.CompareTag("Player"))
        {
            /*_fallSpeed = 0;*/
            Instantiate(_explosion, this.gameObject.transform);
            if(!other.TryGetComponent<Player>(out _player))
            {
                Debug.LogError("cant get player component");
            }
            _player.PLayerHit();
            Destroy(this.gameObject,0.5f);
        }    
    }
    //check for LASER collission (Trigger)
    //instantiate explosion at the position of the astroid (us)
    //Destroy the explosion after 3 second

}
