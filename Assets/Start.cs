using UnityEngine;

public class Start : MonoBehaviour
{
    public GameManager gm;
    private bool hasSpawned = false;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            print("Pressed");
        else if (Input.GetMouseButtonUp(0))
        {
            Spawn();
            Destroy(this.gameObject);
        }
    }

    public void Spawn()
    {
        if (hasSpawned == false)
        {
            hasSpawned = true;
            gm.Spawn();
        }
    }
}
