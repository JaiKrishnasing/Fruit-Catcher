using UnityEngine;

public class SpawnFox : MonoBehaviour
{
    public GameObject fox_parent;
    void Update()
    {
        SpawnObject();
    }

    private void SpawnObject()
    {
        if (LogicManager.instance.playerHasHelper)
        {
            fox_parent.SetActive(true);
        }
        else
        {
            fox_parent.SetActive(false);
        }
    }
}
