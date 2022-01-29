using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public Texture[] enemyimages;
    public RawImage AdImage;

    // Start is called before the first frame update
    private void Start()
    {
        int random = (int)Random.Range(0, enemyimages.Length-1);
        Debug.Log("Enemy " + random+" Selected");
        AdImage.texture = enemyimages[random];
     
    }

    // Update is called once per frame
    private void Update()
    {
    }
}