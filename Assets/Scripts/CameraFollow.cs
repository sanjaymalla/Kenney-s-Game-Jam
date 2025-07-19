using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public float xOffset = 1f;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 newpos = new Vector3(target.position.x+xOffset, target.position.y+yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position,newpos,FollowSpeed*Time.deltaTime);
    }
}
