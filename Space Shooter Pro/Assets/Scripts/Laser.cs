/*using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable of 8
    [SerializeField]
    private float _speed = 8;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up);
        if (transform.position.y > 7.02f)
        {
            Destroy(this.gameObject);
        }
    }
}
