using UnityEngine;
using Pathfinding;

public class windGoBrrr : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float nextWaypointDistance;
    private Path path;
    private int currentWayPoint;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Transform img;
    private float cooldown;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        img = transform.GetChild(0).transform;
        speed = 400f;
        nextWaypointDistance = 1f;
        currentWayPoint = 0;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        cooldown = 3f;
    }
    private void UpdatePath() {
        if(seeker.IsDone()) {
            seeker.StartPath(rb.position, target.position, OnpathComplete);
        }
    }
    private void OnpathComplete(Path p) {
        if(!p.error) {
            path = p;
            currentWayPoint = 0;
        }
    }
    private void FixedUpdate()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0) {
            Destroy(gameObject);
        }
        if(path == null || currentWayPoint >= path.vectorPath.Count) {
            return;
        }
        Vector2 dir = ((Vector2) path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = dir*speed*Time.deltaTime;
        rb.velocity = force; 
        float dis = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        if(dis < nextWaypointDistance) {
            currentWayPoint++;
        }
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
