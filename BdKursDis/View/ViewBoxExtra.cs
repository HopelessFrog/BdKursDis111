using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


    public class ViewBoxExtra
    {
        /// <summary>
        /// Setter for the DisableScaling attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetDisableScaling(DependencyObject element, bool value)
        {
            element.SetValue(DisableScalingProperty, value);
        }

        /// <summary>
        /// Getter for the DisableScaling attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetDisableScaling(DependencyObject element)
        {
            return (bool)element.GetValue(DisableScalingProperty);
        }

        /// <summary>
        /// This property is the one that we will set in the XAML elements 
        /// in order to indicate that we don't want that item to resize
        /// </summary>
        public static readonly DependencyProperty DisableScalingProperty =
         DependencyProperty.RegisterAttached("DisableScaling",
                 typeof(bool), typeof(Viewbox), new UIPropertyMetadata(AddScalingHandler));

        /// <summary>
        /// Setter for the NonScaledObjects attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetNonScaledObjectsProperty
         (DependencyObject element, List<DependencyObject> value)
        {
            element.SetValue(NonScaledObjectsProperty, value);
        }

        /// <summary>
        /// Getter for the NonScaledObjects attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static List<DependencyObject>
         GetNonScaledObjectsProperty(DependencyObject element)
        {
            return (List<DependencyObject>)element.GetValue(NonScaledObjectsProperty);
        }

        /// <summary>
        /// This property is the one that will hold the references 
        /// to the objects that will not be resized for that viewbox
        /// </summary>
        public static readonly DependencyProperty NonScaledObjectsProperty =
               DependencyProperty.RegisterAttached("NonScaledObjects",
                 typeof(List<DependencyObject>), typeof(Viewbox), null);

        /// <summary>
        /// Handler used to prepare the NonScaleObjects list and 
        /// to link the size changed event of the viewbox to our handler
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void AddScalingHandler
          (DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                var viewBox = ObtainParentViewbox(d);
                var nonScaleObjects =
                (List<DependencyObject>)viewBox.GetValue(NonScaledObjectsProperty);
                if ((bool)e.NewValue && nonScaleObjects == null)
                {
                    nonScaleObjects = new List<DependencyObject>();
                    viewBox.SetValue(NonScaledObjectsProperty, nonScaleObjects);
                    viewBox.SizeChanged += ViewBox_SizeChanged;
                }

                if ((bool)e.NewValue)
                {
                    nonScaleObjects.Add(d);
                }
                else
                {
                    nonScaleObjects.Remove(d);
                }
            }
            catch (NullReferenceException exc)
            {
                throw new Exception
                ("The element must be contained inside an ViewBoxExtra", exc);
            }
        }

        /// <summary>
        /// This will be called when a viewBox has any item with the property DisableScaling=true
        /// The main process to invert the object is done here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ViewBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var viewBox = sender as Viewbox;
            var transform =
            ((ContainerVisual)VisualTreeHelper.GetChild(sender as DependencyObject, 0)).Transform;
            if (transform != null && viewBox != null)
            {
                foreach (var nonScaleObject in
                (List<DependencyObject>)viewBox.GetValue(NonScaledObjectsProperty))
                {
                    var element = (FrameworkElement)nonScaleObject;
                    element.LayoutTransform = (Transform)transform.Inverse;
                }
            }
        }

        /// <summary>
        /// Method in order to obtain the viewbox that contains the item
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static Viewbox ObtainParentViewbox(DependencyObject d)
        {
            var parent = VisualTreeHelper.GetParent(d);
            return parent is Viewbox ? parent as Viewbox : ObtainParentViewbox(parent);
        }
    }
