using UnityEngine;
using UnityEngine.UI;

public class GiftScript : MonoBehaviour
{
    public Texture[] giftimages;
    public RawImage AdImage;

    // Start is called before the first frame update
    private void Start()
    {
        int random = (int)Random.Range(0, giftimages.Length-1);
        Debug.Log("Gift " + random+" Selected");
        AdImage.texture = giftimages[random];
     
    }

    // Update is called once per frame
    private void Update()
    {
    }
}