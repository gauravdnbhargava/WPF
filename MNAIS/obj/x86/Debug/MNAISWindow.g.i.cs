﻿#pragma checksum "..\..\..\MNAISWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BACF2718BE086113BB6509EF3CBD9FF21CF58651187DB9639C257983C05617D5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Fluent;
using MNAIS;
using MNAIS.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace MNAIS {
    
    
    /// <summary>
    /// MNAISWindow
    /// </summary>
    public partial class MNAISWindow : Fluent.RibbonWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\MNAISWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.Ribbon Ribbon;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\MNAISWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.RibbonTabItem tbContract;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\MNAISWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.RibbonTabItem tbAnalysis;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\MNAISWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.RibbonTabItem tbResults;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\MNAISWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.RibbonTabItem tbAdministration;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\MNAISWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl c_contentControl;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\MNAISWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl c_projectControl;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MNAIS;component/mnaiswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MNAISWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Ribbon = ((Fluent.Ribbon)(target));
            return;
            case 2:
            this.tbContract = ((Fluent.RibbonTabItem)(target));
            return;
            case 3:
            
            #line 29 "..\..\..\MNAISWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ContractTab_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 42 "..\..\..\MNAISWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Create_Contract_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 45 "..\..\..\MNAISWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Import_Contract_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 46 "..\..\..\MNAISWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Export_Contract_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tbAnalysis = ((Fluent.RibbonTabItem)(target));
            return;
            case 8:
            
            #line 63 "..\..\..\MNAISWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.AnalysisTab_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.tbResults = ((Fluent.RibbonTabItem)(target));
            return;
            case 10:
            
            #line 90 "..\..\..\MNAISWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.OutputTab_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 11:
            this.tbAdministration = ((Fluent.RibbonTabItem)(target));
            return;
            case 12:
            
            #line 121 "..\..\..\MNAISWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.AdminTab_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 13:
            this.c_contentControl = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 14:
            this.c_projectControl = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

