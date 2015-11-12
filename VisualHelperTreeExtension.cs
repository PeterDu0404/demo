using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

using System.Data;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

    /// <summary>
    /// WPF/Silverlight 查找控件扩展方法
    /// </summary>
    public static class VisualHelperTreeExtension
    {
        /// <summary>
        /// 根据控件名称，查找父控件
        /// elementName为空时，查找指定类型的父控件
        /// </summary>
        public static T GetParentByName<T>(this DependencyObject obj, string elementName)
        where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                if ((parent is T) && (((T)parent).Name == elementName || string.IsNullOrEmpty(elementName)))
                {
                    return (T)parent;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        /// <summary>
        /// 根据控件名称，查找子控件
        /// elementName为空时，查找指定类型的子控件
        /// </summary>
        public static T GetChildByName<T>(this DependencyObject obj, string elementName)
        where T : FrameworkElement
        {
            DependencyObject child = null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).Name == elementName) || (string.IsNullOrEmpty(elementName)))
                {
                    return (T)child;
                }
                else
                {
                    T grandChild = GetChildByName<T>(child, elementName);
                    if (grandChild != null)
                    {
                        return grandChild;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 根据控件名称，查找子控件集合
        /// elementName为空时，查找指定类型的所有子控件
        /// </summary>
        public static List<T> GetChildsByName<T>(this DependencyObject obj, string elementName)
        where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).Name == elementName) || (string.IsNullOrEmpty(elementName)))
                {
                    childList.Add((T)child);
                }
                else
                {
                    List<T> grandChildList = GetChildsByName<T>(child, elementName);
                    if (grandChildList != null)
                    {
                        childList.AddRange(grandChildList);
                    }
                }
            }
            return childList;
        }


        /// <summary>
        /// 根据控件名称，查找子控件集合
        /// elementName为空时，查找指定类型的所有子控件
        /// </summary>
        public static List<T> GetChildsByBlueName<T>(this DependencyObject obj, string elementName)
        where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).Name.Contains(elementName)) || (string.IsNullOrEmpty(elementName)))
                {
                    childList.Add((T)child);
                }
                else
                {
                    List<T> grandChildList = GetChildsByBlueName<T>(child, elementName);
                    if (grandChildList != null)
                    {
                        childList.AddRange(grandChildList);
                    }
                }
            }
            return childList;
        }

        /// <summary>
        /// 根据控件名称，查找父控件
        /// elementUid为空时，查找指定类型的父控件
        /// </summary>
        public static T GetParentByUid<T>(this DependencyObject obj, string elementUid)
        where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                if ((parent is T) && (((T)parent).Uid == elementUid || string.IsNullOrEmpty(elementUid)))
                {
                    return (T)parent;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        /// <summary>
        /// 根据控件名称，查找子控件
        /// elementName为空时，查找指定类型的子控件
        /// </summary>
        public static T GetChildByUid<T>(this DependencyObject obj, string elementUid)
        where T : FrameworkElement
        {
            DependencyObject child = null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).Uid == elementUid) || (string.IsNullOrEmpty(elementUid)))
                {
                    return (T)child;
                }
                else
                {
                    T grandChild = GetChildByUid<T>(child, elementUid);
                    if (grandChild != null)
                    {
                        return grandChild;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 根据控件名称，查找子控件集合
        /// elementName为空时，查找指定类型的所有子控件
        /// </summary>
        public static List<T> GetChildsByUid<T>(this DependencyObject obj, string elementUid)
        where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).Uid == elementUid) || (string.IsNullOrEmpty(elementUid)))
                {
                    childList.Add((T)child);
                }
                else
                {
                    List<T> grandChildList = GetChildsByUid<T>(child, elementUid);
                    if (grandChildList != null)
                    {
                        childList.AddRange(grandChildList);
                    }
                }
            }
            return childList;
        }
    }
