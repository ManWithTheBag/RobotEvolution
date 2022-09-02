using UnityEngine;

public class SurefaceSlider : MonoBehaviour
{
    private Vector3 _normal;

    public Vector3 GetDirectionAlongSurface(Vector3 forvardNormalazed)
    {
        return forvardNormalazed - Vector3.Dot(forvardNormalazed, _normal) * _normal; //  Calculat direction along surface
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Map"))
        {
            _normal = collision.contacts[0].normal;
        }
    }
}
