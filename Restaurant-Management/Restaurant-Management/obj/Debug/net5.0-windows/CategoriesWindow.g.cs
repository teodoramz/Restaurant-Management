#pragma checksum "..\..\..\CategoriesWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2636D7D6F2C5E0EF2A1ED86E688FAEB1C3196BAB"
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


namespace Restaurant_Management {
    
    
    /// <summary>
    /// CategoriesWindow
    /// </summary>
    public partial class CategoriesWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\CategoriesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button categoriesButton;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\CategoriesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button productsButton;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\CategoriesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logoutButton;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\CategoriesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button historyButton;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\CategoriesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox idTextBox;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\CategoriesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataTableChosed;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\CategoriesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox categoryNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\..\CategoriesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox detailsTextBox;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\..\CategoriesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addButton;
        
        #line default
        #line hidden
        
        
        #line 181 "..\..\..\CategoriesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button editButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Restaurant-Management;component/categorieswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\CategoriesWindow.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\CategoriesWindow.xaml"
            ((Restaurant_Management.CategoriesWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.categoriesButton = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\CategoriesWindow.xaml"
            this.categoriesButton.Click += new System.Windows.RoutedEventHandler(this.categoriesButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.productsButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\CategoriesWindow.xaml"
            this.productsButton.Click += new System.Windows.RoutedEventHandler(this.productsButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.logoutButton = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\CategoriesWindow.xaml"
            this.logoutButton.Click += new System.Windows.RoutedEventHandler(this.logoutButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.historyButton = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\..\CategoriesWindow.xaml"
            this.historyButton.Click += new System.Windows.RoutedEventHandler(this.historyButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 97 "..\..\..\CategoriesWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.idTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 118 "..\..\..\CategoriesWindow.xaml"
            this.idTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.idTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 8:
            this.dataTableChosed = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 9:
            this.categoryNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.detailsTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.addButton = ((System.Windows.Controls.Button)(target));
            
            #line 169 "..\..\..\CategoriesWindow.xaml"
            this.addButton.Click += new System.Windows.RoutedEventHandler(this.addButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.editButton = ((System.Windows.Controls.Button)(target));
            
            #line 181 "..\..\..\CategoriesWindow.xaml"
            this.editButton.Click += new System.Windows.RoutedEventHandler(this.editButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

