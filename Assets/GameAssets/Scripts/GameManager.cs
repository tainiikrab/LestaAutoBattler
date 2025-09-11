using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject playerGO;
    [SerializeField] private ActorTypeSO playerActorType;
    private Actor player;
    public static int currentMove { get; private set; } = 1;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        ShowClassSelectionMenu();
        InitializePlayer();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A)) player.DealDamage(player, 1);
#endif
    }

    private void InitializePlayer()
    {
        player = playerGO.AddComponent<Actor>();
        player.Initialize(new Attributes(0, 0, 0), playerActorType);
    }

    private void ShowClassSelectionMenu()
    {
        ;
    }
}