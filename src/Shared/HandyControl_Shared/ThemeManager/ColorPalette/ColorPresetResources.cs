﻿// http://github.com/kinnara/ModernWpf

using System;
using System.Windows;
using HandyControl.Controls;

namespace HandyControl.Themes
{
    public class ColorPresetResources : ResourceDictionary
    {
        private ApplicationTheme _targetTheme;

        public ColorPresetResources()
        {
            WeakEventManager<PresetManager, EventArgs>.AddHandler(
                PresetManager.Current,
                nameof(PresetManager.ColorPresetChanged),
                OnCurrentPresetChanged);

            ApplyCurrentPreset();
        }

        public ApplicationTheme TargetTheme
        {
            get => _targetTheme;
            set
            {
                if (_targetTheme != value)
                {
                    _targetTheme = value;
                    ApplyCurrentPreset();
                }
            }
        }

        private void OnCurrentPresetChanged(object sender, EventArgs e)
        {
            ApplyCurrentPreset();
        }
        private void ApplyCurrentPreset()
        {
            if (MergedDictionaries.Count > 0)
            {
                MergedDictionaries.Clear();
            }

            var currentPreset = PresetManager.Current.ColorPreset;
            if (currentPreset !=null)
            {
                var source = PackUriHelper.GetAbsoluteUri(currentPreset.AssemblyName, $"{currentPreset.ColorPreset}/{TargetTheme}.xaml");
                var rd = new ResourceDictionary { Source = source };
                MergedDictionaries.Add(rd);
            }
        }
    }
}
