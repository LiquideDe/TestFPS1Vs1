using UnityEngine;

public class ViewCanDestroy : MonoBehaviour
{
    public void DestroyView() => Destroy(gameObject);
}
