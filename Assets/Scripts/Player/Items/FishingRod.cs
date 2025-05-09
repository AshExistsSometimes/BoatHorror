using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    [SerializeField] bool FishingRodEquipped = false;
    [SerializeField] bool RodCastReady = false;
    

    [Header("References")]
    public Transform cam;
    public Transform castPoint;
    public GameObject hook;

    [Header("Settings")]
    public float Cooldown = 0.5f;
    public float MaxCastDistance = 10f;

    [Header("Casting")]
    public KeyCode castKey = KeyCode.Mouse0;
    public float castForce = 10f;
    public float castUpForce = 1.0f;
    public float reelInSpeed = 5.0f;

    [Header("Reel Spin")]
    public GameObject _reel;
    public float SpinSpeed = 100f;
    public float ReelTime = 0.5f;
    public bool ReelSpinning = false;

    private Vector3 lastHookPosition;

    public void EquipRod()
    {
        FishingRodEquipped = true;
        RodCastReady = true;
    }

    private void LateUpdate()
    {

        float distanceFromCastPoint = Vector3.Distance(castPoint.position, hook.transform.position);
        
        if (FishingRodEquipped && RodCastReady && Input.GetKeyDown(castKey))// CAST ROD
        {
            
            SoundManager.PlaySound(SoundType.CAST_ROD, 0.5f);

            Cast();
            StartCoroutine(WaitForCooldown(Cooldown));

            
        }
        else if (FishingRodEquipped && !RodCastReady && Input.GetKeyDown(castKey))// REEL IN
        {
            SoundManager.PlaySound(SoundType.REEL_IN, 0.5f);

            Rigidbody hookRb = hook.GetComponent<Rigidbody>();

            hookRb.useGravity = false;
            hookRb.mass = 0.001f;
            hookRb.drag = 0.0f;
            hookRb.angularDrag = 0.0f;

            hookRb.velocity = Vector3.zero;
            
            StartCoroutine(ReelInHook(Cooldown));
            StartCoroutine(ReelingTime(ReelTime));
            StartCoroutine(WaitForCooldown(Cooldown));

            if (hook.activeSelf)// locks hook at max distance from cast point
            {
                float currentDistance = Vector3.Distance(castPoint.position, hook.transform.position);

                if (currentDistance > MaxCastDistance)
                {
                    Vector3 directionFromCastPoint = (hook.transform.position - castPoint.position).normalized;
                    hook.transform.position = castPoint.position + directionFromCastPoint * MaxCastDistance;
                }

                lastHookPosition = hook.transform.position;
            }

        }

        if (ReelSpinning)
        {
            _reel.transform.Rotate(0f, 0f, SpinSpeed * Time.deltaTime, Space.Self);
            hook.transform.position = Vector3.Lerp(hook.transform.position, castPoint.transform.position, reelInSpeed);
        }

        if (distanceFromCastPoint > MaxCastDistance)
        {
            Vector3 directionFromCastPoint = (hook.transform.position - castPoint.position).normalized;
            hook.transform.position = castPoint.position + directionFromCastPoint * MaxCastDistance;
        }
        lastHookPosition = hook.transform.position;

    }

    private IEnumerator ReelingTime(float reelingTime)
    {
        ReelSpinning = true;
        yield return new WaitForSeconds(reelingTime);
        ReelSpinning = false;
    }

    private IEnumerator WaitForCooldown(float CDTime)
    {
        yield return new WaitForSeconds(CDTime);
    }

    private void Cast()
    {
        RodCastReady = false;

        hook.transform.position = castPoint.position;
        hook.SetActive(true);

        Rigidbody hookRb = hook.GetComponent<Rigidbody>();
        hookRb.useGravity = true;
        hookRb.mass = 1f;
        hookRb.drag = 0.5f;
        hookRb.angularDrag = 0.4f;

        //Add force to hook
        Vector3 forceToAdd = cam.transform.forward * castForce + transform.up * castUpForce;

        hookRb.AddForce(forceToAdd);

    }

    private IEnumerator ReelInHook(float reelingTime)
    {
        yield return new WaitForSeconds(reelingTime);
        hook.SetActive(false);
        RodCastReady = true;
    }
}
