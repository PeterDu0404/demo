using System;
using System.Collections.Generic;
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
using System.ComponentModel;

namespace Audit.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:BoyouPos.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:BoyouPos;assembly=BoyouPos"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:NewBox/>
    ///
    /// </summary>
    // <summary>
    /// 数字输入文本框
    /// 创建者：Maximus
    /// 创建时间：2013-5-13
    /// 版本：0.2
    /// 可输入：数字0-9,小数点,减号
    /// 可设置小数位数
    /// 可设置数值上下限,若上下限均设置为0，则上下限值无效
    /// </summary>
    public class NewBox : TextBox
    {
        private Label WaterMarkLable;

        private ScrollViewer WaterMarkScrollViewer;

        private char PWD_CHAR = '*';

        /// <summary>
        /// Only copy of real password
        /// </summary>
        /// <remarks>For more security use System.Security.SecureString type instead</remarks>
        private string _password = string.Empty;

        static NewBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NewBox), new FrameworkPropertyMetadata(typeof(NewBox)));
        }



        public NewBox()
        {
            this.LostFocus += new RoutedEventHandler(NewBox_LostFocus);
            this.GotFocus += NewBox_GotFocus;
            this.Loaded += NewBox_Loaded;
            this.LayoutUpdated += NewBox_LayoutUpdated;

            //this.Text = "0";
            //this.Text = "";
        }

        void NewBox_LayoutUpdated(object sender, EventArgs e)
        {
            if (this.WaterMarkLable != null && this.WaterMarkLable.Content == null)
            {
                this.WaterMarkLable.Content = WaterMark;
            }
        }

        public static readonly DependencyProperty FocusedProperty = DependencyProperty.Register("Focused", typeof(bool), typeof(NewBox));
        /// <summary>
        /// 是否默认选中
        /// </summary>
        [Description("是否默认选中，不设置为默认不选中"), Category("输入设置"), DefaultValue(false)]
        public bool Focused
        {
            get
            {
                return (bool)GetValue(FocusedProperty);
            }
            set
            {
                SetValue(FocusedProperty, value);
            }
        }

        public static readonly DependencyProperty SelTextProperty = DependencyProperty.Register("SelText", typeof(bool), typeof(NewBox));
        /// <summary>
        /// 是否默认选中
        /// </summary>
        [Description("是否默认选中所有文字，不设置为默认不选中"), Category("输入设置"), DefaultValue(false)]
        public bool SelText
        {
            get
            {
                return (bool)GetValue(SelTextProperty);
            }
            set
            {
                SetValue(SelTextProperty, value);
            }
        }

        public static readonly DependencyProperty IsNumericProperty = DependencyProperty.Register("IsNumeric", typeof(bool), typeof(NewBox));
        /// <summary>
        /// 是否默认选中
        /// </summary>
        [Description("是否只允许输入数字，不设置为普通输入框"), Category("输入设置"), DefaultValue(false)]
        public bool IsNumeric
        {
            get
            {
                return (bool)GetValue(IsNumericProperty);
            }
            set
            {
                SetValue(IsNumericProperty, value);
            }
        }

        public static readonly DependencyProperty InitialReloadProperty = DependencyProperty.Register("InitialReload", typeof(bool), typeof(NewBox));
        /// <summary>
        /// 是否默认选中
        /// </summary>
        [Description("是否第一次输入数字时，重置默认加载"), Category("输入设置"), DefaultValue(false)]
        public bool InitialReload
        {
            get
            {
                return (bool)GetValue(InitialReloadProperty);
            }
            set
            {
                SetValue(InitialReloadProperty, value);
            }
        }

        public static readonly DependencyProperty IsPasswordProperty = DependencyProperty.Register("IsPassword", typeof(bool), typeof(NewBox));
        /// <summary>
        /// 是否默认选中
        /// </summary>
        [Description("是否是密码输入框，不设置为普通输入框"), Category("输入设置"), DefaultValue(false)]
        public bool IsPassword
        {
            get
            {
                return (bool)GetValue(IsPasswordProperty);
            }
            set
            {
                SetValue(IsPasswordProperty, value);
            }
        }


        void NewBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this != null && !string.IsNullOrEmpty(this.Text))
            {
                if (this.WaterMarkLable != null)
                {
                    this.WaterMarkLable.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                if (this.WaterMarkLable != null)
                {
                    this.WaterMarkLable.Visibility = Visibility.Visible;
                }
            }
        }

        public string WaterMark
        {
            get { return (string)GetValue(WaterMarkProperty); }

            set { SetValue(WaterMarkProperty, value); }
        }

        public static DependencyProperty WaterMarkProperty =
            DependencyProperty.Register("WaterMark", typeof(string), typeof(NewBox), new UIPropertyMetadata(""));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.WaterMarkLable = this.GetTemplateChild("TextPrompt") as Label;

            this.WaterMarkScrollViewer = this.GetTemplateChild("PART_ContentHost") as ScrollViewer;
        }

        void NewBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.WaterMarkLable != null && this.WaterMarkLable.Content == null)
            {
                this.WaterMarkLable.Content = WaterMark;
            }

            if (this.Focused)
            {
                this.Focus();

                if (this.Text.Length > 0)
                {
                    if (this.SelText)
                    {
                        this.SelectAll();
                    }
                    else
                    {
                        this.SelectionStart = this.Text.Length;
                    }
                }
            }      
        }
        #region 属性
        /// <summary>
        /// 在属性设计器中隐藏文本属性
        /// </summary>
        //[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        //public new string Text
        //{
        //    get
        //    {
        //        return base.Text;
        //    }
        //    set
        //    {
        //        base.Text = value;
        //    }
        //}
        public static readonly DependencyProperty DecimalProperty = DependencyProperty.Register("DecimalPlaces", typeof(ushort), typeof(NewBox));
        /// <summary>
        /// 允许输入的小数点个数
        /// </summary>
        [Description("小数位数,0表示不能输入小数"), Category("输入设置")]
        public ushort DecimalPlaces
        {
            get
            {
                return (ushort)GetValue(DecimalProperty);
            }
            set
            {
                SetValue(DecimalProperty, value);
            }
        }
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(decimal), typeof(NewBox));
        /// <summary>
        /// 可输入的最大值
        /// </summary>
        [Description("可输入的最大值"), Category("输入设置"), DefaultValue(0)]
        public decimal Maximum
        {
            get
            {
                return (decimal)GetValue(MaximumProperty);
            }
            set
            {
                SetValue(MaximumProperty, value);
            }
        }


        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(decimal), typeof(NewBox));
        /// <summary>
        /// 可输入的最小值
        /// </summary>
        [Description("可输入的最小值"), Category("输入设置"), DefaultValue(0)]
        public decimal Minimum
        {
            get
            {
                return (decimal)GetValue(MinimumProperty);
            }
            set
            {
                SetValue(MinimumProperty, value);
            }
        }


        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(decimal), typeof(NewBox),
            new PropertyMetadata(new decimal(0), null, OnValueChanged));
        /// <summary>
        /// 数值
        /// </summary>
        [Description("数值"), Category("输入设置"), DefaultValue(0)]
        public decimal Value
        {
            get
            {
                return (decimal)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }


        private static Object OnValueChanged(DependencyObject d, object baseValue)
        {
            decimal value = (decimal)baseValue;
            NewBox Nmbox = d as NewBox;
            Nmbox.Text = value.ToString();
            return value;
        }
        #endregion


        #region 内部事件

        // flag used to bypass OnTextChanged
        private bool dirtyBaseText;

        /// <summary>
        /// Provide access to base.Text without call OnTextChanged 
        /// </summary>
        private string BaseText
        {
            get { return base.Text; }
            set
            {
                dirtyBaseText = true;
                base.Text = value;
                dirtyBaseText = false;
            }
        }

        /// <summary>
        /// Clean Password
        /// </summary>
        public new string Text
        {
            get 
            {
                if (this.IsPassword)
                {
                    return _password;
                }
                else
                {
                    return base.Text;
                }
            }
            set
            {
                if (this.IsPassword)
                {
                    _password = value;
                    this.BaseText = new string(PWD_CHAR, value.Length);
                }
                else
                {
                    base.Text = value;
                }
            }
        }


        //数据绑定用的附加属性RealText  

        public static string GetRealText(DependencyObject obj)
        {
            return (string)obj.GetValue(RealTextProperty);
        }

        public static void SetRealText(DependencyObject obj, string value)
        {
            obj.SetValue(RealTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for RealText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RealTextProperty =
            DependencyProperty.RegisterAttached("RealText", typeof(string), typeof(NewBox), new UIPropertyMetadata(""));

        //第一次输入数字时，是否已经重置默认加载  

        public static bool GetReloadValue(DependencyObject obj)
        {
            return (bool)obj.GetValue(ReloadValueProperty);
        }

        public static void SetReloadValue(DependencyObject obj, bool value)
        {
            obj.SetValue(ReloadValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for RealText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReloadValueProperty =
            DependencyProperty.RegisterAttached("ReloadValue", typeof(bool), typeof(NewBox), new UIPropertyMetadata(false));

        /// <summary>
        /// 丢失焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this != null)
            {
                if (this.IsNumeric && TextCheck() && this.Text.Trim() != string.Empty)
                {
                    this.Value = Convert.ToDecimal(this.Text);
                }

                if (this != null && !string.IsNullOrEmpty(this.Text))
                {
                    if (this.WaterMarkLable != null)
                    {
                        this.WaterMarkLable.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    if (this.WaterMarkLable != null)
                    {
                        this.WaterMarkLable.Visibility = Visibility.Visible;
                    }
                }
            }
        }
        #endregion


        #region 输入限制
        /// <summary>
        /// 文本内容变更检查
        /// </summary>
        /// <param name="e"></param>
        int initial = 0;
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            initial++;

            if (initial == 1)
            {               
                if (this.Text.Length > 0)
                {
                    if (this.SelText)
                    {
                        this.Focus();
                        this.SelectAll();
                    }
                    else
                    {
                        this.SelectionStart = this.Text.Length;
                    }
                }
            }

            if (this.InitialReload && !GetReloadValue(this) && initial == 2)
            {
                TextChange[] change = new TextChange[e.Changes.Count];
                e.Changes.CopyTo(change, 0);

                int offset = change[0].Offset;
                if (change[0].AddedLength > 0)
                {
                    this.Text = this.Text.Remove(0, this.Text.Length - change[0].AddedLength);

                    SetReloadValue(this, true);
                }               
            }

            if (this.IsNumeric)
            {
                TextChange[] change = new TextChange[e.Changes.Count];
                e.Changes.CopyTo(change, 0);

                int offset = change[0].Offset;
                if (change[0].AddedLength > 0)
                {
                    double num = 0;
                    if (!string.IsNullOrEmpty(this.Text) && !Double.TryParse(this.Text, out num))
                    {
                        this.Text = this.Text.Remove(offset, change[0].AddedLength);
                        this.Select(offset, 0);
                    }
                    else
                    {
                        if (!TextCheck())
                        {
                            this.Text = this.Text.Remove(offset, change[0].AddedLength);
                            this.Select(offset, 0);
                        }
                    }
                }
            }

            if (this.IsPassword)
            {
                if (dirtyBaseText == true)
                    return;

                string currentText = this.BaseText;

                int selStart = this.SelectionStart;
                if (currentText.Length < _password.Length)
                {
                    // Remove deleted chars          
                    _password = _password.Remove(selStart, _password.Length - currentText.Length);
                    SetRealText(this, _password);
                }
                if (!string.IsNullOrEmpty(currentText))
                {
                    for (int i = 0; i < currentText.Length; i++)
                    {
                        if (currentText[i] != PWD_CHAR)
                        {
                            // Replace or insert char
                            if (this.BaseText.Length == _password.Length)
                            {
                                _password = _password.Remove(i, 1).Insert(i, currentText[i].ToString());
                            }
                            else
                            {
                                _password = _password.Insert(i, currentText[i].ToString());

                            }
                        }
                    }
                    SetRealText(this, _password);

                    this.BaseText = new string(PWD_CHAR, _password.Length);
                    this.SelectionStart = selStart;
                }
                base.OnTextChanged(e);
            }

            if (this.WaterMarkLable != null)
            {
                if (this != null && !string.IsNullOrEmpty(this.Text))
                {
                    this.WaterMarkLable.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.WaterMarkLable.Visibility = Visibility.Visible;
                }
            }
            return;
        }

        //注释这段代码的原因是因为会截获快捷键的事件，导致问题
        /// <summary>
        /// 输入限制
        /// </summary>
        /// <param name="e"></param>
        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter || e.Key == Key.Escape)
        //    {
        //        e.Handled = true;
        //        return;
        //    }
        //    //可输入：数字0-9,小数点,减号
        //    int PPos = this.Text.IndexOf('.');//获取当前小数点位置
        //    if ((e.Key >= Key.D1 && e.Key <= Key.D9)//数字0-9键
        //        || (e.Key >= Key.NumPad1 && e.Key <= Key.NumPad9))//小键盘数字0-9
        //    {
        //        if (PPos != -1 && this.SelectionStart > PPos)//可输入小数，且输入点在小数点后
        //        {
        //            if (this.Text.Length - PPos - 1 < DecimalPlaces)//小数位数是否已满
        //            {
        //                e.Handled = false;
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            e.Handled = false;
        //            return;
        //        }
        //    }
        //    else if (e.Key == Key.D0 || e.Key == Key.NumPad0)//输入的是0
        //    {
        //        if (this.SelectionStart > 0)
        //        {//非首位输入
        //            if (this.Text.Substring(0, 1) != "0")
        //            {//首位数字不为0
        //                e.Handled = false;
        //                return;
        //            }
        //            else
        //            {//首位数字为0,
        //                if (PPos == 1 && this.Text.Length - PPos - 1 < DecimalPlaces)
        //                { //0后面是小数点,小数位数未满
        //                    e.Handled = false;
        //                    return;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            e.Handled = false;
        //            return;
        //        }
        //    }
        //    else if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)//小数点
        //    {
        //        if (PPos == -1 && this.DecimalPlaces > 0)//未输入过小数点,且允许输入小数
        //        {
        //            if (this.SelectionStart == 0)
        //            {
        //                this.Text = "0.";
        //            }
        //            e.Handled = false;
        //            return;
        //        }
        //    }
        //    else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)//减号
        //    {
        //        if (this.Minimum < 0)//允许输入的最小值小于0
        //        {
        //            if (this.Text.IndexOf('-') == -1 && this.SelectionStart == 0)
        //            {
        //                e.Handled = false;
        //                return;
        //            }
        //        }
        //    }
        //    e.Handled = true;

        //    //this.SelectionStart = this.Text.Length;
        //}


        /// <summary>
        /// 输入合法性检查
        /// </summary>
        private bool TextCheck()
        {
            try
            {
                if (this.Text.Trim() == string.Empty)
                    return true;

                if (this.Text.Trim() == ".")
                    return false;

                decimal d = Convert.ToDecimal(this.Text.Trim());//转换为数字
                if (this.DecimalPlaces > 0 && this.Text.IndexOf(".")>0 && this.Text.Substring(this.Text.Length - 1) != "." && this.Text.IndexOf(".") < this.Text.Length - 1 - this.DecimalPlaces)
                {
                    this.Text = this.Text.Substring(0, this.Text.IndexOf(".") + this.DecimalPlaces + 1);

                    //d = Math.Round(d, this.DecimalPlaces);//舍入小数位数
                    //this.Value = d;
                }
                else if (this.DecimalPlaces == 0 && this.Text.IndexOf(".") > 0)
                {
                    this.Text = this.Text.Substring(0, this.Text.IndexOf("."));
                }

                if (!(this.Minimum == 0 && this.Maximum == 0))
                {
                    if (d < this.Minimum)//输入的值小于最小值
                    {
                        d = this.Minimum;
                        this.Value = d;
                    }
                    else if (d > this.Maximum)//输入的值大于最大值
                    {
                        d = this.Maximum;
                        this.Value = d;
                    }
                }
                //this.Value = d;
            }
            catch
            {
                this.Value = this.Minimum;
                return false;
            }
            return true;
        }
        #endregion
    }
}
