using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neighbors;
    public List<Vector2> valid;

    int width = 25;
    int height = 28;

    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D upHit = Physics2D.Raycast(transform.position + Vector3.up * .5f, Vector2.up, height, mask);
        if (upHit != false)
        {
            if (upHit.transform.CompareTag("Node"))
            {
                neighbors.Add(upHit.transform.gameObject.GetComponent<Node>());
                valid.Add(Vector2.up);
            }
        }

        RaycastHit2D downHit = Physics2D.Raycast(transform.position + Vector3.down * .5f, Vector2.down, height, mask);
        if (downHit != false)
        {
            if (downHit.transform.CompareTag("Node"))
            {
                neighbors.Add(downHit.transform.gameObject.GetComponent<Node>());
                valid.Add(Vector2.down);
            }

        }

        RaycastHit2D leftHit = Physics2D.Raycast(transform.position + Vector3.left * .5f, Vector2.left, width, mask);
        if (leftHit != false)
        {
            if (leftHit.transform.CompareTag("Node"))
            {
                neighbors.Add(leftHit.transform.gameObject.GetComponent<Node>());
                valid.Add(Vector2.left);
            }

        }

        RaycastHit2D rightHit = Physics2D.Raycast(transform.position + Vector3.right * .5f, Vector2.right, width, mask);
        if (rightHit != false)
        {
            if (rightHit.transform.CompareTag("Node"))
            {
                neighbors.Add(rightHit.transform.gameObject.GetComponent<Node>());
                valid.Add(Vector2.right);
            }
        }

        valid = new List<Vector2>();

        for (int i = 0; i < neighbors.Count; i++)
        {
            Node neighbor = neighbors[i];
            valid.Add((neighbor.transform.localPosition - transform.localPosition).normalized);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
