// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

using WhatTheTea.FluentPalleteGen.Utils;

namespace WhatTheTea.FluentPalleteGen
{
    // These classes are not intended to be viewmodels.
    // They deal with the data about an editable palette and are passed to special purpose controls for editing
    public class EditableColorPaletteEntry : IColorPaletteEntry
    {
        public EditableColorPaletteEntry(IColorPaletteEntry sourceColor, ARGB customColor, bool useCustomColor, string title, string description, ColorStringFormat activeColorStringFormat, IReadOnlyList<ContrastColorWrapper> contrastColors)
        {
            _sourceColor = sourceColor;
            _customColor = customColor;
            _useCustomColor = useCustomColor;
            _title = title;
            _description = description;
            _activeColorStringFormat = activeColorStringFormat;

            if (_useCustomColor || _sourceColor == null)
            {
                _activeColor = _customColor;
            }
            else
            {
                _activeColor = _sourceColor.ActiveColor;
            }

            ContrastColors = contrastColors;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private IColorPaletteEntry _sourceColor;
        public IColorPaletteEntry SourceColor
        {
            get { return _sourceColor; }
            set
            {
                if (_sourceColor != null)
                {
                    _sourceColor.ActiveColorChanged -= _sourceColor_ActiveColorChanged;
                }

                _sourceColor = value;

                if (_sourceColor != null)
                {
                    _sourceColor.ActiveColorChanged += _sourceColor_ActiveColorChanged;
                }

                CheckActiveColor();
            }
        }

        private void _sourceColor_ActiveColorChanged(IColorPaletteEntry obj)
        {
            CheckActiveColor();
        }

        private ARGB _customColor;
        public ARGB CustomColor
        {
            get { return _customColor; }
            set
            {
                if (_customColor != value)
                {
                    _customColor = value;
                    CustomColorChanged?.Invoke(this);

                    CheckActiveColor();
                }
            }
        }

        public event Action<IColorPaletteEntry> CustomColorChanged;

        private bool _useCustomColor;
        public bool UseCustomColor
        {
            get { return _useCustomColor; }
            set
            {
                if (_useCustomColor != value)
                {
                    _useCustomColor = value;

                    CheckActiveColor();
                }
            }
        }

        private void CheckActiveColor()
        {
            ARGB newVal;

            if (_useCustomColor || _sourceColor == null)
            {
                newVal = _customColor;
            }
            else
            {
                newVal = _sourceColor.ActiveColor;
            }

            if (newVal != _activeColor)
            {
                _activeColor = newVal;
                ActiveColorChanged?.Invoke(this);

                UpdateContrastColor();
            }
        }

        private ARGB _activeColor;
        public ARGB ActiveColor
        {
            get { return _activeColor; }
            set
            {
                CustomColor = value;
                if (_sourceColor != null)
                {
                    UseCustomColor = _customColor != _sourceColor.ActiveColor;
                }
            }
        }

        public string ActiveColorString
        {
            get
            {
                return ColorUtils.FormatColorString(_activeColor, _activeColorStringFormat);
            }
        }

        private ColorStringFormat _activeColorStringFormat = ColorStringFormat.PoundRGB;
        public ColorStringFormat ActiveColorStringFormat
        {
            get { return _activeColorStringFormat; }
        }

        public event Action<IColorPaletteEntry> ActiveColorChanged;

        private IReadOnlyList<ContrastColorWrapper> _contrastColors;
        public IReadOnlyList<ContrastColorWrapper> ContrastColors
        {
            get { return _contrastColors; }
            set
            {
                if (_contrastColors != value)
                {
                    if (_contrastColors != null)
                    {
                        foreach (var c in _contrastColors)
                        {
                            c.Color.ActiveColorChanged -= ContrastColor_ActiveColorChanged;
                        }
                    }

                    _contrastColors = value;

                    if (_contrastColors != null)
                    {
                        foreach (var c in _contrastColors)
                        {
                            c.Color.ActiveColorChanged += ContrastColor_ActiveColorChanged;
                        }
                    }

                    UpdateContrastColor();
                }
            }
        }

        private void ContrastColor_ActiveColorChanged(IColorPaletteEntry obj)
        {
            UpdateContrastColor();
        }

        private ContrastColorWrapper _bestContrastColor;
        public ContrastColorWrapper BestContrastColor
        {
            get { return _bestContrastColor; }
        }

        public double BestContrastValue
        {
            get
            {
                if (_bestContrastColor == null)
                {
                    return 0;
                }
                return ColorUtils.ContrastRatio(ActiveColor, _bestContrastColor.Color.ActiveColor);
            }
        }

        private void UpdateContrastColor()
        {
            ContrastColorWrapper newContrastColor = null;

            if (_contrastColors != null && _contrastColors.Count > 0)
            {
                double maxContrast = -1;
                foreach (var c in _contrastColors)
                {
                    double contrast = ColorUtils.ContrastRatio(ActiveColor, c.Color.ActiveColor);
                    if (contrast > maxContrast)
                    {
                        maxContrast = contrast;
                        newContrastColor = c;
                    }
                }
            }

            if (_bestContrastColor != newContrastColor)
            {
                _bestContrastColor = newContrastColor;
                ContrastColorChanged?.Invoke(this);
            }
        }

        public event Action<IColorPaletteEntry> ContrastColorChanged;
    }
}
