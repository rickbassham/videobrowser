﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaBrowser.Library.Plugins;
using MediaBrowser.Library;
using MediaBrowser.Library.Plugins.Configuration;

namespace MediaInfoProvider
{
    public class PluginOptions : PluginConfigurationOptions
    {
        [Label("Save to File")]
        public bool SaveToFile = true;

        [Label("Allow Rips  (service)")]
        public bool AllowBDRips = false;

        [Label("Timeout (ms)")]
        public string ServiceTimeout = "60000";

        [Label("Clear Bad File List")]
        public bool ClearBadFiles = false;

        public List<string> BadFiles = new List<string>();
        public List<string> FormerBadFiles = new List<string>();

    }
}
