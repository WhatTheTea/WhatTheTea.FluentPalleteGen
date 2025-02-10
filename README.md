# FluentPalleteGen - portable windows-like pallete generation

[![build](https://github.com/WhatTheTea/WhatTheTea.FluentPalleteGen/actions/workflows/publish.yml/badge.svg)](https://github.com/WhatTheTea/WhatTheTea.FluentPalleteGen/actions/workflows/publish.yml)
![NuGet Version](https://img.shields.io/nuget/v/WhatTheTea.FluentPalleteGen)
[![Made in Ukraine](https://img.shields.io/badge/made_in-Ukraine-ffd700.svg?labelColor=0057b7)](https://stand-with-ukraine.pp.ua)

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

There is demo app source available at [WhatTheTea.FluentPalleteGen.Demo](https://github.com/WhatTheTea/WhatTheTea.FluentPalleteGen/tree/master/WhatTheTea.FluentPalleteGen.Demo). It consists of color picker and array of rectangles to represent generated pallete.