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

    public GameObject objective;

    private void Awake()
    {
        objective = GameObject.Find("bubble(Clone)");
        if (objective == null) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        state = STATE_IDLE;

        ComputePosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed, y_line + Mathf.Sin(Time.time), 0);
        sr.sprite = sprites[state];
    }

    void ComputePosition()
    {
        float h = Camera.main.orthographicSize * 2;
        float w = h * Camera.main.aspect;

        float x = w / 2 + 1;

        if (speed >= 0)
        {
            x = -x;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        float objective_speed = objective.GetComponent<BubbleMovement>().floatspeed;
        float time_to_hit_bubble_x = Mathf.Abs((objective.transform.position.x - x) / speed);

        float y = objective.transform.position.y + objective_speed * 0.005f * time_to_hit_bubble_x;

        if (y > h/2 || y < -h / 2)
        {
            Debug.Log("Fish out of bounds");
            Destroy(gameObject);
        }
        y_line = y;

        transform.position = new Vector3(x, y, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");

        if (other.gameObject.name == "bubble(Clone)")
        {
            Debug.Log("triggered");
            state = STATE_BITE;
        }
    }
}
