using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private float _spawnRate = 2;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn game object every 5 seconds
    //Create a coroutine of type IEnumerator -- yield Events
    //while loop

    private void SpawnPlayer()
    {
        Vector3 playerSpawnPos = new(0, 0, 0);
        GameObject player = Instantiate(_player, playerSpawnPos, Quaternion.identity);
        player.transform.parent = this.transform;
    }
    IEnumerator SpawnRoutine()
    {
        //while loop (infinite loop)
        //Instantiate enemy prefab
        //yield wait for 5 seconds
        while (_stopSpawning == false)
        {
            Vector3 position = new Vector3(Random.Range(-9.22f, 9.22f), 7.65f);
            GameObject newEnemy = Instantiate(_enemyPrefab,position,Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
