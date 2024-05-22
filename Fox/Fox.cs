using UnityEngine;

public class Fox : MonoBehaviour
{
    public GameObject player;
    public GameObject foxGameObject;

    private int moveSpeed = 3;
    private Animator animator;

    private void Start()
    {
        animator = foxGameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        WalkToWordsObject();
    }

    private void WalkToWordsObject()
    {
        GameObject closestGameObject = GetClosestObject();

        if (closestGameObject == null) return;

        // De positie van het dichtstbijzijnde GameObject ophalen (x, y, z)
        Vector3 targetPosition = closestGameObject.transform.position;

        // Controleren of de speler dichter bij het object is dan de vos
        // Zo ja, dan return
        if (player.transform.position.x < targetPosition.x) return;

        // De vos heeft alleen de x positie nodig, de rest is gelijk aan de vos y,z 
        targetPosition.y = transform.position.y; 
        targetPosition.z = transform.position.z;

        // Het horizontale verschil (=xDiff) berekenen tussen de huidige positie en de targetposition
        // xDiff < 0 of xDiff > 0 betekent dat de vos moet lopen richting het object
        float xDiff = transform.position.x - targetPosition.x;
        float tolerance = 0.0001f; 

        if (Mathf.Abs(xDiff) < tolerance)
        {
            xDiff = 0f;
        }

        // De vos is op de targetposition, idle animatie afspelen
        if(xDiff == 0f)
        {
            animator.SetBool("isWalking", false);
        }

        // Naar rechts gespiegeld worden
        if (xDiff > 0f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1);
            animator.SetBool("isWalking", true);
        } // Naar links gespiegeld worden
        else if(xDiff < 0f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            animator.SetBool("isWalking", true);
        }

        // Lopen richting de nieuwe positie
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.position = newPosition;
    }


    // Bereken welk object het dichtst bij de vos is
    private GameObject GetClosestObject()
    {
        // Alleen objecten zoeken die de tag "object" hebben
        GameObject[] objectsToFind = GameObject.FindGameObjectsWithTag("object");
        GameObject closestObject = null;

        // Minimale afstand instellen -> positief oneindig 
        float minDist = Mathf.Infinity;

        // Door alle gevonden objecten gaan
        foreach(GameObject obj in objectsToFind)
        {
            // Afstand bereken tussen de vos en het huidige object
            float dist = Vector3.Distance(transform.position, obj.transform.position);

            // Controleren of de afstand kleiner is dan de minimale afstand 
            if (dist < minDist)
            {
                minDist = dist;
                closestObject = obj;
            }
        }

        return closestObject;
    }
}
