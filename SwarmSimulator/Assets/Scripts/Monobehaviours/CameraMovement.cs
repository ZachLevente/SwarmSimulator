using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Something.Controllers
{
    public class CameraMovement : MonoBehaviour
    {
        private float _cameraSpeed = 0.1f;
        private float _panSpeed = 0.7f;

        private Transform _transform;

        private void Update()
        {
            _transform = transform;
            MoveCamera();
            RotateCamera();
            transform.position = _transform.position;
            transform.rotation = _transform.rotation;
        }

        private void MoveCamera()
        {
            var pos = _transform.position;
            
            if (Input.GetKey(KeyCode.W))
            {
                pos += _transform.forward * _cameraSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                pos -= _transform.forward * _cameraSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                pos -= _transform.right * _cameraSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                pos += _transform.right * _cameraSpeed;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                pos += Vector3.up * _cameraSpeed;   
            }
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl))
            {
                pos += Vector3.down * _cameraSpeed;
            }

            _transform.position = pos;
        }

        private void RotateCamera()
        {
            if (Input.GetMouseButton(1))
            {
                _transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * _panSpeed;
            }
        }
    }
}