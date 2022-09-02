using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _scale_speed = 1f;

    private float _min_scale = 0.5f;

    private float _max_scale = 5f;

    private MeshRenderer _renderer;

    private float _scale = 0f;



    public event Action<float> OnAttack;



    public float Scale => _scale;



    public void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();

        _scale = transform.localScale.x;
    }



    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float normalizedMousePosition = Input.mousePosition.x / Screen.width * 2f - 1f;

            if (normalizedMousePosition < 0f && _scale > _min_scale)
            {
                _scale -= _scale_speed * Time.deltaTime;
            }
            else if (normalizedMousePosition > 0f && _scale < _max_scale)
            {
                _scale += _scale_speed * Time.deltaTime;
            }

            transform.localScale = Vector3.one * _scale;
        }
    }



    public void SetUpColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void SetUpScales(float min, float max)
    {
        _min_scale = min;

        _max_scale = max;
    }

    public void Attack(float scale)
    {
        OnAttack?.Invoke(scale);
    }
}