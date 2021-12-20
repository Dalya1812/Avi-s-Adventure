using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject avi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = avi.transform.position.x;
        float y = avi.transform.position.y;
        float z = -10f;
        transform.position = new Vector3(x, y, z);
    }
}
