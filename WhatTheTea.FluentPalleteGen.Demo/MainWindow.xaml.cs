using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Microsoft.UI.Xaml;

using WhatTheTea.FluentPalleteGen;
using WhatTheTea.FluentPalleteGen.Utils;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PalleteTest
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<string> Pallete = [];

        public string SelectedColor
        {
            get;
            set
            {
                Set(ref field, value);
                GeneratePallete();
            }
        } = "#000000";

        public MainWindow()
        {
            this.InitializeComponent();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void Set<T>(ref T field, T value, [CallerMemberName] string? member = null)
        {
            if (!(field?.Equals(value) ?? false))
            {
                field = value;
                PropertyChanged?.Invoke(this, new(member));
            }
        }

        private void GeneratePallete()
        {
            if (ColorUtils.TryParseColorString(SelectedColor, out var baseColor))
            {
                Pallete.Clear();

                var pallete = new ColorPalette(7, baseColor);

                foreach (var entry in pallete.Palette)
                {
                    Pallete.Add(entry.ActiveColorString);
                }
            }
        }
    }
}
