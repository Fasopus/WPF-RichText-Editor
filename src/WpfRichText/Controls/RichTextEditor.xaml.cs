using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfRichText
{
    /// <summary>
    /// Interaction logic for BindableRichTextbox.xaml
    /// </summary>
    public partial class RichTextEditor : UserControl
    {
		/// <summary></summary>
		public static readonly DependencyProperty TextProperty =
		  DependencyProperty.Register("Text", typeof(string), typeof(RichTextEditor),
		  new PropertyMetadata(string.Empty));

		/// <summary></summary>
		public static readonly DependencyProperty IsToolBarVisibleProperty =
		  DependencyProperty.Register("IsToolBarVisible", typeof(bool), typeof(RichTextEditor),
		  new PropertyMetadata(true));

		/// <summary></summary>
		public static readonly DependencyProperty IsContextMenuEnabledProperty =
		  DependencyProperty.Register("IsContextMenuEnabled", typeof(bool), typeof(RichTextEditor),
		  new PropertyMetadata(true));

        /// <summary></summary>
        public static readonly DependencyProperty CutVisibilityProperty =
          DependencyProperty.Register("CutVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty CopyVisibilityProperty =
          DependencyProperty.Register("CopyVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty PasteVisibilityProperty =
          DependencyProperty.Register("PasteVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty UndoVisibilityProperty =
          DependencyProperty.Register("UndoVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty RedoVisibilityProperty =
          DependencyProperty.Register("RedoVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty FontSelectionVisibilityProperty =
          DependencyProperty.Register("FontSelectionVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty FontSizeSelectionVisibilityProperty =
          DependencyProperty.Register("FontSizeSelectionVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty BoldFontVisibilityProperty =
          DependencyProperty.Register("BoldFontVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty ItalicFontVisibilityProperty =
          DependencyProperty.Register("ItalicFontVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty UnderlinedFontVisibilityProperty =
          DependencyProperty.Register("UnderlinedFontVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty FontColorVisibilityProperty =
          DependencyProperty.Register("FontColorVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty JustificationVisibilityProperty =
          DependencyProperty.Register("JustificationVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty BulletFormattingVisibilityProperty =
          DependencyProperty.Register("BulletFormattingVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty IndentationVisibilityProperty =
          DependencyProperty.Register("IndentationVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty InsertLinkVisibilityProperty =
          DependencyProperty.Register("InsertLinkVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty SeparatorVisibilityProperty =
          DependencyProperty.Register("SeparatorVisibility", typeof(Visibility), typeof(RichTextEditor),
          new PropertyMetadata(Visibility.Visible));

        /// <summary></summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
		  DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(RichTextEditor),
		  new PropertyMetadata(false));

		/// <summary></summary>
		public static readonly DependencyProperty AvailableFontsProperty =
		  DependencyProperty.Register("AvailableFonts", typeof(Collection<String>), typeof(RichTextEditor),
		  new PropertyMetadata(new Collection<String>(
			  new List<String>(4) 
			  {
				  "Arial",
				  "Courier New",
				  "Tahoma",
				  "Times New Roman"
			  }
		)));


		private TextRange textRange = null;

		/// <summary></summary>
		public RichTextEditor()
        {
            InitializeComponent();
		}

		/// <summary></summary>
		public string Text
		{
			get
            {
                return GetValue(TextProperty) as string;
            }
			set
			{
				SetValue(TextProperty, value);
			}
		}

		/// <summary></summary>
		public bool IsToolBarVisible
		{
			get
            {
                return (GetValue(IsToolBarVisibleProperty) as bool? == true);
            }
			set
			{
				SetValue(IsToolBarVisibleProperty, value);
				//this.mainToolBar.Visibility = (value == true) ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		/// <summary></summary>
		public bool IsContextMenuEnabled
		{
			get 
			{ 
				return (GetValue(IsContextMenuEnabledProperty) as bool? == true);
			}
			set
			{
				SetValue(IsContextMenuEnabledProperty, value);
			}
		}

		/// <summary></summary>
		public bool IsReadOnly
		{
			get
            {
                return (GetValue(IsReadOnlyProperty) as bool? == true);
            }
			set
			{
				SetValue(IsReadOnlyProperty, value);
				SetValue(IsToolBarVisibleProperty, !value);
				SetValue(IsContextMenuEnabledProperty, !value);
			}
		}

        /// <summary></summary>
        public Visibility CutVisibility
        {
            get
            {
                return (Visibility)GetValue(CutVisibilityProperty);
            }
            set
            {
                SetValue(CutVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility CopyVisibility
        {
            get
            {
                return (Visibility)GetValue(CopyVisibilityProperty);
            }
            set
            {
                SetValue(CopyVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility PasteVisibility
        {
            get
            {
                return (Visibility)GetValue(PasteVisibilityProperty);
            }
            set
            {
                SetValue(PasteVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility UndoVisibility
        {
            get
            {
                return (Visibility)GetValue(UndoVisibilityProperty);
            }
            set
            {
                SetValue(UndoVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility RedoVisibility
        {
            get
            {
                return (Visibility)GetValue(RedoVisibilityProperty);
            }
            set
            {
                SetValue(RedoVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility FontSelectionVisibility
        {
            get
            {
                return (Visibility)GetValue(FontSelectionVisibilityProperty);
            }
            set
            {
                SetValue(FontSelectionVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility FontSizeSelectionVisibility
        {
            get
            {
                return (Visibility)GetValue(FontSizeSelectionVisibilityProperty);
            }
            set
            {
                SetValue(FontSizeSelectionVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility BoldFontVisibility
        {
            get
            {
                return (Visibility)GetValue(BoldFontVisibilityProperty);
            }
            set
            {
                SetValue(BoldFontVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility ItalicFontVisibility
        {
            get
            {
                return (Visibility)GetValue(ItalicFontVisibilityProperty);
            }
            set
            {
                SetValue(ItalicFontVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility UnderlinedFontVisibility
        {
            get
            {
                return (Visibility)GetValue(UnderlinedFontVisibilityProperty);
            }
            set
            {
                SetValue(UnderlinedFontVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility FontColorVisibility
        {
            get
            {
                return (Visibility)GetValue(FontColorVisibilityProperty);
            }
            set
            {
                SetValue(FontColorVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility JustificationVisibility
        {
            get
            {
                return (Visibility)GetValue(JustificationVisibilityProperty);
            }
            set
            {
                SetValue(JustificationVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility BulletFormattingVisibility
        {
            get
            {
                return (Visibility)GetValue(BulletFormattingVisibilityProperty);
            }
            set
            {
                SetValue(BulletFormattingVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility IndentationVisibility
        {
            get
            {
                return (Visibility)GetValue(IndentationVisibilityProperty);
            }
            set
            {
                SetValue(IndentationVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility InsertLinkVisibility
        {
            get
            {
                return (Visibility)GetValue(InsertLinkVisibilityProperty);
            }
            set
            {
                SetValue(InsertLinkVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Visibility SeparatorVisibility
        {
            get
            {
                return (Visibility)GetValue(SeparatorVisibilityProperty);
            }
            set
            {
                SetValue(SeparatorVisibilityProperty, value);
            }
        }

        /// <summary></summary>
        public Collection<String> AvailableFonts
		{
			get { return GetValue(AvailableFontsProperty) as Collection<String>; }
			set
			{
				SetValue(AvailableFontsProperty, value);
			}
		}

		private void FontColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
		{
			this.mainRTB.Selection.ApplyPropertyValue(ForegroundProperty, e.NewValue.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.mainRTB != null && this.mainRTB.Selection != null)
				this.mainRTB.Selection.ApplyPropertyValue(FontFamilyProperty, e.AddedItems[0]);
		}

		private void insertLink_Click(object sender, RoutedEventArgs e)
		{
			this.textRange = new TextRange(this.mainRTB.Selection.Start, this.mainRTB.Selection.End);
			this.uriInputPopup.IsOpen = true;
		}

		private void uriCancelClick(object sender, RoutedEventArgs e)
		{
			e.Handled = true;
			this.uriInputPopup.IsOpen = false;
			this.uriInput.Text = string.Empty;
		}

		private void uriSubmitClick(object sender, RoutedEventArgs e)
		{
			e.Handled = true;
			this.uriInputPopup.IsOpen = false;
			this.mainRTB.Selection.Select(this.textRange.Start, this.textRange.End);
			if (!string.IsNullOrEmpty(this.uriInput.Text))
			{
				this.textRange = new TextRange(this.mainRTB.Selection.Start, this.mainRTB.Selection.End);
				Hyperlink hlink = new Hyperlink(this.textRange.Start, this.textRange.End);
				hlink.NavigateUri = new Uri(this.uriInput.Text, UriKind.RelativeOrAbsolute);
				this.uriInput.Text = string.Empty;
			}
			else
				this.mainRTB.Selection.ClearAllProperties();			
		}

		private void uriInput_KeyPressed(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Enter:
					this.uriSubmitClick(sender, e);
					break;
				case Key.Escape:
					this.uriCancelClick(sender, e);
					break;
				default:
					break;
			}
		}

		private void ContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
		{
			if (!this.IsContextMenuEnabled == true)
				e.Handled = true;
		}

        private void mainToolBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = toolBar.HasOverflowItems ? Visibility.Visible : Visibility.Collapsed;
            }

            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                var defaultMargin = new Thickness(0, 0, 11, 0);
                mainPanelBorder.Margin = toolBar.HasOverflowItems ? defaultMargin : new Thickness(0);
            }
        }
    }
}
