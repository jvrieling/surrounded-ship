using UnityEngine;

public class SpawnBannerAd : MonoBehaviour
{
    public GameObject bannerAd;

    void Start()
    {
        if(BannerAd.Instance == null)
        {
            Instantiate(bannerAd, Vector3.zero, Quaternion.identity);
        } else
        {
            Destroy(gameObject);
        }
    }
}
