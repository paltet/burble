using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishManager : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite[] sprites;
    public int state;
    const int STATE_IDLE = 0, STATE_BITE = 1, STATE_KO = 2;

    public float speed;
    private float y_line;

    private float h;
    private float w;
    private float r;
    private float direction;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        state = STATE_IDLE;

        System.Random rand = new System.Random();

        r = UnityEngine.Random.Range(-2, 2);

        direction = rand.Next(2);


        speed = UnityEngine.Random.Range(speed, Mathf.Min(5, 1 + Camera.main.GetComponent<GameManager>().round));
        if (direction == 0) speed = -speed;

        Debug.Log(speed);

        ComputePosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (state != STATE_KO)
        {
            transform.position = new Vector3(transform.position.x + speed * 0.01f, y_line + Mathf.Sin(Time.time) / 2, 0);
            transform.Translate(direction*speed*Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Rotate(0, 0, r);
            transform.localScale = transform.localScale * 0.999f;
        }
        sr.sprite = sprites[state];
        if (direction == 0 && transform.position.x < -w / 2) Destroy(gameObject);
        else if (direction == 1 && transform.position.y > w / 2) Destroy(gameObject);
    }

    void ComputePosition()
    {
        h = Camera.main.orthographicSize * 2;
        w = h * Camera.main.aspect;

        float x = w / 2 + 1;

        if (speed > 0)
        {
            x = -x;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        /*
        float objective_speed = objective.GetComponent<BubbleMovement>().floatspeed;
        float time_to_hit_bubble_x = Mathf.Abs((objective.transform.position.x - x) / speed);

        float y = objective.transform.position.y + objective_speed * 0.005f * time_to_hit_bubble_x;

        if (y > h / 2 || y < -h / 2)
        {
            Debug.Log("Fish out of bounds");
            Destroy(gameObject);
        }*/

        float y = UnityEngine.Random.Range(-h / 2 +1, h / 2 - 1);

        y_line = y;

        transform.position = new Vector3(x, y, 0f);
    }

    public void TriggeredInside(GameObject other)
    {
        if (other.gameObject.name == "bubble(Clone)")
        {
            if (!other.gameObject.GetComponent<BubbleManager>().isBad() && state == STATE_BITE)
            {
                Eat(other.gameObject);
            }
        }
    }

    public void TriggeredOutside(GameObject other)
    {
        if (other.gameObject.name == "bubble(Clone)")
        {
            if (!other.gameObject.GetComponent<BubbleManager>().isBad())
            {
                if (state == STATE_IDLE) state = STATE_BITE;
            }
        }
    }

    private void Eat(GameObject bubbleeaten)
    {
        //Camera.main.GetComponent<GameManager>().MissedBubble();
        BubbleManager bm = bubbleeaten.GetComponent<BubbleManager>();
        if (!bm.popped)
        {
            AppManager.instance.PlayAudio("bite");
            ParticleSystem particle = bubbleeaten.GetComponent<ParticleSystem>();
            bm.content.SetActive(false);
            particle.Play();
            bubbleeaten.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(bubbleeaten, particle.main.duration);
            bm.popped = true;
        }
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 0f) return;
        Die();
    }

    private void Die()
    {
        state = STATE_KO;
        Camera.main.GetComponent<GameManager>().KilledFish();
        rb.velocity = UnityEngine.Random.onUnitSphere * 5;
        StartCoroutine(Kill());
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
