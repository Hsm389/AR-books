
using UnityEngine;
using Vuforia;
using UnityEngine.Playables;
using DG.Tweening;
/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class OnlyEnterFirstTrackableEventHandler: MonoBehaviour, ITrackableEventHandler
{
    private GameObject MainScene;
    private PlayableDirector director;
    private GameObject ARCamera;
    protected TrackableBehaviour mTrackableBehaviour;
    protected virtual void Awake()
    {
        MainScene = GameObject.Find("MainScene");
        ARCamera = GameObject.Find("ARCamera");
        ARCamera.transform.localPosition = new Vector3(0.1f, 2.71f, -4.46f);
        ARCamera.transform.DOLocalMove(new Vector3(0.1f, 2.71f, -4.46f), 0.5f);
        ARCamera.transform.DOLocalRotate(new Vector3(11f, 0f, 0f), 0.5f);
        director = FindObjectOfType<PlayableDirector>();
        Debug.Log("Awake Enter");
        MainScene.SetActive(false);
    }
    protected virtual void Start()
    {
       
       // ARCamera.transform.eulerAngles = new Vector3(11f, 0f, 0f);
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }
    
    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
    }

    protected virtual void OnTrackingFound()
    {
        Debug.Log("Found");
        MainScene.SetActive(true);
        director.Play();
    }
    
}
