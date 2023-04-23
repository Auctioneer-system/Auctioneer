using UnityEngine;

public class Lineto : MonoBehaviour {

    [SerializeField] LineRenderer line;
    [SerializeField] Collider2D collider2d;

    Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        line.useWorldSpace = true;
    }

    private void Start()
    {
        line.positionCount = 2;
    }

    void Update ()
    {
        var distance2d = rigidbody2d.Distance(collider2d);
        line.SetPosition(0, distance2d.pointA);
        line.SetPosition(1, distance2d.pointB);
    }
}