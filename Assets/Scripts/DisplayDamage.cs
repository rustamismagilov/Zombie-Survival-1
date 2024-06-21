using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Canvas impactCanvas;
    [SerializeField] List<GameObject> impactImages; // List to hold the images
    [SerializeField] float impactTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var image in impactImages)
        {
            image.SetActive(false);
        }
    }

    public void ShowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }

    IEnumerator ShowSplatter()
    {
        int randomIndex = Random.Range(0, impactImages.Count); // Get a random index
        GameObject selectedImage = impactImages[randomIndex];

        selectedImage.SetActive(true); // Enable the random image
        yield return new WaitForSeconds(impactTime);
        selectedImage.SetActive(false); // Disable the image after impactTime
    }
}
