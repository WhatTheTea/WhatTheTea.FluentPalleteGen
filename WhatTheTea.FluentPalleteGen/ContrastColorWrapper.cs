// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace WhatTheTea.FluentPalleteGen
{
    public class ContrastColorWrapper
    {
        public ContrastColorWrapper(IColorPaletteEntry color, bool showInContrastList, bool showContrastErrors)
        {
            Color = color ?? throw new ArgumentNullException("color");
            ShowInContrastList = showInContrastList;
            ShowContrastErrors = showContrastErrors;
        }

        public IColorPaletteEntry Color { get; }

        public bool ShowInContrastList { get; }

        public bool ShowContrastErrors { get; }
    }
}
