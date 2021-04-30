using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    Camera cam;
    Vector3 spawn;

    public GameObject bubblePrefab;

    public int round;
    public int lifes;

    public int score;
    public int spawned;
    public int missed;
    public int spawned_g;

    public float upperY;
    public float downY;
    private int spawned_at_round_start;
    private int missed_at_round_start;
    private int lifes_at_round_start;
    private int spawned_g_at_round_start;

    private bool in_round;

    private void Awake()
    {
        cam = GetComponent<Camera>();

        ComputePositions();

        round = 0;
        lifes = 3;
        score = 0;
        spawned = 0;
        missed = 0;
        spawned_g = 0;
        in_round = false;
    }

    void Start()
    {
        StartRound();
    }

    // Update is called once per frame
    void Update()
    {
        int n_bubbles = 15 + round * 10;
        if (in_round && spawned_at_round_start + n_bubbles <= spawned)
        {
            CancelInvoke("SpawnBubble");
            in_round = false;
            //Debug.Log("round done");
            StartCoroutine(WaitAndStartRound());
        }

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
    }

    public void LoseLife(int n)
    {
        lifes -= n;
        //check if alive
    }

    void ComputePositions()
    {
        float h = cam.orthographicSize * 2;
        float w = h * cam.aspect;

        spawn = new Vector3(w / 2, -h / 2 - 1, 0);

        downY = spawn.y - 1;
        upperY = -downY;
    }

    void SpawnBubble()
    {
        GameObject newbubble = Instantiate(bubblePrefab);
        newbubble.transform.position = new Vector3(Random.Range(-spawn.x, spawn.x), spawn.y, spawn.z);

        float initial_scale = newbubble.transform.localScale.x;
        float new_scale = Random.Range(Mathf.Max(0.2f, initial_scale - round * 0.025f), initial_scale);
        //Debug.Log(new_scale);
        newbubble.GetComponent<BubbleMovement>().startScale = new Vector3(new_scale, new_scale, 0);

        float initial_speed = newbubble.GetComponent<BubbleMovement>().floatspeed;
        float new_speed = Random.Range(initial_speed, Mathf.Min(5, 1 + round * 1f));
        //Debug.Log(new_speed);
        newbubble.GetComponent<BubbleMovement>().floatspeed = new_speed;

        spawned++;
    }

    void StartRound()
    {
        Debug.Log("round " + round);
        in_round = true;
        spawned_at_round_start = spawned;
        lifes_at_round_start = lifes;
        missed_at_round_start = missed;
        spawned_g_at_round_start = spawned_g;

        float pre_time = 0f;
        if (round == 0) pre_time = 1f;

        float elapsed_between_spawn_time = Mathf.Pow(0.5f, Mathf.Max(round/5, 1));

        InvokeRepeating("SpawnBubble", pre_time, elapsed_between_spawn_time);
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
        yield return new WaitUntil(() => GameObject.Find("bubble(Clone)") == null);
        UpdateDifficulty();
        StartRound();
    }

    void ApplyRoundChanges()
    {
        round++;
        //size-- each round
        //nº bombolles cada ronda
        //velocitat cada 3
        cam.GetComponent<AnaglyphEffect>().strength = round + 1;  
    }
}
