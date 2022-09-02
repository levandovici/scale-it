using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private Size[] _sizes = new Size[0];

    [SerializeField]
    private Enemy _enemyPrefab;

    [SerializeField]
    private float _delay = 5f;

    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private Text _score;

    [SerializeField]
    private Text _best_score;

    [SerializeField]
    private Button _play_again;

    [SerializeField]
    private Transform _transform;

    private int _count = 0;

    [SerializeField]
    private Canvas _tutorial;

    [SerializeField]
    private Text _main_score;



    private void Awake()
    {
        DataManager.Initialize();

        _transform.gameObject.SetActive(true);

        _canvas.gameObject.SetActive(false);

        _main_score.gameObject.SetActive(false);
    }



    private void Start()
    {

        _play_again.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        });

        StartCoroutine(Generator());

        StartCoroutine(Tutorial());

        _player.SetUpScales(_sizes[0].size, _sizes[_sizes.Length - 1].size);

        _player.OnAttack += (scale) =>
        {
            int player_id = _sizes.Length - 1;

            int enemy_id = _sizes.Length - 1;

            for (int i = 0; i < _sizes.Length; i++)
            {
                if (_player.transform.localScale.x <= _sizes[i].size)
                {
                    player_id = i;

                    break;
                }
            }

            for (int i = 0; i < _sizes.Length; i++)
            {
                if (scale < _sizes[i].size)
                {
                    enemy_id = i;

                    break;
                }
            }

            if (player_id != enemy_id)
            {
                Destroy(_player.gameObject);

                _canvas.gameObject.SetActive(true);

                _score.text = $"Score: {_count}";

                _best_score.text = $"Best Score: {DataManager.Current.best_score}";

                StopAllCoroutines();

                _transform.gameObject.SetActive(false);

                _main_score.gameObject.SetActive(false);

                _tutorial.gameObject.SetActive(false);

                if (DataManager.Current.best_score < _count)
                {
                    DataManager.Current.best_score = _count;

                    DataManager.Save();
                }
            }
            else
            {
                _count++;
            }
        };
    }



    private void Update()
    {
            for (int i = 0; i < _sizes.Length; i++)
            {
                if (_player.transform.localScale.x <= _sizes[i].size)
                {
                    _player.SetUpColor(_sizes[i].color);

                    break;
                }
            }

        _main_score.text = $"Score: {_count}";
    }

    private IEnumerator Generator()
    {
        while(true)
        {
            float x = UnityEngine.Random.Range(-15f, 15f);

            float z = UnityEngine.Random.Range(-25f, -15f);

            float scale = UnityEngine.Random.Range(_sizes[0].size, _sizes[_sizes.Length - 1].size);

            Enemy enemy = Instantiate(_enemyPrefab, new Vector3(x, scale * 0.5f, z), Quaternion.identity, _transform);

            for (int i = 0; i < _sizes.Length; i++)
            {
                if (scale <= _sizes[i].size)
                {
                    enemy.SetUp(scale, _sizes[i].color);

                    break;
                }
            }

            yield return new WaitForSeconds(_delay);
        }
    }

    private IEnumerator Tutorial()
    {
        yield return new WaitForSeconds(5f / 5f);

        _main_score.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f / 5f * 4f);

        _tutorial.gameObject.SetActive(false);
    }
}
