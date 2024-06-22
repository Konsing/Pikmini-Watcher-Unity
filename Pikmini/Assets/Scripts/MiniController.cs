using UnityEngine;
using UnityEngine.AI;

public class MiniController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private ColorBind colorBindings;
    
    private ColorWatcher watcher;
    private PublisherManager publisherManager;

    private float throttle = 3.0f;
    private int groupID = 1;
    private float timeSinceChecked = 0.0f;
    private float pikminTotalLife;
    private float totalLife;

    void Awake()
    {
        SetupComponents();
        AssignRandomGroupID();
        SubscribeToPublisher();
        SetInitialDestination();
        SetRandomTotalLife();
    }

    void Update()
    {
        CheckColorChange();
        UpdatePikminLife();
    }

    private void SetupComponents()
    {
        publisherManager = GameObject.FindGameObjectWithTag("Script Home").GetComponent<PublisherManager>();
        RandomizeBody();
    }

    private void AssignRandomGroupID()
    {
        groupID = Random.Range(1, 4);
    }

    private void SubscribeToPublisher()
    {
        publisherManager.SubscribeToGroup(groupID, OnMoveMessage);
        System.Func<Color> colorRetrievalFunction = GetColorRetrievalFunction(groupID);
        if (colorRetrievalFunction != null)
        {
            watcher = new ColorWatcher(colorRetrievalFunction, ChangeColor);
        }
    }

    private System.Func<Color> GetColorRetrievalFunction(int id)
    {
        switch (id)
        {
            case 1:
                ChangeColor(colorBindings.GetGroup1Color());
                return colorBindings.GetGroup1Color;
            case 2:
                ChangeColor(colorBindings.GetGroup2Color());
                return colorBindings.GetGroup2Color;
            case 3:
                ChangeColor(colorBindings.GetGroup3Color());
                return colorBindings.GetGroup3Color;
            default:
                Debug.LogError("MiniController is Awake but has no valid group.");
                return null;
        }
    }

    private void SetInitialDestination()
    {
        Vector3 randomDestination = new Vector3(Random.Range(-20f, 20f), transform.position.y, Random.Range(-20f, 20f));
        agent.SetDestination(randomDestination);
    }

    private void SetRandomTotalLife()
    {
        totalLife = UnityEngine.Random.Range(10f, 40f);
    }

    private void CheckColorChange()
    {
        if (timeSinceChecked > throttle)
        {
            watcher?.Watch();
            timeSinceChecked = 0.0f;
        }
        else
        {
            timeSinceChecked += Time.deltaTime;
        }
    }

    private void UpdatePikminLife()
    {
        pikminTotalLife += Time.deltaTime;
        if (pikminTotalLife >= totalLife)
        {
            Destroy(gameObject);
        }
    }

    private void ChangeColor(Color color)
    {
        foreach (Transform child in transform)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.SetColor("MainColor", color); 
            }
        }
    }

    private void RandomizeBody()
    {
        float randomScale = Random.Range(0.1f, 1.0f);
        transform.localScale *= randomScale;
    }

    public void OnMoveMessage(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private void OnDestroy()
    {
        publisherManager?.UnsubscribeFromGroup(groupID, OnMoveMessage);
    }
}
