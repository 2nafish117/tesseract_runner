using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK;
using JMRSDK.InputModule;


namespace JMRSDK.Toolkit
{
    [System.Serializable]
    public enum ScrollBtnType
    {
        Up,
        Down,
        Left,
        Right
    }

    public class JMRScrollBtn : MonoBehaviour, IManipulationHandler
    {
        public JMRCustomScrollRect customScrollRect;

        public ScrollBtnType scrollBtnType;

        Coroutine longPressScrollCoroutine;

        public void OnManipulationStarted(ManipulationEventData eventData)
        {
            longPressScrollCoroutine = StartCoroutine("LongPressScroll");
        }


        public void OnManipulationUpdated(ManipulationEventData eventData)
        {
        }


        public void OnManipulationCompleted(ManipulationEventData eventData)
        {
            StopAllCoroutines();
        }

        private IEnumerator LongPressScroll()
        {
            switch(scrollBtnType)
            {
                case ScrollBtnType.Up:
                    customScrollRect.ProcessVerticalScroll(-0.33f);  
                    break;
                case ScrollBtnType.Down:
                    customScrollRect.ProcessVerticalScroll(0.33f);
                    break;
                case ScrollBtnType.Right:
                    customScrollRect.ProcessHorizontalScroll(-0.63f);
                    break;
                case ScrollBtnType.Left:
                    customScrollRect.ProcessHorizontalScroll(0.63f);

                    break;
            }
            yield return new WaitForSeconds(1f);
            StartCoroutine(LongPressScroll());
        }

    }

    
}