using ReactiveUI;
using ReactiveUI.Xaml;
using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RxUI_BindingFailure
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window, IViewFor<MainWindowViewModel>
    {
        public MainWindowView()
        {
            InitializeComponent();

            ViewModel = new MainWindowViewModel();
            this.DataContext = ViewModel;

            //this isn't working as I would expect. Am I Doing it Wrong?
            this.WhenAnyValue(x => x.items.SelectedValue)
                .Select(x => x == null ? string.Empty : x.ToString())
                .BindTo(this, x => x.ViewModel.SelectedItem);

            //if the above binding works then the selected text should show up
            this.OneWayBind(ViewModel, vm => vm.SelectedItem, v => v.theSelectedItem.Text);
            //if the above binding works then the button should get enabled.
            this.BindCommand(ViewModel, vm => vm.GoToCmd, v => v.goTo);


            //this is not working as I would expect. If I remove the xaml binding nothing shows up.
            //this.OneWayBind(ViewModel, vm => vm.Items, v=>v.items.ItemsSource);

        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MainWindowViewModel), typeof(MainWindowView), new PropertyMetadata(null));

        public MainWindowViewModel ViewModel
        {
            get { return (MainWindowViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainWindowViewModel)value; }
        }
    }
}
