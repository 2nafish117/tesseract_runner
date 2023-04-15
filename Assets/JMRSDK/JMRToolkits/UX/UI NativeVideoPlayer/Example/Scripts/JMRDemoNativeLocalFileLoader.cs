//Copyright (c) 2020 JioGlass. All Rights Reserved.
using MediaServices;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace JMRSDK.Demo
{
    public class JMRDemoNativeLocalFileLoader : MonoBehaviour
    {
        [SerializeField]
        private MediaPlayer mediaPlayer;
        [SerializeField]
        private RawImage minVideo, maxVideo;

        public List<MediaItem> currentPlayList = new List<MediaItem>();
        [SerializeField]
        private JMRDemoNativeMediaItem mediaItemPrefab;
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

                JMRDemoNativeMediaItem mediaObject = Instantiate(mediaItemPrefab, container);
                mediaObject.Init(this,item);
            }

            Debug.Log("===>ADDING LOCAL FILES : DONE");
        }

        public void OnSelect(MediaItem item)
        {
            if (mediaPlayer.isPlaying)
            {
                mediaPlayer.Stop();
            }
            mediaPlayer.AddAndPlay(item);
        }
        public void OnDeSelect(MediaItem item)
        {
            mediaPlayer.RemoveItem(item);
        }

    }
}
