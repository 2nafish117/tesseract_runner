using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using JMRSDK.InputModule;
using System.Collections;
using System.Collections.Generic;
using JMRSDK;
using DG.Tweening;

namespace JMRSDK.Toolkit
{
    public class JMRCustomScrollRect : ScrollRect, ISwipeHandler
    {
        public Button upScrollButton;
        public Button downScrollButton;

        private ScrollRect j_ParentScroll;
        private PointerEventData j_PntrData;
        private Vector2 j_TempVelocity;
        private bool isVerticalScroll;

        protected override void Start()
        {
            base.Start();
            if (!Application.isPlaying)
                return;
            j_ParentScroll = GetScrollParent(transform);
            CreatePointerEventData();
        }

        private void CreatePointerEventData()
        {
            if (j_PntrData != null) { return; }
            StartCoroutine(WaitTillEventSystemLoads());
        }

        IEnumerator WaitTillEventSystemLoads()
        {
            while (!EventSystem.current)
            {
                yield return new WaitForEndOfFrame();
            }
            if (j_PntrData != null) { yield break; }
            j_PntrData = new PointerEventData(EventSystem.current);
            j_PntrData.Reset();
        }

        ScrollRect GetScrollParent(Transform t)
        {
            if (t.parent != null)
            {
                ScrollRect scroll = t.parent.GetComponent<ScrollRect>();
                if (scroll != null) { return scroll; }
                else return GetScrollParent(t.parent);
            }
            return null;
        }

        public override void OnScroll(PointerEventData data)
        {
            return;
        }

        private void Update()
        {
            if (!Application.isPlaying)
                return;

            velocity = Vector2.Lerp(velocity, Vector2.zero, scrollSensitivity * Time.deltaTime);
        }


        public void ProcessVerticalScroll(float eventData)
        {
            ProcessScroll(false, eventData);
            if (JMRAnalyticsManager.Instance != null)
            {

                if (eventData < 0)
                    JMRAnalyticsManager.Instance.WriteEvent(JMRAnalyticsManager.Instance.EVENT_XGLSY_GAZE_UPSCROLL); 
                if (eventData > 0)
                    JMRAnalyticsManager.Instance.WriteEvent(JMRAnalyticsManager.Instance.EVENT_XGLSY_GAZE_DOWNSCROLL);
            }
        }

        public void ProcessHorizontalScroll(float eventData)
        {
            ProcessScroll(true, eventData);
            if (JMRAnalyticsManager.Instance != null)
            {

                if (eventData < 0)
                    JMRAnalyticsManager.Instance.WriteEvent(JMRAnalyticsManager.Instance.EVENT_XGLSY_GAZE_RIGHTSCROLL);
                if (eventData > 0)
                    JMRAnalyticsManager.Instance.WriteEvent(JMRAnalyticsManager.Instance.EVENT_XGLSY_GAZE_LEFTSCROLL);
            }
        }

        public void ProcessScroll(bool isXAxis, float eventData)
        {
            if (j_PntrData == null) { return; }

            isVerticalScroll = !isXAxis;
            j_TempVelocity.x = isXAxis ? eventData : 0;
            j_TempVelocity.y = !isXAxis ? eventData : 0;

            if (j_ParentScroll != null && ((j_ParentScroll.vertical && isVerticalScroll) || (j_ParentScroll.horizontal && !isVerticalScroll)))
            {
                j_ParentScroll.velocity = j_TempVelocity * j_ParentScroll.scrollSensitivity * 3000;
                StartCoroutine(WaitToProcessScroll(isXAxis, j_ParentScroll));
            }
            else
            {
                velocity = j_TempVelocity * scrollSensitivity * 3000;
                StartCoroutine(WaitToProcessScroll(isXAxis, this));
            }
        }

        private void SetScrollButtonState(ScrollRect _scrollRect, bool isXAxis)
        {
            float normalizedPosition = !isXAxis ? _scrollRect.verticalNormalizedPosition : _scrollRect.horizontalNormalizedPosition;
            if (upScrollButton == null || downScrollButton == null)
                return;

            if (normalizedPosition >= 1)
            {
                upScrollButton.enabled = false;
                upScrollButton.gameObject.GetComponent<Image>().DOFade(0.4f, 0.1f);
            }
            else if (normalizedPosition <= 0)
            {
                downScrollButton.enabled = false;
                downScrollButton.gameObject.GetComponent<Image>().DOFade(0.4f, 0.1f);
            }
            else
            {
                if (!upScrollButton.enabled)
                {
                    upScrollButton.enabled = true;
                    upScrollButton.gameObject.GetComponent<Image>().DOFade(1f, 0.1f);
                }
                if (!downScrollButton.enabled)
                {
                    downScrollButton.enabled = true;
                    downScrollButton.gameObject.GetComponent<Image>().DOFade(1f, 0.1f);
                }
            }
        }

        IEnumerator WaitToProcessScroll(bool isXAxis, ScrollRect _scrollRect)
        {            
            yield return new WaitForEndOfFrame();
            if (velocity.x <= 0 && velocity.y <= 0)
            {
                SetScrollButtonState(_scrollRect, isXAxis);
                yield break;
            }
            else
                StartCoroutine(WaitToProcessScroll(isXAxis, _scrollRect));
        }

        public void OnSwipeLeft(SwipeEventData eventData, float value)
        {
            ProcessScroll(true, value);
        }

        public void OnSwipeRight(SwipeEventData eventData, float value)
        {
            ProcessScroll(true, value);
        }

        public void OnSwipeUp(SwipeEventData eventData, float value)
        {
            ProcessScroll(false, value);
        }

        public void OnSwipeDown(SwipeEventData eventData, float value)
        {
            ProcessScroll(false, value);
        }

        public void OnSwipeStarted(SwipeEventData eventData)
        {
            
        }

        public void OnSwipeUpdated(SwipeEventData eventData, Vector2 swipeData)
        {
            
        }

        public void OnSwipeCompleted(SwipeEventData eventData)
        {
            
        }

        public void OnSwipeCanceled(SwipeEventData eventData)
        {
            
        }        
    }
}