using UnityEngine;

public class PikminiSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject pikminiPrefab;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            SpawnPikmini();
        }
    }

    private void SpawnPikmini()
    {
        Instantiate(pikminiPrefab, transform.position, Quaternion.identity);
        FindObjectOfType<SoundManager>().PlaySoundEffect("NewMini");
    }
}