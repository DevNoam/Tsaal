using UnityEngine;

public class Object : MonoBehaviour
{
    public Rigidbody2D rb;
    public int tile;
    public GameObject particle;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();
        int rndColor = Random.Range(0, 3);
    }

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private bool movingTile = false;
    public void Update()
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
                            return;
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
                            return;
                        }
                    }
                }
                else
                {
                    if (currentSwipe.y < 0)
                    {
                        tile = 4;
                        audioManager.Play("Down");
                        rb.gravityScale = 28;
                    }
                    else
                    {
                        //Swipe Up
                    }
                }
                //Swiping Audio

            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && movingTile == false)
        {
            movingTile = true;
            if (tile == 0)
            {
                return;
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
                return;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && movingTile == false)
        {
            tile = 4;
            audioManager.Play("Down");
            movingTile = true;
            rb.gravityScale = 28;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) && movingTile == true)
        {
            movingTile = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (collision.gameObject.tag == this.gameObject.tag)
        {
            gm.addScore(1);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag != this.gameObject.tag && collision.gameObject.tag.ToString().Contains("Container"))
        {
            try
            {
            audioManager.Play("WrongLocation");
            }
            catch (System.Exception)
            {
            }
            gm.HealthChange(-1);
            Instantiate(particle, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
        gm.Spawn();
    }
}
