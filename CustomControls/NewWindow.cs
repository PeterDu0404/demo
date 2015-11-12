using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace Audit.Controls
{
    public partial class NewWindow : Window
    {
        public NewWindow()
        {
            this.Loaded += NewWindow_Loaded;
            this.SizeChanged+=NewWindow_SizeChanged;
        }

        public static readonly DependencyProperty IsCornerRadiusProperty = DependencyProperty.Register("IsCornerRadius", typeof(bool), typeof(NewWindow));
        /// <summary>
        /// 是否默认选中
        /// </summary>
        [Description("窗体是否圆角，不设置为不是圆角"), Category("输入设置"), DefaultValue(false)]
        public bool IsCornerRadius
        {
            get
            {
                return (bool)GetValue(IsCornerRadiusProperty);
            }
            set
            {
                SetValue(IsCornerRadiusProperty, value);
            }
        }

        protected void NewWindow_Loaded(object sender, RoutedEventArgs e)
        {
            statckpanel = this.GetChildByName<FrameworkElement>("TopPanel");
        }

        protected void NewWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IsCornerRadius)
            {
                System.Windows.Rect r = new System.Windows.Rect(e.NewSize);
                RectangleGeometry gm = new RectangleGeometry(r, 7, 7); // 40 is radius here
                ((UIElement)sender).Clip = gm;
            }
        }       

        private FrameworkElement statckpanel = null;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置  
            if (statckpanel != null)
            {
                Point position = e.GetPosition(statckpanel);

                // 如果鼠标位置在标题栏内，允许拖动  
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (position.X >= 0 && position.X < statckpanel.ActualWidth && position.Y >= 0 && position.Y < statckpanel.ActualHeight)
                    {
                        this.DragMove();
                    }
                }
            }
        }
    }
}
