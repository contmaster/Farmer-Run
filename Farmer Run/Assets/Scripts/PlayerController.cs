using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator _animator;
    [SerializeField] private ParticleSystem _crashParticleEffect;
    [SerializeField] private ParticleSystem _dirtParticleEffect;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _feetPoint;

    [Header("Variables")]
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _gravityModifier;
    
    public bool isOnGround;
    public static bool _gameOver;
    public static int score;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        playerRB.mass = 1;
        Physics.gravity *= _gravityModifier;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !_gameOver)
        {
            playerRB.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _dirtParticleEffect.Stop();
            GameManager.Instance._thisAudioSource.PlayOneShot(GameManager.Instance._jumpAudio, 0.3f);
            _animator.SetTrigger("Jump_trig");
        }
    }

    private bool IsGrounded()
    {
        float radius = 0.1f;
        bool grounded = Physics.CheckSphere(_feetPoint.position, radius, _groundMask);
        return grounded;
    }

    private void OnDrawGizmos()
    {
        float radius = 0.1f;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_feetPoint.position, radius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            _gameOver = true;
            
            GameManager.Instance._thisAudioSource.PlayOneShot(GameManager.Instance._gameOverAudio, 0.2f);
            
            playerRB.mass = 60;
            AnimatorSetting();
            ParticleEffects();
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            _dirtParticleEffect.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            score++;
            Debug.Log("Score: " + score);
        }
    }

    private void AnimatorSetting()
    {
        _animator.SetInteger("DeathType_int", 1);
        _animator.SetBool("Death_b", true);
    }

    private void ParticleEffects()
    {
        _dirtParticleEffect.Stop();
        _crashParticleEffect.Play();
    }
}
