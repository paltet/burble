using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GameManager : MonoBehaviour
{
    Camera cam;
    Vector3 spawn;
    CameraShaker cs;

    public GameObject bubblePrefab;
    public GameObject bubble_b_Prefab;
    public GameObject fishPrefab;
    public Slider scoreSlider;

    public int round;
    public int lifes;

    public int score_previous;
    public int score;
    public int scorelevel;
    public int score_next_level;

    public int spawned;
    public int missed;
    public int spawned_g;
    public int teeth;
    public int killedfish;

    public float upperY;
    public float downY;
    private int spawned_at_round_start;
    private int missed_at_round_start;
    private int lifes_at_round_start;
    private int spawned_g_at_round_start;

    private bool in_round;
    private bool in_game;


    private void Awake()
    {
        cam = GetComponent<Camera>();
        cs = cam.GetComponent<CameraShaker>();

        ComputePositions();

        in_game = true;
        round = 0;
        lifes = 3;

        if (DataManager.instance.UserSessionHasAchievement("days1")) lifes++;

        score = 0;
        scorelevel = 0;
        score_previous = 0;
        spawned = 0;
        missed = 0;
        spawned_g = 0;
        in_round = false;
        teeth = 0;
        killedfish = 0;

        GameObject.Find("lifes").gameObject.GetComponent<LifeManager>().LoseLife(lifes);
        ComputeNextLevelScore();
        AppManager.instance.StopAllAudio();
    }

    void Start()
    {
        if (DataManager.instance.UserSessionHasSeenTutorial("collectibles"))
        {
            GameObject tutorial = GameObject.Find("Tutorial");
            if (tutorial != null) tutorial.SetActive(false);
            StartRound();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!in_game) return;
        int n_bubbles = Math.Min(15 + round * 5, 40);
        if (in_round && spawned_at_round_start + n_bubbles <= spawned)
        {
            CancelInvoke("SpawnBubble");
            CancelInvoke("SpawnFish");
            in_round = false;
            //Debug.Log("round done");
            StartCoroutine(WaitAndStartRound());
        }

        if (Input.GetKeyDown("space"))
        {
            PauseGame();
        }
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnFish();
        }*/
    }

    public void KilledFish()
    {
        killedfish++;
    }

    public void MissedBubble()
    {
        missed++;
        spawned_g++;
    }

    public void GainScore(int n)
    {
        score += n;
        spawned_g++;
        CheckScore();
    }

    public void LoseLife(int n)
    {
        if (lifes > 0) lifes -= n;
        else return;
        GameObject.Find("lifes").gameObject.GetComponent<LifeManager>().LoseLife(lifes);
        if (lifes <= 0) StartCoroutine(Die());
    }

    public void TeethPopped()
    {
        teeth++;
        AppManager.instance.PlayAudio("yay");
    }

    void ComputePositions()
    {
        float h = cam.orthographicSize * 2;
        float w = h * cam.aspect;

        spawn = new Vector3(w / 2 -0.5f, -h / 2 - 1, 0);

        downY = spawn.y - 1;
        upperY = -downY;
    }

    void SpawnBubble()
    {
        GameObject newbubble;
        if (round == 0) newbubble = Instantiate(bubble_b_Prefab);
        else newbubble = Instantiate(bubblePrefab);

        newbubble.transform.position = new Vector3(UnityEngine.Random.Range(-spawn.x, spawn.x), spawn.y, spawn.z);

        float initial_scale = newbubble.transform.localScale.x;
        float new_scale = UnityEngine.Random.Range(Mathf.Max(0.2f, initial_scale - round * 0.025f), initial_scale);
        //Debug.Log(new_scale);
        newbubble.GetComponent<BubbleMovement>().startScale = new Vector3(new_scale, new_scale, 0);

        float initial_speed = newbubble.GetComponent<BubbleMovement>().floatspeed;
        float new_speed = UnityEngine.Random.Range(initial_speed+round-1, initial_speed+round);
        //Debug.Log(new_speed);
        newbubble.GetComponent<BubbleMovement>().floatspeed = new_speed;

        spawned++;
    }

    void SpawnFish()
    {
        GameObject newfish = Instantiate(fishPrefab);
        Debug.Log("fish spawned");
    }

    void StartRound()
    {
        //Debug.Log("round " + round);
        in_round = true;
        spawned_at_round_start = spawned;
        lifes_at_round_start = lifes;
        missed_at_round_start = missed;
        spawned_g_at_round_start = spawned_g;

        float pre_time = 0f;
        if (round == 0) pre_time = 1f;

        float elapsed_between_spawn_time = Mathf.Pow(0.5f, Mathf.Max(round/5, 1));

        InvokeRepeating("SpawnBubble", pre_time, elapsed_between_spawn_time);
        int n_bubbles = Math.Min(15 + round * 5, 40);
        float round_time = elapsed_between_spawn_time * n_bubbles;

        int invoked_fish = round / 3 + 1;

        if (round != 0) 
            InvokeRepeating("SpawnFish", round_time/ (invoked_fish+1) , round_time / (invoked_fish+1));
    }

    void UpdateDifficulty()
    {
        if (lifes < lifes_at_round_start) return;
        float percent_required = Mathf.Min((50.0f + round * 5.0f) / 100.0f, 0.96f);
        float percent = 1.0f - (((float)missed - (float)missed_at_round_start) / ((float)spawned_g - (float)spawned_g_at_round_start));
        if (percent >= percent_required) ApplyRoundChanges();
    }

    IEnumerator WaitAndStartRound()
    {
        //Debug.Log("start round");
        yield return new WaitUntil(() => GameObject.Find("bubble(Clone)") == null);
        yield return new WaitUntil(() => GameObject.Find("bubble_b(Clone)") == null);
        yield return new WaitForSeconds(2f);
        if (in_game)
        {
            UpdateDifficulty();
            StartRound();
        }
    }

    void ApplyRoundChanges()
    {
        round++;
        cam.GetComponent<AnaglyphEffect>().strength = round * 4f;
        Debug.Log("Round " + round + "  Prismes: " + cam.GetComponent<AnaglyphEffect>().strength);
    }

    IEnumerator Die()
    {
        CancelInvoke();
        in_game = false;
        GameObject bubble = GameObject.Find("bubble(Clone)");
        float time = bubble.GetComponent<ParticleSystem>().main.duration;

        while (bubble != null)
        {
            bubble.GetComponent<ParticleSystem>().Play();
            AppManager.instance.PlayAudio("bubble_pop");
            bubble.transform.GetChild(0).gameObject.SetActive(false);
            bubble.GetComponent<SpriteRenderer>().enabled = false;

            StartCoroutine(Camera.main.GetComponent<CameraShaker>().Shake(time, 0.1f));
            Destroy(bubble, time);
            yield return new WaitForSeconds(time);

            bubble = GameObject.Find("bubble(Clone)");
        }

        EndGame();
    }

    void ComputeNextLevelScore()
    {
        score_next_level = Fib(scorelevel)*10;
    }
    int Fib(int n)
    {
        if (n == 0 || n == 1) return 1;
        else return (Fib(n - 1) + Fib(n - 2));
    }

    void CheckScore()
    {
        int currentlevel = score - score_previous;

        if (currentlevel >= score_next_level)
        {
            scorelevel++;
            score_previous = score_next_level;
            ComputeNextLevelScore();
            scoreSlider.GetComponent<ScoreSlider>().RefreshAll();
        }
        else scoreSlider.GetComponent<ScoreSlider>().Refresh();
    }
    void EndGame()
    {
        GameObject panel = GameObject.Find("HUD").transform.Find("EndGamePanel").gameObject;
        panel.transform.Find("LevelLabel").GetComponent<Text>().text = scorelevel.ToString();
        panel.transform.Find("ScoreLabel").GetComponent<Text>().text = score.ToString();

        panel.SetActive(true);
        GameObject.Find("HUD").transform.Find("Score").gameObject.SetActive(false);

        GameData data = new GameData();
        data.date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        data.score = score;
        data.prisms = cam.GetComponent<AnaglyphEffect>().strength / 4f;
        data.teeth = teeth;
        data.killedfish = killedfish;

        AppManager.instance.AddGameData(data);
    }

    public void GameDone()
    {
        AppManager.instance.LoadScene("main");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameObject panel = GameObject.Find("HUD").transform.Find("PausePanel").gameObject;
        panel.SetActive(true);
    }

    public void Resume()
    {
        GameObject panel = GameObject.Find("HUD").transform.Find("PausePanel").gameObject;
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        GameData data = new GameData();
        data.date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        data.score = score;
        data.prisms = cam.GetComponent<AnaglyphEffect>().strength / 4f;
        data.teeth = teeth;
        AppManager.instance.AddGameData(data);
        GameDone();
    }

    public void CompleteTutorial()
    {
        GameObject tutorial = GameObject.Find("Tutorial");
        if (tutorial != null) tutorial.SetActive(false);
        DataManager.instance.UserSessionCompleteTutorial("game");
        StartRound();
    }
}
