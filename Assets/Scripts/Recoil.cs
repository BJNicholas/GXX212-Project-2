using UnityEngine;

public class Recoil : MonoBehaviour
{
    public static Recoil instance;
    Vector3 currentRotation;
    Vector3 targetRotation;

    public Vector3 recoil;

    [Header("Settings")]
    public float snappiness;
    public float returnSpeed;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(recoil.x, Random.Range(-recoil.y, recoil.y), Random.Range(-recoil.z, recoil.z));
    }
}
