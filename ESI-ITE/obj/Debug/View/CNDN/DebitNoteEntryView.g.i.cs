﻿#pragma checksum "..\..\..\..\View\CNDN\DebitNoteEntryView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CABF9CBF4A90FF573841BC9BE9D69F03997F7C49"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ESI_ITE.View.CNDN;
using ESI_ITE.ViewModel.CNDN;
using ESI_ITE.ViewModel.Converter;
using MahApps.Metro.SimpleChildWindow;
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace ESI_ITE.View.CNDN {
    
    
    /// <summary>
    /// DebitNoteEntryView
    /// </summary>
    public partial class DebitNoteEntryView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\View\CNDN\DebitNoteEntryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ESI_ITE.ViewModel.CNDN.DebitNoteEntryViewModel viewModel;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\View\CNDN\DebitNoteEntryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridSearch;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\View\CNDN\DebitNoteEntryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grdSearchModal;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\View\CNDN\DebitNoteEntryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCustomerSearch;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\View\CNDN\DebitNoteEntryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid customerSearchGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/ESI-ITE;component/view/cndn/debitnoteentryview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\CNDN\DebitNoteEntryView.xaml"
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
            this.viewModel = ((ESI_ITE.ViewModel.CNDN.DebitNoteEntryViewModel)(target));
            return;
            case 2:
            this.gridSearch = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.grdSearchModal = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.txtCustomerSearch = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.customerSearchGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

