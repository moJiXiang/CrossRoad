using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;
    public int dir;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        transform.localScale = new Vector3(dir, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - startPos.x) > 25f)
        {
            Destroy(this.gameObject);
        }
        Move();
    }

    public void Move()
    {
        transform.position += dir * speed * transform.right * Time.deltaTime;
    }
}
