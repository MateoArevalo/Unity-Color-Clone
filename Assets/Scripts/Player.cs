using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private float verticalForce = 450f;
    private float restarDelay = 1f;
    [SerializeField] private Color[] colors;
    private string currentColor;
    [SerializeField] private ParticleSystem playerParticles;

    [SerializeField] private AudioClip jumpSound, colorSwitchSound, finishSound, dieSound;

    private Rigidbody2D playerRb;
    private SpriteRenderer playerSr;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSr = GetComponent<SpriteRenderer>();

        ChangeColor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce(new Vector2(0, verticalForce));
        AudioManager.Instance.PlaySound(jumpSound);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("ColorChanger"))
        {
            ChangeColor();
            Destroy(collider.gameObject);
            AudioManager.Instance.PlaySound(colorSwitchSound);
            // termina la funcion, no se va a ejecutar nada mas
            return;
        }
        if (collider.gameObject.CompareTag("FinishLine"))
        {
            gameObject.SetActive(false);
            Instantiate(playerParticles, transform.position, Quaternion.identity);
            Invoke("LoadNextScene", restarDelay);
            AudioManager.Instance.PlaySound(finishSound);
            return;
        }
        if (!collider.gameObject.CompareTag(currentColor))
        {
            gameObject.SetActive(false);
            Instantiate(playerParticles, transform.position, Quaternion.identity);
            Invoke("RestartGame", restarDelay);
            AudioManager.Instance.PlaySound(dieSound);
        }
    }

    private void RestartGame()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    private void LoadNextScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex + 1);
    }

    void ChangeColor()
    {
        int randomNumber = Random.Range(0, 4);
        playerSr.color = colors[randomNumber];

        switch (randomNumber)
        {
            case 0:
                currentColor = "Yellow";
                break;
            case 1:
                currentColor = "Purple";
                break;
            case 2:
                currentColor = "LightBlue";
                break;
            case 3:
                currentColor = "Pink";
                break;
            default:
                break;
        }
    }
}
