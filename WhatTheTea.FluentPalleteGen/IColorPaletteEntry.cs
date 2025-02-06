﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace WhatTheTea.FluentPalleteGen
{
    // These classes are not intended to be viewmodels.
    // They deal with the data about an editable palette and are passed to special purpose controls for editing
    public interface IColorPaletteEntry
    {
        string Title { get; }
        string Description { get; }

        ARGB ActiveColor
        {
            get;
            set;
        }
        string ActiveColorString { get; }
        ColorStringFormat ActiveColorStringFormat { get; }

        event Action<IColorPaletteEntry> ActiveColorChanged;

        IReadOnlyList<ContrastColorWrapper> ContrastColors { get; set; }
        ContrastColorWrapper BestContrastColor { get; }

        event Action<IColorPaletteEntry> ContrastColorChanged;
    }
}
