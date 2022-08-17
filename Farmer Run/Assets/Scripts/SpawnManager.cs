using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private GameObject _backGround;

    private int _randomIndex;
    
    private Vector3 _spawnPos = new Vector3(12.5f, 0f, -0.6f);

    public static float repeatRate = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, 2f);
    }

    private void SpawnObstacle()
    {
        if (!PlayerController._gameOver)
        {
            _randomIndex = Random.Range(0, _obstacles.Length);
            GameObject obstacle = Instantiate(_obstacles[_randomIndex], _spawnPos, _obstacles[_randomIndex].transform.rotation);
            //setting of speed
            obstacle.GetComponent<MoveLeft>()._speed = _backGround.GetComponent<MoveLeft>()._speed;
        }
    }
}
