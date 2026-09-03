using UnityEngine;

public class killScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float killTime = 2f;

    private void Start()
    {
        Destroy(gameObject, killTime);
    }


}
