﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MediaBrowser.Library.Entities;
using MediaBrowser.Library.Providers.Attributes;
using MediaBrowser.Library.Providers;
using MediaBrowser.Library;


namespace MtnFrameGrabProvider {
    // we mark this as slow, so we ensure it only runs at the end of the chain
    [SlowProvider]
    [SupportedType(typeof(Movie))]
    [SupportedType(typeof(Episode))]
    class FrameGrabProvider : BaseMetadataProvider {
        static MediaType[] supportedMediaTypes = new MediaType[] { MediaType.Avi, MediaType.Mkv, MediaType.Mpg, MediaType.Mp4, MediaType.Wmv, MediaType.Unknown };

        Video Video { get { return (Video)Item; } }

        bool Supported {
            get {
                return supportedMediaTypes.Contains(Video.MediaType) && !Video.VideoFiles.First().ToLower().StartsWith("http");
            }
        }

        public override void Fetch() {
            Item.PrimaryImagePath = "mtngrab://" + Video.VideoFiles.First();
        }

        public override bool NeedsRefresh() {
            return Item.PrimaryImagePath == null && Supported;
        }
    }
}
