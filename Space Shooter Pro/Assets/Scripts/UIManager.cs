/*using System.Collections;
using System.Collections.Generic;*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle to Text
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _liveImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        //assign text component to the handle
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        //display img sprite
        //give it a new one based on the currentLives index
        _liveImg.sprite = _liveSprites[currentLives];

        if(currentLives == 0)
        {
            _gameOverText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlicker());
        }
    }

    IEnumerator GameOverFlicker()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
