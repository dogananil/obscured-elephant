using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public string ViewName;

    [SerializeField] Button _closeButton;


    protected virtual void Awake()
    {
        if (_closeButton != null)
        {
            _closeButton.onClick.AddListener(() => Hide().Forget());
        }
    }
    public virtual async UniTask Show()
    {

    }
    public virtual async UniTask Hide()
    {
    }
}
