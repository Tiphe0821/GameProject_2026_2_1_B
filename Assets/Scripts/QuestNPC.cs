using UnityEngine;

public class QuestNPC : MonoBehaviour
{

    [SerializeField] private QuestManager questManager;

    [SerializeField] private Quest quest;

    private bool playerInNear;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInNear = true; // e 키로 상호작용
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInNear = false;
        }
    }
}
