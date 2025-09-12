using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject playerGO;
    [SerializeField] private GameObject enemyGO;
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
        if (Input.GetKeyDown(KeyCode.A)) player.TryDealDamage(player, out var damage);
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
    }

    private Attributes GenerateAttributes(int min = 1, int max = 3)
    {
        return new Attributes(Random.Range(min, max + 1), Random.Range(min, max + 1), Random.Range(min, max + 1));
    }

    private void InitializePlayer()
    {
        player = playerGO.AddComponent<Actor>();
        player.Initialize(GenerateAttributes(), playerActorType, enemyGO);
    }

    private void ShowClassSelectionMenu()
    {
        ;
    }
}