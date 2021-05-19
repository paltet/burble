using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite[] sprites;
    public int state;
    const int STATE_IDLE = 0, STATE_BITE = 1, STATE_KO = 2;

    public float speed;
    private float y_line;

    private float h;
    private float r;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        state = STATE_IDLE;

        r = Random.Range(-2, 2);

        ComputePosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (state != STATE_KO)
        {
            transform.position = new Vector3(transform.position.x + speed * 0.01f, y_line + Mathf.Sin(Time.time) / 2, 0);
        }
        else
        {
            transform.Rotate(0, 0, r);
            transform.localScale = transform.localScale * 0.999f;
        }
        sr.sprite = sprites[state];
    }

    void ComputePosition()
    {
        h = Camera.main.orthographicSize * 2;
        float w = h * Camera.main.aspect;

        float x = w / 2 + 1;

        if (speed >= 0)
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

        float y = Random.Range(-h / 2 +1, h / 2 - 1);

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
        Die();
    }

    private void Die()
    {
        state = STATE_KO;
        rb.velocity = Random.onUnitSphere * 5;
        //transform.Rotate(0, 0, 90);
        //Destroy(gameObject);
    }
}
