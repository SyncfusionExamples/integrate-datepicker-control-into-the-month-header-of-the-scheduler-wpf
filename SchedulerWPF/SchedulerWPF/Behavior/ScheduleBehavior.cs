using Microsoft.Xaml.Behaviors;
using Syncfusion.UI.Xaml.Scheduler;
using Syncfusion.Windows.Controls.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace SchedulerWPF
{
    public class ScheduleBehavior : Behavior<Grid>
    {
        Grid grid;
        SfDatePicker sfDatePicker;
        SfScheduler schedule;
        protected override void OnAttached()
        {
            grid = this.AssociatedObject as Grid;
            sfDatePicker = grid.Children[0] as SfDatePicker;
            schedule = grid.Children[1] as SfScheduler;
            sfDatePicker.ValueChanged += SfDatePicker_ValueChanged;
            schedule.ViewChanged += Schedule_ViewChanged;
        }

        private void Schedule_ViewChanged(object sender, ViewChangedEventArgs e)
        {
            var totalDays = (int)(e.NewValue.EndDate - e.NewValue.StartDate).TotalDays;
            int midDate = totalDays / 2;
            sfDatePicker.Value = e.NewValue.StartDate.AddDays(midDate);
        }

        private void SfDatePicker_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            this.schedule.DisplayDate = (e.NewValue as DateTime?).Value;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            sfDatePicker.ValueChanged -= SfDatePicker_ValueChanged;
            schedule.ViewChanged -= Schedule_ViewChanged;
            grid = null;
            schedule = null;
            sfDatePicker = null;
        }

    }
}
