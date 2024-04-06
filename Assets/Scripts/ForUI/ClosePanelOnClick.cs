using UnityEngine;
using UnityEngine.UI;

public class ClosePanelOnClick : MonoBehaviour
{
   [SerializeField] private GameObject panelToControl;
   [SerializeField] private float timeScaleWhenPanelClosed = 1f;
   [SerializeField] private float timeScaleWhenPanelOpen = 0f;

    private void Awake()
    {
                Time.timeScale = timeScaleWhenPanelOpen;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Clicked");

            if (panelToControl == null)
            {
                Debug.LogError("UI panel is not assigned to ClosePanelOnClick script!");
                return;
            }

            if (panelToControl.activeSelf)
            {
                panelToControl.SetActive(false);
                Debug.Log("UI panel closed");
                Time.timeScale = timeScaleWhenPanelClosed;
                Debug.Log("Time scale set to: " + timeScaleWhenPanelClosed);
            }
            else
            {
                panelToControl.SetActive(true);
                Debug.Log("UI panel opened");
                Time.timeScale = timeScaleWhenPanelOpen;
                Debug.Log("Time scale set to: " + timeScaleWhenPanelOpen);
            }
        }
    }
}
