using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

// RequireComponent 要求元件 : 在第一次套用此腳本會同時添加指定元件
// ARRaycastManager 提供的 AR 設限碰撞管理器
[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlaceObject : MonoBehaviour
{
    [Header("要放置的物件")]
    public GameObject goTarget;

    private ARRaycastManager arManager;

    private List<ARRaycastHit> arHit;

    private Vector2 posClick;

    private void Start()
    {
        arManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        Tap();
    }

    /// <summary>
    /// 點擊 : 判斷是否觸控，並取得座標進行射線偵測碰撞並產生物件
    /// </summary>
    private void Tap()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            posClick = Input.mousePosition;
            // AR管理器.射線偵測(座標，碰撞清單，碰撞類型)
            arManager.Raycast(posClick, arHit, TrackableType.All);

            Vector3 pos = arHit[0].pose.position;

            GameObject temp = Instantiate(goTarget, pos, Quaternion.identity);

            Vector3 angle = temp.transform.eulerAngles;
            temp.transform.LookAt(transform.position);
            angle.x = 0;
            angle.z = 0;
            temp.transform.eulerAngles = angle;

        }
    }
}
