// Updated by XamlIntelliSenseFileGenerator 11/16/2021 7:25:32 PM
#pragma checksum "..\..\..\HistoryWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A2EA76E211F4D521CBC3C9B3EC75C36A3CAD4991"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Restaurant_Management;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Restaurant_Management
{


    /// <summary>
    /// HistoryWindow
    /// </summary>
    public partial class HistoryWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 33 "..\..\..\HistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button categoriesButton;

#line default
#line hidden


#line 54 "..\..\..\HistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button productsButton;

#line default
#line hidden


#line 75 "..\..\..\HistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button historyButton;

#line default
#line hidden


#line 96 "..\..\..\HistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logoutButton;

#line default
#line hidden


#line 153 "..\..\..\HistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataTableChosed;

#line default
#line hidden


#line 204 "..\..\..\HistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addButton;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.11.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Restaurant-Management;component/historywindow.xaml", System.UriKind.Relative);

#line 1 "..\..\..\HistoryWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:

#line 8 "..\..\..\HistoryWindow.xaml"
                    ((Restaurant_Management.HistoryWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);

#line default
#line hidden
                    return;
                case 2:
                    this.employersButton = ((System.Windows.Controls.Button)(target));

#line 11 "..\..\..\HistoryWindow.xaml"
                    this.employersButton.Click += new System.Windows.RoutedEventHandler(this.employersButton_Click);

#line default
#line hidden
                    return;
                case 3:
                    this.categoriesButton = ((System.Windows.Controls.Button)(target));

#line 33 "..\..\..\HistoryWindow.xaml"
                    this.categoriesButton.Click += new System.Windows.RoutedEventHandler(this.categoriesButton_Click);

#line default
#line hidden
                    return;
                case 4:
                    this.productsButton = ((System.Windows.Controls.Button)(target));

#line 54 "..\..\..\HistoryWindow.xaml"
                    this.productsButton.Click += new System.Windows.RoutedEventHandler(this.productsButton_Click);

#line default
#line hidden
                    return;
                case 5:
                    this.historyButton = ((System.Windows.Controls.Button)(target));

#line 75 "..\..\..\HistoryWindow.xaml"
                    this.historyButton.Click += new System.Windows.RoutedEventHandler(this.historyButton_Click);

#line default
#line hidden
                    return;
                case 6:
                    this.logoutButton = ((System.Windows.Controls.Button)(target));

#line 96 "..\..\..\HistoryWindow.xaml"
                    this.logoutButton.Click += new System.Windows.RoutedEventHandler(this.logoutButton_Click);

#line default
#line hidden
                    return;
                case 7:

#line 117 "..\..\..\HistoryWindow.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);

#line default
#line hidden
                    return;
                case 8:
                    this.idTextBox = ((System.Windows.Controls.TextBox)(target));

#line 138 "..\..\..\HistoryWindow.xaml"
                    this.idTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.idTextBox_PreviewTextInput);

#line default
#line hidden
                    return;
                case 9:
                    this.dataTableChosed = ((System.Windows.Controls.DataGrid)(target));
                    return;
                case 10:
                    this.nameTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 11:
                    this.phoneTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 12:
                    this.positionTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 13:
                    this.addButton = ((System.Windows.Controls.Button)(target));

#line 204 "..\..\..\HistoryWindow.xaml"
                    this.addButton.Click += new System.Windows.RoutedEventHandler(this.addButton_Click);

#line default
#line hidden
                    return;
                case 14:
                    this.editButton = ((System.Windows.Controls.Button)(target));

#line 216 "..\..\..\HistoryWindow.xaml"
                    this.editButton.Click += new System.Windows.RoutedEventHandler(this.editButton_Click);

#line default
#line hidden
                    return;
                case 15:
                    this.deleteButton = ((System.Windows.Controls.Button)(target));

#line 228 "..\..\..\HistoryWindow.xaml"
                    this.deleteButton.Click += new System.Windows.RoutedEventHandler(this.deleteButton_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Button createSellButton;
        internal System.Windows.Controls.Button editButton;
        internal System.Windows.Controls.Button deleteButton;
        internal System.Windows.Controls.TextBox idSellTextBox;
        internal System.Windows.Controls.TextBox productIDTextBox;
        internal System.Windows.Controls.TextBox quantityTextBox;
    }
}

