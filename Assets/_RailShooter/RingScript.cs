using UnityEngine;

public class RingScript : MonoBehaviour
{

    public GameObject ringMesh;
    public Vector3 rotateSpeed;

    private void Update()
    {
        ringMesh.transform.Rotate(rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
