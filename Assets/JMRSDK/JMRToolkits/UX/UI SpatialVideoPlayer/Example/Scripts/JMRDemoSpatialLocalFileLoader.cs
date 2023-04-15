//Copyright (c) 2020 JioGlass. All Rights Reserved.
using JMRSDK.Toolkit;
using JMRSDK.Toolkit.UI;
using MediaServices;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace JMRSDK.Demo
{
    public class JMRDemoSpatialLocalFileLoader : MonoBehaviour
    {
        [SerializeField]
        private JMRSpatialVideoPlayer mediaPlayer;
        [SerializeField]
        private RawImage minVideo, maxVideo;

        public List<MediaItem> currentPlayList = new List<MediaItem>();
        [SerializeField]
        private JMRDemoSpacialMediaItem mediaItemPrefab;
        [SerializeField]
        private JMRUITertiaryButtonGroup group;
        [SerializeField]
        private Transform container;

        void Start()
        {
            if (!Application.isEditor)
            {
                CreateLocalList();
            }
        }

        void CreateLocalList()
        {
            Debug.Log("===>ADDING LOCAL FILES " + Application.persistentDataPath);
            string path = Application.persistentDataPath.Substring(0, Application.persistentDataPath.IndexOf("Android", StringComparison.Ordinal));
            string myPath = path + "/SamplesVideos/";
            //string myPath = "/storage/emulated/0/SamplesVideos/";
            DirectoryInfo dir = new DirectoryInfo(myPath);
            FileInfo[] info = dir.GetFiles("*.*");
            foreach (FileInfo f in info)
            {

                Debug.LogError("Extention = " + f.Extension);
                if (f.Extension != ".aac" && f.Extension != ".aiff" && f.Extension != ".flac"
                    && f.Extension != ".m4a" && f.Extension != ".mmf" && f.Extension != ".ogg" && f.Extension != ".opus"
                    && f.Extension != "..wav" && f.Extension != ".m4r")
                {
                    Debug.Log("===> Files  " + f.FullName);
                    MediaItem item = new MediaItem(
                    f.FullName,
                    false,
                    null,
                    f.Name,
                    null,
                    null,
                    ""
                    );
                    currentPlayList.Add(item);

                    JMRDemoSpacialMediaItem mediaObject = Instantiate(mediaItemPrefab, container);
                    mediaObject.Init(this, item);
                }
            }
            if (info.Length > 1)
            {
                group.enabled = true;
            }
            Debug.Log("===>ADDING LOCAL FILES : DONE");
        }

        public void OnSelect(MediaItem item)
        {

            if (mediaPlayer.IsPlaying())
            {
                mediaPlayer.Stop();
                minVideo.material.mainTexture = Texture2D.whiteTexture;
                maxVideo.material.mainTexture = Texture2D.whiteTexture;
            }
            mediaPlayer.SetAndPlayMediaItem(item._mediaUrl);
        }
        public void OnDeSelect(MediaItem item)
        {
        }
    }
}
