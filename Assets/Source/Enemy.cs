using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _target;

    [SerializeField]
    private float _move_speed = 5f;

    private float _scale;



    public void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }



    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _move_speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _target.transform.position) < 0.1f)
        {
            _target.Attack(_scale);

            Destroy(this.gameObject);
        }
    }



    public void SetUp(float scale, Color color)
    {
        _scale = scale;

        transform.position = new Vector3(transform.position.x, _scale * 0.5f, transform.position.z);

        transform.localScale = new Vector3(scale, scale, scale);

        GetComponent<MeshRenderer>().material.color = color;
    }
}
