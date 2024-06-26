using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    public string currentColor;
    public SpriteRenderer sr;
    public Color cCyan; 
    public Color cYellow; 
    public Color cPink; 
    public Color cPurple;
    public GameObject deathEffect,colorChangerEffect;
    public CircleCollider2D cc;
    private float gravity = 3f;
    private bool gameEnded = false,setNewHighscore = false;
    private int score = 0,highscore;
    public TMP_Text scoreTxt,highscoreTxt;

    void Start()
    {
        SetRandomColor();
        if(PlayerPrefs.HasKey("Highscore")) highscore = PlayerPrefs.GetInt("Highscore");
        else highscore = 0;
        highscoreTxt.text = "Highscore: " + highscore.ToString();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !gameEnded) 
        {
            if(rb.gravityScale == 0f) rb.gravityScale= gravity;
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "ColorChanger")
        {
            SetRandomColor();
            colorChangerEffect.GetComponent<ParticleSystem>().startColor = sr.color;
            Instantiate(colorChangerEffect,transform.position,transform.rotation);
            Destroy(coll.gameObject);
            return;
        }
        if (coll.tag != currentColor)
        {
            gameEnded = true;
            deathEffect.GetComponent<ParticleSystem>().startColor = sr.color;
            Instantiate(deathEffect,transform.position,transform.rotation);
            if (setNewHighscore)
            {
                highscore = score;
                PlayerPrefs.SetInt("Highscore", highscore);
            }
            StartCoroutine(DelayBeforeReload());
        }
        if (coll.tag == currentColor) 
        {
            score++;
            scoreTxt.text = "Score: " + score.ToString();
            if (score >= highscore) { setNewHighscore = true; highscoreTxt.text = "Highscore: " + score.ToString(); }
        }

    }
    IEnumerator DelayBeforeReload() 
    {
        if(sr!=null) sr.enabled = false;
        if(cc!=null) cc.enabled = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SetRandomColor() 
    {
        int index = Random.Range(0, 4);
        switch (index) 
        {
            case 0: 
                currentColor = "Cyan";
                sr.color = cCyan;
                break;
            case 1: 
                currentColor = "Yellow";
                sr.color = cYellow;
                break;
            case 2: 
                currentColor = "Pink";
                sr.color = cPink;
                break;
            case 3:
                currentColor = "Purple";
                sr.color = cPurple;
                break;
        }
    }
}
