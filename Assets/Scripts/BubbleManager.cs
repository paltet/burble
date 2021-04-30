using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    GameManager game;
    float upperY, downY;

    public bool popped = false;
    public GameObject[] contents;
    private GameObject content = null;
    //private AudioSource pop;

    ParticleSystem ps;

    private void Awake()
    {
        //pop = GetComponent<AudioSource>();
        ps = GetComponent<ParticleSystem>();

        if (contents.Length != 0)
        {
            content = contents[Random.Range(0, contents.Length)];
            content = Instantiate(content);
            content.transform.localScale = transform.localScale;
            content.transform.position = transform.position;
            content.transform.parent = transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("Main Camera").GetComponent<GameManager>();
        if (game != null)
        {
            upperY = game.upperY;
            downY = game.downY;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (game != null)
        {
            if (transform.position.y > upperY)
            {
                if (content != null)
                {
                    ItemData data = content.GetComponent<ItemData>();
                    if (!data.is_bad) game.MissedBubble();
                }
                Destroy(gameObject);
            }
            if (transform.position.y < downY)
            {
                if (content != null)
                {
                    ItemData data = content.GetComponent<ItemData>();
                    if (data.is_bad)
                    {
                        game.LoseLife(data.lifes);
                    }
                    else
                    {
                        game.GainScore(data.score);
                    }
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        popped = true;
        ps.Play();
        //pop.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }
}
