using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDestroyer : MonoBehaviour
{
    // Singleton
    private static TrailDestroyer _instance;
    public static TrailDestroyer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TrailDestroyer>();
            }

            return _instance;
        }
    }

    public GameObject prefab;
    private GameObject item;

    public Transform VertexHolder;
    public Vertex upVert;
    public Vertex downVert;
    public Vertex leftVert;
    public Vertex rightVert;

    public AudioSource warning;
    public AudioClip explotion;

    private Vertex destroyedTrail = null;

    public bool Flage = true;
    private bool gameOver = false;

    private int destroyedIndex = -1;

    private int lastDestroyedIndex = -1;
    private List<SpriteRenderer> spriteRenderer = new List<SpriteRenderer>(5);
    public Sprite[] spriteArray;
    public ParticleSystem[] ps;
    private float time = 0f;
    private int random;
    public ParticleSystem explotionfactory;
    public ParticleSystem explotionps;
    private void Awake()
    {
        random = Random.Range(3, 5);
        spriteRenderer.Add(VertexHolder.GetChild(2).GetComponent<SpriteRenderer>());
        spriteRenderer.Add(VertexHolder.GetChild(6).GetComponent<SpriteRenderer>());
        spriteRenderer.Add(VertexHolder.GetChild(14).GetComponent<SpriteRenderer>());
        spriteRenderer.Add(VertexHolder.GetChild(10).GetComponent<SpriteRenderer>());

        upVert = VertexHolder.GetChild(2).GetComponent<Vertex>();
        downVert = VertexHolder.GetChild(6).GetComponent<Vertex>();
        leftVert = VertexHolder.GetChild(14).GetComponent<Vertex>();
        rightVert = VertexHolder.GetChild(10).GetComponent<Vertex>();
        for (int i = 0; i < 4; i++)
        {
            Instance.ps[i].Stop();
        }
        Instance.explotionps.Stop();
        Instance.explotionfactory.Stop();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= random)
        {
            random = Random.Range(3, 5);
            time = 0;
            if (TrailDestroyer.Instance.Flage)
            {
                if (TrailDestroyer.IsTrailDestroyed())
                    TrailDestroyer.RestoreTrail();
                TrailDestroyer.DestroyRandomTrail();
            }

        }
    }

    public static void DestroyRandomTrail()
    {
        Instance.StartCoroutine(Create());

    }

    public static bool IsTrailDestroyed()
    {
        return Instance.destroyedIndex != -1;
    }

    public static void RestoreTrail()
    {
        Instance.lastDestroyedIndex = Instance.destroyedIndex;

        Instance.destroyedTrail.state = true;
        for (int i = 0; i < 4; i++)
        {
            Instance.spriteRenderer[i].sprite = Instance.spriteArray[0];
            Instance.ps[i].Stop();
        }

        Instance.destroyedIndex = -1;
        Instance.destroyedTrail = null;
    }

    private static IEnumerator Create()
    {
        Instance.Flage = false;
        int r;
        if (Instance.destroyedIndex == -1)
        {
            r = Random.Range(0, 4);
        }
        else
        {
            if (Instance.lastDestroyedIndex == 0)
                r = Random.Range(1, 4);
            else if (Instance.lastDestroyedIndex == 3)
                r = Random.Range(0, 3);
            else
            {
                int r1 = Random.Range(0, Instance.lastDestroyedIndex);
                int r2 = Random.Range(Instance.lastDestroyedIndex, 4);
                if (Random.Range(0, 2) == 0)
                    r = r1;
                else
                    r = r2;
            }
        }

        int random = Random.Range(0, 5);
        if (random >= 2)
        {

            Instance.destroyedIndex = r;
            if (r == 0)
            {

                Instance.warning.Play();
                Instance.item = Instantiate(Instance.prefab, Instance.upVert.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(4);
                Instance.warning.Stop();

                Destroy(Instance.item);
                AudioSource.PlayClipAtPoint(Instance.explotion, Vector3.zero, 4f);
                Instance.destroyedTrail = Instance.upVert;
                Instance.spriteRenderer[0].sprite = Instance.spriteArray[1];
                Instance.ps[0].Play();
            }

            else if (r == 1)
            {
                Instance.warning.Play();
                Instance.item = Instantiate(Instance.prefab, Instance.downVert.transform.position, Quaternion.identity);

                yield return new WaitForSeconds(4);
                Instance.warning.Stop();

                Destroy(Instance.item);
                AudioSource.PlayClipAtPoint(Instance.explotion, Vector3.zero, 4f);
                Instance.destroyedTrail = Instance.downVert;
                Instance.spriteRenderer[1].sprite = Instance.spriteArray[1];
                Instance.ps[1].Play();
            }

            else if (r == 2)
            {
                Instance.warning.Play();
                Instance.item = Instantiate(Instance.prefab, Instance.leftVert.transform.position, Quaternion.identity);

                yield return new WaitForSeconds(4);
                Instance.warning.Stop();

                Destroy(Instance.item);

                Instance.destroyedTrail = Instance.leftVert;
                Instance.spriteRenderer[2].sprite = Instance.spriteArray[1];
                AudioSource.PlayClipAtPoint(Instance.explotion, Vector3.zero, 4f);
                Instance.ps[2].Play();
            }

            else if (r == 3)
            {
                Instance.warning.Play();
                Instance.item = Instantiate(Instance.prefab, Instance.rightVert.transform.position, Quaternion.identity);

                yield return new WaitForSeconds(4);
                Instance.warning.Stop();

                Destroy(Instance.item);

                AudioSource.PlayClipAtPoint(Instance.explotion, Vector3.zero, 4f);
                Instance.destroyedTrail = Instance.rightVert;
                Instance.spriteRenderer[3].sprite = Instance.spriteArray[1];
                Instance.ps[3].Play();
            }
            Instance.destroyedTrail.state = false;
        }
        Instance.Flage = true;

    }

    public void GameOver(int i)
    {
        if (gameOver)
            return;
        Time.timeScale = 0.5f;
        gameOver = true;
        if (i == 0)
            Instance.explotionps.Play();
        else
            Instance.explotionfactory.Play();
        TrainMovement.StopMoving();
        BlackFader.GoToScene("GameOver", UnityEngine.SceneManagement.LoadSceneMode.Single, 1f);

        // Reset static fields
        Wagon.upgradespeed = 1f;
    }
}
