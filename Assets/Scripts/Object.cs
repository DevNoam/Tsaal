using UnityEngine;

public class Object : MonoBehaviour
{
    public Rigidbody2D rb;
    public int tile;
    public GameObject particle;

    void Start()
    {
         int rndColor = Random.Range(0, 3);
        /*
        if (rndColor == 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            this.gameObject.tag = "Container1";
        }
        else if (rndColor == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            this.gameObject.tag = "Container2";
        }
        else if (rndColor == 2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            this.gameObject.tag = "Container3";
        }*/
    }

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    void Update()
    {
        Swipe();
    }

    private bool movingTile = false;
    public void Swipe()
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
                            rb.MovePosition(new Vector2(-1.81f, transform.position.y));
                            tile = 0;
                        }
                        else if (tile == 2)
                        {
                            rb.MovePosition(new Vector2(0, transform.position.y));
                            tile = 1;
                        }
                    }
                    else
                    {
                        Debug.Log("Right");
                        if (tile == 0)
                        {
                            rb.MovePosition(new Vector2(0, transform.position.y));
                            tile = 1;
                        }
                        else if (tile == 1)
                        {
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
                        Debug.Log("down");
                        rb.gravityScale = 20;
                    }
                    else
                    {
                        //Swipe Up
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
                return;
            }
            else if (tile == 1)
            {
                rb.MovePosition(new Vector2(-1.81f, transform.position.y));
                tile = 0;
            }
            else if (tile == 2)
            {
                rb.MovePosition(new Vector2(0, transform.position.y));
                tile = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && movingTile == false)
        {
            movingTile = true;
            if (tile == 0)
            {
                rb.MovePosition(new Vector2(0, transform.position.y));
                tile = 1;
            }
            else if (tile == 1)
            {
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
            movingTile = true;
            rb.gravityScale = 20;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) && movingTile == true)
        {
            movingTile = false;
        }
#endif
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (collision.gameObject.tag == this.gameObject.tag)
        {
            print("Correct location");
            gm.addScore(1);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag != this.gameObject.tag && collision.gameObject.tag.ToString().Contains("Container"))
        {
            print("Wrong location!");
            gm.HealthChange(-1);
            Instantiate(particle, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
        gm.Spawn();
    }
}
