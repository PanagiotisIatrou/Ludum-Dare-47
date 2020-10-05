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
    private static GameObject item;

    public Transform VertexHolder;
    public Vertex upVert;
    public Vertex downVert;
    public Vertex leftVert;
    public Vertex rightVert;

    private Vertex destroyedTrail = null;
    
    public bool Flage = true;
   
    private int destroyedIndex = -1;

    private int lastDestroyedIndex = -1;
    private List <SpriteRenderer> spriteRenderer= new List<SpriteRenderer>(5);
    public Sprite[] spriteArray;
    private float time = 0f;
    private int random ;
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
        for (int i =0;i < 4; i++)
        {
            Instance.spriteRenderer[i].sprite = Instance.spriteArray[0];
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
                    
                    
                item = Instantiate(Instance.prefab, Instance.upVert.transform.position, Quaternion.identity);

                yield return  new WaitForSeconds(4);

                Destroy(item);

                Instance.destroyedTrail = Instance.upVert;
                Instance.spriteRenderer[0].sprite = Instance.spriteArray[1];
            }

            else if (r == 1)
            {

                item = Instantiate(Instance.prefab, Instance.downVert.transform.position, Quaternion.identity);

                yield return new WaitForSeconds(4);

                Destroy(item);

                Instance.destroyedTrail = Instance.downVert;
                Instance.spriteRenderer[1].sprite = Instance.spriteArray[1];
            }

            else if (r == 2)
            {
                    
                item = Instantiate(Instance.prefab, Instance.leftVert.transform.position, Quaternion.identity);

                yield return new WaitForSeconds(4);

                Destroy(item);

                Instance.destroyedTrail = Instance.leftVert;
                Instance.spriteRenderer[2].sprite = Instance.spriteArray[1];

            }

            else if (r == 3)
            {

                item = Instantiate(Instance.prefab, Instance.rightVert.transform.position, Quaternion.identity);

                yield return  new WaitForSeconds(4);

                Destroy(item);


                Instance.destroyedTrail = Instance.rightVert;
                Instance.spriteRenderer[3].sprite = Instance.spriteArray[1];
            }
            Instance.destroyedTrail.state = false;
        }
        Instance.Flage = true;

    }

}
