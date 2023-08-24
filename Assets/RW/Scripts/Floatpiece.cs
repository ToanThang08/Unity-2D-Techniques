

using UnityEngine;
using System.Collections;

public class Floatpiece : MonoBehaviour
{
    // Sound & Animation
    public GameObject handcamObj;
    public GameObject golightObj;
    private SpriteRenderer handcamRenderer;
    private SpriteRenderer golightRenderer;
    private AnimateController handcamAniController;
    private AnimateController golightAniController;
    private SoundController sound;

    // Score
    private ScoreBoard scoreBoard;

    private BuoyancyEffector2D floatEffector;
    public float floatingTime = 0f; // floating duration
    private float runningTime = 0f; // the current duration since startTime
    private float startTime = 0f;

    void Start()
    {
        // Get scoreboard and sound object
        GameObject obj = GameObject.Find("scoreText");
        scoreBoard = obj.GetComponent<ScoreBoard>();
        sound = GameObject.Find("SoundObjects").GetComponent<SoundController>();
        // Animation objects
        handcamRenderer = handcamObj.GetComponent<Renderer>() as SpriteRenderer;
        golightRenderer = golightObj.GetComponent<Renderer>() as SpriteRenderer;
        handcamAniController = handcamObj.GetComponent<AnimateController>();
        golightAniController = golightObj.GetComponent<AnimateController>();

        floatEffector = GetComponent<BuoyancyEffector2D>(); // assign component to this instance
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.name == "ball")
        {
            sound.bonus.Play();
            scoreBoard.gamescore = scoreBoard.gamescore + 10;
            golightRenderer.sprite = golightAniController.spriteSet[0];
            StartCoroutine(BeginFloat());
        }
        // on enter zone, start float
        floatEffector.density = 50;
        // start timer
        if (startTime == 0f)
        {
            startTime = Time.time;
            sound.bonus.Play();
            scoreBoard.gamescore += 10;
            golightRenderer.sprite = golightAniController.spriteSet[0];
            StartCoroutine(BeginFloat());
        }
    }

    IEnumerator BeginFloat()
    {
        while (true)
        {
            // calculate current duration
            runningTime = Time.time - startTime;

            // play animation loop
            int index = (int)Mathf.PingPong(handcamAniController.fps *
                        Time.time, handcamAniController.spriteSet.Length);
            handcamRenderer.sprite = handcamAniController.spriteSet[index];
            yield return new WaitForSeconds(0.1f);

            // when time is up            
            if (runningTime >= floatingTime)
            {
                // stop float and reset timer
                floatEffector.density = 0;
                runningTime = 0f;
                startTime = 0f;

                // stop sound & animation 
                sound.bonus.Stop();
                golightRenderer.sprite = golightAniController.spriteSet[1];
                handcamRenderer.sprite = handcamAniController.spriteSet
                              [handcamAniController.spriteSet.Length - 1];
                break;
            }
        }
    }

}
