
using UnityEngine;
using TMPro;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameText;
    public GameObject panel;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        scoreText.text =    "Score:" + score.ToString();
        if (score==10)
        {
            panel.transform.DOScale(new Vector3(1f,1f,1f),1f);

            gameText.text = "Win the game";
            Character.Instance.isMove = false;
        }
        else if(Character.Instance.gameOver)
        {
            panel.transform.DOScale(new Vector3(1f,1f,1f),1f);
            gameText.text = "Game Over";
            Character.Instance.isMove = false;
        }
    }
}
