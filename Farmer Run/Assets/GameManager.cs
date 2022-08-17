using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private GameObject _panel;

    [Header("Audio Elements")]
    public AudioSource _audioSource;
    public AudioSource _thisAudioSource;
    public AudioClip _gameOverAudio;
    public AudioClip _jumpAudio;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _thisAudioSource = GetComponent<AudioSource>();
        PlayerController._gameOver = false;

        GetHighScore();
    }

    void Update()
    {
        _scoreText.text = "Score: " + PlayerController.score.ToString();

        SetHighScore();

        if (PlayerController._gameOver)
        {
            _audioSource.Stop();
            _panel.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                Invoke(nameof(RestartGame), 0.3f);
            }
        }
    }

    private void SetHighScore()
    {
        if (PlayerController.score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", PlayerController.score);
        }
    }
    
    private void GetHighScore()
    {
        _highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Physics.gravity /= 1.5f;
        _panel.SetActive(false);
    }
}
