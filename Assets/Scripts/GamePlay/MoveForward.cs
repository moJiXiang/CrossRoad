using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;

    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - startPos.x) > 15f)
        {
            Destroy(this.gameObject);
        }
        Move();
    }

    public void Move()
    {
        transform.position += speed * transform.right * Time.deltaTime;
    }
}
