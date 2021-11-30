using UnityEngine;
using ManAndPig;

namespace ManAndPig.Input
{
    public abstract class InputPresenter<T> : MonoBehaviour
    {
        [SerializeField] private TransformablePresenter<T> _presenter;

        protected TransformablePresenter<T> presenter => _presenter;
    }
}