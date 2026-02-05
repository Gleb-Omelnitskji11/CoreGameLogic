using UnityEngine.EventSystems;

namespace BaseProject.Scripts.Core.Input
{
    public class UnityInputService : IInputService
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        public bool GetClick()
        {
            return GetMouseButtonClick();
        }

        private bool GetMouseButtonClick()
        {
            return UnityEngine.Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject();
        }
#else
        public bool GetClick()
        {
            return GetTouchUp();
        }

        private bool GetTouchUp()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended ||
                    touch.phase == TouchPhase.Canceled)
                {
                    return true;
                }
            }

            return false;
        }
#endif
    }
}