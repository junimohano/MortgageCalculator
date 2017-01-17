using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MortgageCalculator.CustomRenderers
{
    public class CustomDatePicker : DatePicker
    {
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create<CustomDatePicker, double>(p => p.FontSize, Font.Default.FontSize);
        //        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create<MyExtendedEditor, string>(p => p.FontFamily, Font.Default.FontFamily);
        //        public static readonly BindableProperty TextColorProperty = BindableProperty.Create<MyExtendedEditor, Color>(p => p.TextColor, Color.Default);
        //        public static readonly BindableProperty PlaceHolderTextColorProperty = BindableProperty.Create<MyExtendedEditor, Color>(p => p.TextColor, Color.Default);
        //        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(MyExtendedEditor), -1, BindingMode.OneWay, null, null, null, null);
        //        public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create<MyExtendedEditor, string>(p => p.PlaceHolder, null);

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        //public string FontFamily
        //{
        //    get { return (string)GetValue(FontFamilyProperty); }
        //    set { SetValue(FontFamilyProperty, value); }
        //}

        //public Color TextColor
        //{
        //    get { return (Color)GetValue(TextColorProperty); }
        //    set { SetValue(TextColorProperty, value); }
        //}
        //public string PlaceHolder
        //{
        //    get { return (string)GetValue(PlaceHolderProperty); }
        //    set { SetValue(PlaceHolderProperty, value); }
        //}

        //public int MaxLength
        //{
        //    get
        //    {
        //        return (int)base.GetValue(MyExtendedEditor.MaxLengthProperty);
        //    }
        //    set
        //    {
        //        base.SetValue(MyExtendedEditor.MaxLengthProperty, value);
        //    }
        //}

        //public Color PlaceHolderTextColor
        //{
        //    get { return (Color)GetValue(PlaceHolderTextColorProperty); }
        //    set { SetValue(PlaceHolderTextColorProperty, value); }
        //}
    }
}
