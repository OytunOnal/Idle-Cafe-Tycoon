using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private Vector3 offset = new Vector3(0, 7, -10);
    [SerializeField] private float followSpeed = 5f;

    private void LateUpdate()
    {
        Vector3 targetPosition = playerTransform.position + offset;
        transform.position = Vector3.Slerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
