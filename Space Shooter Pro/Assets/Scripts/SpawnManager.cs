using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _tripleShotPUPrefab;

    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerUpContainer;

    [SerializeField]
    private float _enemySpawnRate = 2;


    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTripleShotRoutine());
    }

    //spawn game object every 5 seconds
    //Create a coroutine of type IEnumerator -- yield Events
    //while loop

    private void SpawnPlayer()
    {
        Vector3 playerSpawnPos = new(0, -3, 0);
        GameObject player = Instantiate(_player, playerSpawnPos, Quaternion.identity);
        player.transform.parent = this.transform;
    }
    IEnumerator SpawnEnemyRoutine()
    {
        //while loop (infinite loop)
        //Instantiate enemy prefab
        //yield wait for 5 seconds
        while (_stopSpawning == false)
        {
            Vector3 position = new(Random.Range(-9.22f, 9.22f), 9f);
            GameObject newEnemy = Instantiate(_enemyPrefab,position,Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnRate);
        }
    }

    IEnumerator SpawnTripleShotRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 position = new(Random.Range(-9.22f, 9.22f), 9f);
            GameObject tripleShot = Instantiate(_tripleShotPUPrefab, position, Quaternion.identity);
            tripleShot.transform.parent = _powerUpContainer.transform;

            float _triplePUSpawnRate = Random.Range(7f,10f);
            yield return new WaitForSeconds(_triplePUSpawnRate);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
