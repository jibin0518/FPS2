using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class AimManager : MonoBehaviour
{
    AimBase curState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

    [SerializeField] float mouseSense = 1;
    [SerializeField] Transform CamFollowPos;

    float X, Y;

    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineVirtualCamera cam;
    public float AdsFov = 40;

    [HideInInspector] public float HipFov;
    [HideInInspector] public float CurFov;
    public float FovSmoothSpd = 10;

    [SerializeField] Transform AimPos;
    [SerializeField] float AimSmoothSpd = 20;
    [SerializeField] LayerMask AimMask;

    void Start()
    {
        cam = GetComponentInChildren<CinemachineVirtualCamera>();
        HipFov = cam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(Hip);
    }

    // Update is called once per frame
    void Update()
    {
        X += Input.GetAxisRaw("Mouse X") * mouseSense;
        Y -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        Y = Mathf.Clamp(Y, -80, 80);

        cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView , CurFov, FovSmoothSpd * Time.deltaTime);

        curState.UpdateState(this);

        Vector2 screenCen = new Vector2(Screen.width/2, Screen.height/2);
        Ray ray = Camera.main.ScreenPointToRay(screenCen);

        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, AimMask))
        {
            AimPos.position = Vector3.Lerp(AimPos.position, hit.point, AimSmoothSpd * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        CamFollowPos.localEulerAngles = new Vector3(Y, CamFollowPos.localEulerAngles.y, CamFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, X, transform.eulerAngles.z);
    }

    public void SwitchState(AimBase state)
    {
        curState = state;
        curState.EnterState(this);
    }
}
