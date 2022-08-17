using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float _speed = 10f;
    private float _leftBound = -10f;

    [Header("Time Variables")]
    private float _elapsedTime = 0f;
    private float _timer = 0.1f;

    void Update()
    {
        Timer();

        if (!PlayerController._gameOver)
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.left);
        }

        CrossingTheBorderObjects();
    }

    private void CrossingTheBorderObjects()
    {
        if (transform.position.x < _leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    private void Timer()
    {
        if (_elapsedTime < _timer && !PlayerController._gameOver)
        {
            _elapsedTime += Time.deltaTime;
        }
        else
        {
            SetTheGameSpeed();
            _elapsedTime = 0;
        }
    }

    private void SetTheGameSpeed()
    {
        _speed += 0.01f;
        //SpawnManager.repeatRate -= 0.0001f;
    }
}
