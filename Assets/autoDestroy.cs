using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestroy : MonoBehaviour
{
    [Tooltip("Destroy this Object in X seconds")]
    public float destroyIn = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyThis", destroyIn);
    }
    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
