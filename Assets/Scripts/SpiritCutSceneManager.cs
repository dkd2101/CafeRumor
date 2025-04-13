using System.Collections;
using Cinemachine;
using UnityEngine;

public class SpiritCutSceneManager : MonoBehaviour
{
    [Header("Camera Focus Transforms")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform spirit;

    [Header("Cutscene Timing Variables")]
    [SerializeField] private float shakeTime = 2f;
    [SerializeField] private float moveTowardsSpiritTime = 3f;
    [SerializeField] private float spiritFocusTime = 3f;
    [SerializeField] private float moveBackToPlayerTime = 3.5f;
    
    private Transform currentTransform;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineImpulseSource impulseSource;
    private AudioSource audioSource;
    
    private void OnValidate()
    {
        if (player == null || spirit == null)
        {
            Debug.LogWarning("SpiritCutSceneManager: Assign both Player and Spirit transforms in the inspector!", this);
        }
    }

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        audioSource = GetComponent<AudioSource>();
        currentTransform = player;
    }

    public void SetSpiritTransform(Transform transform)
    {
        spirit = transform;
    }

    public void StartAppearance(GameObject spiritObj)
    {
        StartCoroutine(CutScene(spiritObj));
    }

    private IEnumerator CutScene(GameObject spiritObj)
    {
        InputManager.Instance.PauseMovement();
        currentTransform = spirit;
        var transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (transposer != null)
        {
            transposer.m_XDamping = 10f;
            transposer.m_YDamping = 10f;
        }
        virtualCamera.m_Follow = currentTransform;
        
        // wait to center on the spirit
        yield return new WaitForSeconds(moveTowardsSpiritTime);
        
        CameraShake();
        spiritObj.SetActive(true);
        audioSource.Play();
        
        // time focused on spirit
        yield return new WaitForSeconds(spiritFocusTime);
        
        currentTransform = player;
        virtualCamera.m_Follow = currentTransform;
        
        // wait for camera to come back to player
        yield return new WaitForSeconds(moveBackToPlayerTime);
        if (transposer != null)
        {
            transposer.m_XDamping = 1f;
            transposer.m_YDamping = 1f;
        }
        InputManager.Instance.UnPauseMovement();
        yield return null;
    }

    private void CameraShake()
    {
        impulseSource.GenerateImpulseWithForce(shakeTime);
    }
}
