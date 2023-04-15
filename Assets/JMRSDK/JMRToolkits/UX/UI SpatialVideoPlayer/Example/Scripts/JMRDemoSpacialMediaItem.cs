//Copyright (c) 2020 JioGlass. All Rights Reserved.
using JMRSDK.Toolkit;
using MediaServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace JMRSDK.Demo {
    public class JMRDemoSpacialMediaItem : MonoBehaviour
    {
        [SerializeField]
        private JMRUITertiaryButton button;
        [SerializeField]
        private TMP_Text title;
        private JMRDemoSpatialLocalFileLoader localFileMediaManager;
        private MediaItem mediaItem;

        void Start()
        {
            button.OnValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            button.OnValueChanged.RemoveListener(OnValueChanged);
        }

        public void Init(JMRDemoSpatialLocalFileLoader localFileMediaManager, MediaItem mediaItem)
        {
            this.localFileMediaManager = localFileMediaManager;
            this.mediaItem = mediaItem;
            title.text = mediaItem._title;
        }

        private void OnValueChanged(bool state)
        {
            if (state)
            {
                localFileMediaManager.OnSelect(mediaItem);
            }
        }
    }
}