using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Rigidbody2D rb;
    public int tile;
    public int HPtoRemove = 3;
    public GameObject particle;


    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    private GameManager gm;

    bool destroied = false;
    [Header("Reasons")]
    public string[] explodedSentense;
    private AudioManager audioManager;


    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();
    }
    void Update()
    {
        Swipe();
    }
    private void OnMouseDown()
    {
        if (destroied == false)
        {
            audioManager.Play("BombOutSide");
            destroied = true;
            //Animation Instantiation;
            gm.Spawn();
            Instantiate(particle, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (destroied == true)
            return;
    }

    private bool movingTile = false;
    public void Swipe()
    {
        if (destroied == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            if (Input.GetMouseButtonUp(0))
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                currentSwipe.Normalize();


                if (!(secondPressPos == firstPressPos))
                {
                    if (Mathf.Abs(currentSwipe.x) > Mathf.Abs(currentSwipe.y))
                    {
                        if (currentSwipe.x < 0)
                        {
                            if (tile == 0)
                            {
                                audioManager.Play("ExitArea");
                                audioManager.Play("BombOutSide");
                                rb.MovePosition(new Vector2(-5f, transform.position.y));
                                gm.Spawn();
                                Destroy(this.gameObject);
                            }
                            else if (tile == 1)
                            {
                                audioManager.Play("Swipe");
                                rb.MovePosition(new Vector2(-1.81f, transform.position.y));
                                tile = 0;
                            }
                            else if (tile == 2)
                            {
                                audioManager.Play("Swipe");
                                rb.MovePosition(new Vector2(0, transform.position.y));
                                tile = 1;
                            }
                        }
                        else
                        {
                            if (tile == 0)
                            {
                                audioManager.Play("Swipe");
                                rb.MovePosition(new Vector2(0, transform.position.y));
                                tile = 1;
                            }
                            else if (tile == 1)
                            {
                                audioManager.Play("Swipe");
                                rb.MovePosition(new Vector2(1.81f, transform.position.y));
                                tile = 2;
                            }
                            else if (tile == 2)
                            {
                                audioManager.Play("ExitArea");
                                audioManager.Play("BombOutSide");
                                rb.MovePosition(new Vector2(5f, transform.position.y));
                                gm.Spawn();
                                Destroy(this.gameObject);
                            }
                        }
                    }
                    else
                    {
                        if (currentSwipe.y > 0)
                        {
                            return;
                            /*
                            movingTile = true;
                            destroied = true;
                            //Animation Instantiation;
                            Instantiate(particle, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                            gm.Spawn();
                            Destroy(this.gameObject); */
                        }
                        else
                        {
                            tile = 4;
                            audioManager.Play("BombFall");
                            Debug.Log("down");
                            rb.gravityScale = 28;
                        }
                    }
                }
            }
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetKeyDown(KeyCode.LeftArrow) && movingTile == false)
            {
                movingTile = true;
                if (tile == 0)
                {
                    audioManager.Play("ExitArea");
                    audioManager.Play("BombOutSide");
                    rb.MovePosition(new Vector2(-5f, transform.position.y));
                    gm.Spawn();
                    Destroy(this.gameObject);
                }
                else if (tile == 1)
                {
                    audioManager.Play("Swipe");
                    rb.MovePosition(new Vector2(-1.81f, transform.position.y));
                    tile = 0;
                }
                else if (tile == 2)
                {
                    audioManager.Play("Swipe");
                    rb.MovePosition(new Vector2(0, transform.position.y));
                    tile = 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && movingTile == false)
            {
                movingTile = true;
                Debug.Log("Right");
                if (tile == 0)
                {
                    audioManager.Play("Swipe");
                    rb.MovePosition(new Vector2(0, transform.position.y));
                    tile = 1;
                }
                else if (tile == 1)
                {
                    audioManager.Play("Swipe");
                    rb.MovePosition(new Vector2(1.81f, transform.position.y));
                    tile = 2;
                }
                else if (tile == 2)
                {
                    audioManager.Play("ExitArea");
                    audioManager.Play("BombOutSide");
                    rb.MovePosition(new Vector2(5f, transform.position.y));
                    gm.Spawn();
                    Destroy(this.gameObject);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && movingTile == false)
            {
                audioManager.Play("BombFall");
                movingTile = true;
                rb.gravityScale = 28;
            }
            else if (Input.GetKeyDown(KeyCode.Space) /*|| Input.GetKeyDown(KeyCode.UpArrow)*/ && movingTile == false)
            {
                audioManager.Play("Bombed");
                movingTile = true;
                destroied = true;
                //Animation Instantiation;
                gm.Spawn();
                Instantiate(particle, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                Destroy(this.gameObject);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) && movingTile == true)
            {
                movingTile = false;
            }
#endif
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != this.gameObject.tag && collision.gameObject.tag.ToString().Contains("Container"))
        {
            try
            {
            audioManager.Play("Bombed");

            }
            catch (System.Exception)
            {
            }
            print("Game is over!");
            gm.HealthChange(HPtoRemove);
            if(gm.HP <= 0)
                gm.GameOver(explodedSentense[Random.Range(0, explodedSentense.Length)]);

            Instantiate(particle, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
