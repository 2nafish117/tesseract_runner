//Copyright (c) 2020 JioGlass. All Rights Reserved.
using JMRSDK.Toolkit;
using MediaServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace JMRSDK.Demo {
    public class JMRDemoNativeMediaItem : MonoBehaviour
    {
        [SerializeField]
        private JMRUISecondaryButton button;
        [SerializeField]
        private TMP_Text title;
        private JMRDemoNativeLocalFileLoader localFileMediaManager;
        private MediaItem mediaItem;

        void Start()
        {
            button.OnSelect.AddListener(OnSelect);
            button.OnDeselect.AddListener(OnDeSelect);
        }

        private void OnDestroy()
        {
            button.OnSelect.RemoveListener(OnSelect);
            button.OnDeselect.RemoveListener(OnDeSelect);
        }

        public void Init(JMRDemoNativeLocalFileLoader localFileMediaManager, MediaItem mediaItem)
        {
            this.localFileMediaManager = localFileMediaManager;
            this.mediaItem = mediaItem;
            title.text = mediaItem._title;
        }

        private void OnSelect()
        {
            localFileMediaManager.OnSelect(mediaItem);
        }
        private void OnDeSelect()
        {
            localFileMediaManager.OnDeSelect(mediaItem);
        }
    }
}