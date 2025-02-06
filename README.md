# FluentPalleteGen - portable windows-like pallete generation

This library is a polished version of code extracted from [Fluent XAML Theme Editor](https://github.com/Microsoft/fluent-xaml-theme-editor) to generate **system accent colors** like windows does in WinUI / UWP frameworks by default.

## Minimal sample

```csharp
string selectedAccentColor = "#29903b";
var selectedARGB = ColorUtils.TryParseColorString(selectedAccentColor, out var baseColor);
var pallete = new ColorPalette(7, baseColor);

// Resulting pallete can be used to redefine system brushes like 
//  "SystemAccentColorDark1", "SystemAccentColorDark2", "SystemAccentColorDark3" . . .
```

## Demo App

There is demo app source available at [WhatTheTea.FluentPalleteGen.Demo](WhatTheTea.FluentPalleteGen.Demo/). It consists of color picker and array of rectangles to represent generated pallete.