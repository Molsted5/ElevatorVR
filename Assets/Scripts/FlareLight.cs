using UnityEngine;

public class FlareLight : MonoBehaviour
{
    private Vector3 offsetPosition;

    private void Start() {
        offsetPosition = transform.position - transform.parent.position;
    }

    // after update but before rendering
    private void LateUpdate() {
        // keep light centered with y offset
        // but still moves together with flare model in editor through parrenting

        transform.position = transform.parent.position + offsetPosition;

        transform.rotation = Quaternion.identity; // ignore parent rotation 
    }

}
